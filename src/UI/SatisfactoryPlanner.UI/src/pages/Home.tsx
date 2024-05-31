import * as React from "react";

import { ContentLayout } from "components/Layout/ContentLayout";
import { H2 } from "components/Typography";
import { ItemStats } from "features/warehouse/components/ItemStats";
import { useAuth0 } from "@auth0/auth0-react";

export const Home = () => {
  const { isAuthenticated } = useAuth0();

  return (
    <ContentLayout title="Home">
      {isAuthenticated ? (
        <>
          <H2>Item Stats</H2>
          <ItemStats />
        </>
      ) : (
        <>Login to get started.</>
      )}
    </ContentLayout>
  );
};
