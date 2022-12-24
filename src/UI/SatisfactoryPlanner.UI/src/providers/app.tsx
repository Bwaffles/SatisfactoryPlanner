import * as React from "react";
import { ErrorBoundary } from "react-error-boundary";
import { HelmetProvider } from "react-helmet-async";
import { BrowserRouter as Router } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";
import { QueryClient, QueryClientProvider } from "react-query";

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

const AuthProvider = ({ children, ...props }: any) => {
    return <Auth0Provider {...props}>{children}</Auth0Provider>;
};

type AppProviderProps = {
    children: React.ReactNode;
};

const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            suspense: true,
        },
    },
});

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
                <HelmetProvider>
                    <Router>
                        <AuthProvider
                            domain={Config.AUTH0_DOMAIN}
                            clientId={Config.AUTH0_CLIENT_ID}
                            redirectUri={Config.REDIRECT_URL}
                            audience={Config.API_URL}
                        >
                            <QueryClientProvider client={queryClient}>
                                {children}
                            </QueryClientProvider>
                        </AuthProvider>
                    </Router>
                </HelmetProvider>
            </ErrorBoundary>
        </React.Suspense>
    );
};
