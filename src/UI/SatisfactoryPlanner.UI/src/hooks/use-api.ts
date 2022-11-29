import { useEffect, useState } from 'react';
import { useAuth0 } from '@auth0/auth0-react';

export const useApi = (url: string, options: any = {}) => {
    const { getAccessTokenSilently } = useAuth0();
    const [error, setError] = useState<unknown>(null);
    const [loading, setLoading] = useState(true);
    const [statusCode, setStatusCode] = useState<number>(0);
    const [data, setData] = useState(null);
    const [refreshIndex, setRefreshIndex] = useState(0);

    useEffect(() => {
        (async () => {
            try {
                const baseUrl = "http://localhost:55915/api";
                const { ...fetchOptions } = options;
                const accessToken = await getAccessTokenSilently({
                    audience: baseUrl
                });
                const res = await fetch(baseUrl + url, {
                    ...fetchOptions,
                    headers: {
                        ...fetchOptions.headers,
                        // Add the Authorization header to the existing headers
                        Authorization: `Bearer ${accessToken}`,
                    },
                });

                setStatusCode(await res.status);
                setData(await res.json());;
                setError(null);
                setLoading(false);
            } catch (error) {
                console.error(error);
                setError(error);
                setLoading(false);
            }
        })();
    }, [refreshIndex]);

    return {
        error,
        loading,
        statusCode,
        data,
        refresh: () => setRefreshIndex(refreshIndex + 1)
    };
};