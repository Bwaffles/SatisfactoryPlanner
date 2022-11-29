import React, {useEffect} from "react";

import { useApi } from "../hooks/use-api";
import { useNavigate } from "react-router-dom";

const Callback = () => {
    const navigate = useNavigate();
    const {
        loading,
        statusCode
    } = useApi("/user-access/users/@me", {});

    useEffect(() => {
        if (loading)
            return;

        console.debug(statusCode);
        if (statusCode === 204) {
            // TODO call new command that I have to still write
            console.debug("user not created");
            navigate("/");
        }
        else if (statusCode === 200) {
            console.debug("user created. Redirecting to home...");
            navigate("/");
        } else {
            throw new Error("Something went wrong.");
        }

    });

    return (
        <h1>
            Loading...
        </h1>
    );
};

export default Callback;