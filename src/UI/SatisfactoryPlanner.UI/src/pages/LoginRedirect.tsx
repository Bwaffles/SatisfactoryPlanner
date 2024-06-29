import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import { Spinner } from "components/Elements/Spinner";

// On login, Auth0 will redirect here and we'll wait until the authentication process is finished.
// For whatever reason, we can land here before the user is authenticated
// Once the user is authenticated we'll redirect to the Home page to handle the rest of the process.
// user-provider handles making sure the user/world is set up properly before loading the app
export const LoginRedirect = () => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth0();

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/");
    }
  }, [isAuthenticated, navigate]);

  return (
    <div className="flex items-center justify-center w-screen h-screen bg-gray-900">
      <Spinner size="xl" />
    </div>
  );
};
