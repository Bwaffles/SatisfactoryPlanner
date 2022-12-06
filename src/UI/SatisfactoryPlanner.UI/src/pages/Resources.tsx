import React, { useEffect, useState } from "react";

import PageHeader from "../components/PageHeader";
import { useApi, ApiResponse } from "../hooks/use-api";

import makeDebugger from "../utils/makeDebugger";
const debug = makeDebugger("Resources");

export const Resources = () => {
    debug("Rendering...");

    const api = useApi();
    const [resources, setResources] = useState<any>();

    useEffect(() => {
        api("/resources/resources", {
            method: "GET",
        }).then((value: ApiResponse) => {
            debug("GetResources response: ", value);
            setResources(value.data);
        });
    }, []);

    return (
        <React.Fragment>
            <PageHeader text="Resources" />
            <pre>{JSON.stringify(resources, null, 2)}</pre>
        </React.Fragment>
    );
};
