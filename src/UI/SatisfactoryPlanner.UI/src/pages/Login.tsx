import React, { useEffect } from "react";
import { useQuery } from "react-query";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { Spinner } from "components/Elements/Spinner";
import { getCurrentUser } from "features/users/api/getCurrentUser";
import { createCurrentUser } from "features/users/api/createCurrentUser";
import { getCurrentPioneerWorlds } from "features/worlds/api/getCurrentPioneerWorlds";
import { CurrentPioneerWorld } from "features/worlds/types";
import { useApi } from "lib/api";

export const Login = () => {
  const navigate = useNavigate();
  const { isSuccess, isError } = useLogin();

  // Need to wrap my navigate call in useEffect so that it runs after the component finishes rendering
  useEffect(() => {
    if (isSuccess) {
      navigate("/");
    }
  }, [isSuccess, navigate]);

  useEffect(() => {
    if (isError) {
      navigate("/loginError");
    }
  }, [isError, navigate]);

  return (
    <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
      <Spinner size="xl" />
    </div>
  );
};

const useLogin = () => {
  const api = useApi();
  const { getAccessTokenSilently, user } = useAuth0();
  const auth0UserId = user!.sub!;

  // ? is it bad to do a mutation inside of this query?
  return useQuery("login", async () => {
    const currentUser = await getCurrentUser(getAccessTokenSilently);
    if (currentUser) {
      return true;
    }

    await createCurrentUser(getAccessTokenSilently, auth0UserId);

    // Polling to see if the spawn pioneer process has finished--it should take around 4-5 seconds to complete
    // TODO better polling.
    const currentWorlds = await poll({
      fn: () => getCurrentPioneerWorlds(api),
      validate: (worlds: CurrentPioneerWorld[]) => worlds.length,
      interval: 3000,
      maxAttempts: 10,
    });
    if (currentWorlds.length) {
      return true;
    }
  });
};

const poll = async ({
  fn,
  validate,
  interval,
  maxAttempts,
}: {
  fn: any;
  validate: any;
  interval: number;
  maxAttempts: number;
}): Promise<CurrentPioneerWorld[]> => {
  let attempts = 0;

  const executePoll = async (resolve: any, reject: any) => {
    const result = await fn();
    attempts++;

    if (validate(result)) {
      return resolve(result);
    } else if (maxAttempts && attempts === maxAttempts) {
      return reject(new Error("Exceeded max attempts"));
    } else {
      setTimeout(executePoll, interval, resolve, reject);
    }
  };

  return new Promise(executePoll);
};
