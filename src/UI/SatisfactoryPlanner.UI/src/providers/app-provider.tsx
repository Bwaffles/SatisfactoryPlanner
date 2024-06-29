import * as React from "react";
import { ErrorBoundary } from "react-error-boundary";
import { HelmetProvider } from "react-helmet-async";
import { BrowserRouter as Router } from "react-router-dom";
import { QueryClientProvider } from "react-query";

import { Spinner } from "components/Elements/Spinner";
import { queryClient } from "lib/react-query";
import { AuthProvider } from "./auth-provider";

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
            <AuthProvider>
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
