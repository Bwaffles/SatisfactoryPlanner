import * as React from "react";
import { ErrorBoundary } from "react-error-boundary";
import { BrowserRouter as Router, useNavigate } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";

import * as Config from "../config";
import { Spinner } from "../components/Elements/Spinner";

const ErrorFallback = () => {
    return (
        <div
            className="flex flex-col justify-center items-center w-screen h-screen bg-gray-900 text-red-900"
            role="alert"
        >
            <h2 className="text-xl font-bold">Oops, something went wrong :(</h2>
        </div>
    );
};

const Auth0ProviderWithRedirectCallback = ({ children, ...props }: any) => {
    const navigate = useNavigate();

    const onRedirectCallback = () => {
        // redirect to the callback page so that I can ensure the user is created after sign up
        navigate("/callback");
    };
    return (
        <Auth0Provider
            onRedirectCallback={onRedirectCallback}
            {...props}
        >
            {children}
        </Auth0Provider>
    );
};

type AppProviderProps = {
    children: React.ReactNode;
};

export const AppProvider = ({ children }: AppProviderProps) => {
    return (
        <React.Suspense
            fallback={
                <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
                    <Spinner size="xl" />
                </div>
            }
        >
            <ErrorBoundary FallbackComponent={ErrorFallback}>
                <Router>
                    <Auth0ProviderWithRedirectCallback
                        domain={Config.AUTH0_DOMAIN}
                        clientId={Config.AUTH0_CLIENT_ID}
                        redirectUri={Config.REDIRECT_URL}
                        audience={Config.API_URL}
                    >
                        {children}
                    </Auth0ProviderWithRedirectCallback>
                </Router>
            </ErrorBoundary>
        </React.Suspense>
    );
};
