import React, { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";

import PageHeader from "./PageHeader";

const Profile = () => {
    const { user, getAccessTokenSilently } = useAuth0();
    const [userMetadata, setUserMetadata] = useState(null);

    useEffect(() => {
        const getUserMetadata = async () => {
            try {
                const accessToken = await getAccessTokenSilently({
                    audience: process.env.REACT_APP_AUTH0_AUDIENCE,
                    scope: "read:current_user",
                });

                const userDetailsByIdUrl = `${process.env.REACT_APP_AUTH0_API_URL}users/${user!.sub}`;

                const metadataResponse = await fetch(userDetailsByIdUrl, {
                    headers: {
                        Authorization: `Bearer ${accessToken}`,
                    },
                });

                const { user_metadata } = await metadataResponse.json();

                setUserMetadata(user_metadata);
            } catch (e) {
                console.log(e);
            }
        };

        getUserMetadata();
    }, [getAccessTokenSilently, user?.sub]);
    
    return (
        <div>
            <PageHeader text="Profile" />
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

            <hr />
            <h3>User Metadata</h3>
            {userMetadata ? (
                <pre>{JSON.stringify(userMetadata, null, 2)}</pre>
            ) : (
                "No user metadata defined"
            )}
        </div >
    );
}

export default Profile;