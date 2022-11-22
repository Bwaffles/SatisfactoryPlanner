import React, { useEffect, useState } from 'react';
import { useAuth0 } from '@auth0/auth0-react';

import PageHeader from "../components/PageHeader";

const Resources = () => {
    const { getAccessTokenSilently } = useAuth0();
    const [resources, setResources] = useState(null);

    useEffect(() => {
        (async () => {
            try {
                const token = await getAccessTokenSilently({
                    audience: "http://localhost:55915/api", // Value in Identifier field for the API being called.
                    scope: '', // Scope that exists for the API being called. You can create these through the Auth0 Management API or through the Auth0 Dashboard in the Permissions view of your API.
                });

                console.debug(token);
                const response = await fetch("http://localhost:55915/api/resources", {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                setResources(await response.json());
            } catch (e) {
                console.error(e);
            }
        })();
    }, [getAccessTokenSilently]);

    if (!resources) {
        return <div>Loading...</div>;
    }

    return (
        <React.Fragment>
            <PageHeader text="Resources" />
            <pre>{JSON.stringify(resources, null, 2)}</pre>
        </React.Fragment>
    );
};

export default Resources;