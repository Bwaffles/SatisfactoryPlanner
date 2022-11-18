import React, { useState, useEffect } from "react";
import Auth0 from "auth0-js";

import Auth from "./Auth/Auth";
import PageHeader from "./PageHeader";

interface ProfileProps {
    auth: Auth;
}

const Profile = ({ auth }: ProfileProps) => {
    const [profile, setProfile] = useState<Auth0.Auth0UserProfile>();
    const [error, setError] = useState<Auth0.Auth0Error>();

    useEffect(() => {
        auth.getProfile((profile: Auth0.Auth0UserProfile, error: Auth0.Auth0Error) => {
            setProfile(profile);
            setError(error);
        });
    });

    if (!profile) return null;

    return (
        <div>
            <PageHeader text="Profile" />
            <p>{profile?.name}</p>
            <img
                style={{ maxWidth: 50, maxHeight: 50 }}
                src={profile?.picture}
                alt="profile pic"
            />
            <pre>{JSON.stringify(profile, null, 2)}</pre>
            <p>{error?.error}</p>
        </div >
    );
}

export default Profile;