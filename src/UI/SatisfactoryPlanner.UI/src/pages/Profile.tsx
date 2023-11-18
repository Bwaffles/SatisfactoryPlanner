import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import { ContentLayout } from "../components/Layout/ContentLayout";

export const Profile = () => {
    const { user } = useAuth0();

    return (
        <ContentLayout title="Profile">
            <p className="space-x-4 mb-3">
                <img
                    className="w-12 h-12 rounded-full inline "
                    src={user?.picture}
                    alt="profile pic"
                />
                <span className="text-xl">{user?.name}</span>
            </p>
            <hr />

            <h3>Profile Data</h3>
            <pre>{JSON.stringify(user, null, 2)}</pre>
        </ContentLayout>
    );
};
