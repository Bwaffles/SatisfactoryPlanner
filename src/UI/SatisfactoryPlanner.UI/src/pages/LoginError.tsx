import React from 'react';
import { useAuth0 } from "@auth0/auth0-react";

function LoginError() {
  const { isAuthenticated, logout } = useAuth0();

  if (isAuthenticated){
    logout({ returnTo: window.location.href })
  }

  return (
    <p>Something went wrong logging in.</p>
  );
}

export default LoginError;