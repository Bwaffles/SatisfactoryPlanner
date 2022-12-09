import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { useCurrentUser } from "../features/users/api/getCurrentUser";
import makeDebugger from "../utils/makeDebugger";
import { Spinner } from "../components/Elements/Spinner";
import { useCreateCurrentUser } from "../features/users/api/createCurrentUser";
const debug = makeDebugger("Login");

export const Login = () => {
    const navigate = useNavigate();
    const { data: currentUserData, isRejected: currentUserIsRejected } =
        useCurrentUser();
    const {
        run: runCreateCurrentUser,
        isFulfilled: createCurrentUserIsFufilled,
        isRejected: createCurrentUserIsRejected,
    } = useCreateCurrentUser();

    debug("Rendering...");

    // TODO can I break this up better? It's basically 2 workflows but they're intwined so I'd love to make this cleaner.
    useEffect(() => {
        // Using useEffect because without it calling navigate causes a console error. Need to navigate after rendering is finished.
        debug("Data effect...", currentUserData);
        if (currentUserData) {
            debug("User already exists. Redirecting to home...");
            navigate("/");
        } else if (currentUserData == "") {
            debug("User not created. Starting creation process...");
            runCreateCurrentUser();
        }
    }, [currentUserData]);

    useEffect(() => {
        if (createCurrentUserIsFufilled) {
            debug("User created. Redirecting to home...");
            navigate("/");
        }
    }, [createCurrentUserIsFufilled]);

    useEffect(() => {
        // TODO do I really need to redirect to a new page here?
        if (currentUserIsRejected || createCurrentUserIsRejected) {
            navigate("/loginError"); // if finishing the login process on the server fails, need to log the user out of auth0 so we're in a consistent state
        }
    }, [currentUserIsRejected, createCurrentUserIsRejected]);

    return (
        <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
            <Spinner size="xl" />
        </div>
    );
};
