import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { useApi, ApiResponse } from "../hooks/use-api";

import makeDebugger from '../utils/makeDebugger';
const debug = makeDebugger('Callback');

const Callback = () => {
    const navigate = useNavigate();
    const api = useApi();
    const { user } = useAuth0();

    const [error, setError] = useState<string>();

    debug("Rendering...");

    api("/user-access/users/@me",
        {
            method: "GET"
        })
        .then((value: ApiResponse) => {

            debug("GetCurrentUser response:", value);

            if (value.statusCode === 204) {
                debug("User not created. Starting creation process...");

                const auth0UserId = user!.sub;
                api("/user-access/users/@me",
                    {
                        method: "POST",
                        body: JSON.stringify({
                            auth0UserId: auth0UserId
                        })
                    })
                    .then((value: ApiResponse) => {

                        debug("CreateCurrentUser response:", value);

                        if (value.statusCode === 201) {
                            debug("User created. Redirecting to home...");
                            navigate("/");
                        } else {
                            debug("Something went wrong adding the user");
                        }
                    });
            }
            else if (value.statusCode === 200) {
                debug("User already exists. Redirecting to home...");
                navigate("/");
            } else {
                setError(error);
                debug("Error: ", value.error);

            }
        });

    return (
        <h1>
            Loading...
        </h1>
    );
};

export default Callback;