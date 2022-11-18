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
            <p className="space-x-4 mb-3">
                <img
                    className="w-12 h-12 rounded-full inline "
                    src={profile?.picture}
                    alt="profile pic"
                />
                <span className="text-xl">{profile?.name}</span>
            </p>
            <pre>{JSON.stringify(profile, null, 2)}</pre>
            <p>{error?.error}</p>
        </div >
    );
}

export default Profile;