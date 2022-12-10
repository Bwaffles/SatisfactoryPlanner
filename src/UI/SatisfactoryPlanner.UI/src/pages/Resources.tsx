import React from "react";

import { ContentLayout } from "../components/Layout/ContentLayout";
import { useGetResources } from "../features/resources/api/getResources";

export const Resources = () => {
    const { isError, data: resources, error } = useGetResources();

    if (isError) {
        return <span>Error: {(error as Error).message}</span>;
    }

    return (
        <ContentLayout title="Resources">
            <pre>{JSON.stringify(resources, null, 2)}</pre>
        </ContentLayout>
    );
};
