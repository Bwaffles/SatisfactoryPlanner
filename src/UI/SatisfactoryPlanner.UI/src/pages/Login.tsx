import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { useApi, ApiResponse } from "../hooks/use-api";
import { useCurrentUser } from "../features/users/api/getCurrentUser";
import makeDebugger from "../utils/makeDebugger";
import { Spinner } from "../components/Elements/Spinner";
const debug = makeDebugger("Login");

export const Login = () => {
    const navigate = useNavigate();
    const api = useApi();
    const { user } = useAuth0();
    const { data, isRejected, error } = useCurrentUser();

    debug("Rendering...");

    useEffect(() => {
        // Using useEffect because without it calling navigate causes a console error. Need to navigate after rendering is finished.
        debug("Data effect...", data);
        if (data) {
            debug("User already exists. Redirecting to home...");
            navigate("/");
        } else if (data == "") {
            debug("User not created. Starting creation process...");

            const auth0UserId = user!.sub;
            api("/user-access/users/@me", {
                method: "POST",
                body: JSON.stringify({
                    auth0UserId: auth0UserId,
                }),
            }).then((value: ApiResponse) => {
                debug("CreateCurrentUser response:", value);

                if (value.statusCode === 201) {
                    debug("User created. Redirecting to home...");
                    navigate("/");
                } else {
                }
            });
        }
    }, [data]);

    useEffect(() => {
        debug("Rejected effect...", isRejected);
        if (isRejected) {
            debug(error.message);
            navigate("/loginError"); // if finishing the login process on the server fails, need to log the user out of auth0 so we're in a consistent state
        }
    }, [isRejected]);

    return (
        <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
            <Spinner size="xl" />
        </div>
    );
};
