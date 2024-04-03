import React from "react";

import { ContentLayout } from "@/components/Layout/ContentLayout";
import { ResourcesList } from "@/features/resources/components/ResourcesList";

export const Resources = () => {
    return (
        <ContentLayout title="Resources" description="Select a resource to view its details and manage your nodes.">
            <ResourcesList />
        </ContentLayout>
    );
};
