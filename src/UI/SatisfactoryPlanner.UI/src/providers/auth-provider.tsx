import React from "react";
import { useNavigate } from "react-router-dom";
import { AppState, Auth0Provider } from "@auth0/auth0-react";

import * as Config from "config";

export const AuthProvider = ({ children, ...props }: any) => {
  const navigate = useNavigate();
  const onRedirectCallback = (appState: AppState) => {
    navigate((appState && appState.returnTo) || window.location.pathname);
  };

  return (
    <Auth0Provider
      domain={Config.AUTH0_DOMAIN}
      clientId={Config.AUTH0_CLIENT_ID}
      authorizationParams={{
        redirect_uri: Config.REDIRECT_URL,
        audience: Config.API_URL,
      }}
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
