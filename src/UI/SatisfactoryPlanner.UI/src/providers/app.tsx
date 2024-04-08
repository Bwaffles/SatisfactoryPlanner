import * as React from "react";
import { ErrorBoundary } from "react-error-boundary";
import { HelmetProvider } from "react-helmet-async";
import { BrowserRouter as Router, useNavigate } from "react-router-dom";
import { AppState, Auth0Provider } from "@auth0/auth0-react";
import { QueryClientProvider } from "react-query";

import * as Config from "config";
import { Spinner } from "components/Elements/Spinner";
import { queryClient } from "lib/react-query";

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
  const navigate = useNavigate();
  const onRedirectCallback = (appState: AppState) => {
    navigate((appState && appState.returnTo) || window.location.pathname);
  };

  return (
    <Auth0Provider
      onRedirectCallback={onRedirectCallback}
      useRefreshTokensFallback={true}
      useRefreshTokens={true}
      cacheLocation="localstorage"
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
        <HelmetProvider>
          <Router>
            <AuthProvider
              domain={Config.AUTH0_DOMAIN}
              clientId={Config.AUTH0_CLIENT_ID}
              authorizationParams={{
                redirect_uri: Config.REDIRECT_URL,
                audience: Config.API_URL,
              }}
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
