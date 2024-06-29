import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";

import { getCurrentPioneerWorlds } from "features/worlds/api/getCurrentPioneerWorlds";
import { CurrentPioneerWorld } from "features/worlds/types";
import { useApi } from "lib/api";
import { useAuth0 } from "@auth0/auth0-react";
import { getCurrentUser } from "features/users/api/getCurrentUser";
import { CurrentUser } from "features/users/types";
import { Spinner } from "components/Elements";
import { createCurrentUser } from "features/users/api/createCurrentUser";

type UserProviderProps = {
  children: React.ReactNode;
};

export const UserProvider = ({ children }: UserProviderProps) => {
  const api = useApi();
  const { isAuthenticated, user: auth0User } = useAuth0();
  const [world, setWorld] = useState<CurrentPioneerWorld>();
  const [user, setUser] = useState<CurrentUser>();

  // Load the user--on the user's first login we will have to create the user in the system
  const loadUser = useCallback(async () => {
    if (!isAuthenticated) return;

    console.debug("Loading user");

    let user = await getCurrentUser(api);
    if (user) {
      setUser(user);
      return;
    }

    await createCurrentUser(api, auth0User!.sub!);
    user = await getCurrentUser(api);
    setUser(user);
  }, [isAuthenticated]); // only load when authenticated

  useEffect(() => {
    loadUser();
  }, [loadUser]); // will run once and when authentication changes

  // After user is loaded, get their current world--this will take a short amount of time after user is created
  const loadWorld = useCallback(async () => {
    if (!user) return;

    console.debug("Loading world");

    let worlds = await poll({
      fn: () => getCurrentPioneerWorlds(api),
      validate: (worlds: CurrentPioneerWorld[]) => worlds.length,
      interval: 2000,
      maxAttempts: 10,
    });

    setWorld(worlds[0]);
  }, [user]); // every time user changes, new world loaded

  useEffect(() => {
    loadWorld();
  }, [loadWorld]); // will run once and when user changes

  // Need this to prevent the pages from trying to load before we have the world id ready
  // worldId guaranteed to be defined if user is logged in and authenticated
  if (isAuthenticated && !world) {
    return (
      <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
        <Spinner size="xl" />
      </div>
    );
  }

  const contextValue: UserContextInterface = {
    world: world,
    user: user,
  };

  return (
    <UserContext.Provider value={contextValue}>{children}</UserContext.Provider>
  );
};

interface UserContextInterface {
  world?: CurrentPioneerWorld;
  user?: CurrentUser;
}

const initialUserContext: UserContextInterface = {
  world: undefined,
  user: undefined,
};

const UserContext = createContext<UserContextInterface>(initialUserContext);

// Hook to access user context
const useUser = (context = UserContext): UserContextInterface =>
  useContext(context) as UserContextInterface;

export default useUser;

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
}): Promise<any> => {
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
