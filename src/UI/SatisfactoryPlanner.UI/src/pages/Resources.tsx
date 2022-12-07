import React, { useEffect, useState } from "react";

import { ContentLayout } from "../components/Layout/ContentLayout";
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
        <ContentLayout title="Resources">
            <pre>{JSON.stringify(resources, null, 2)}</pre>
        </ContentLayout>
    );
};
