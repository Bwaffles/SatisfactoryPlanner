import React from "react";
import { useParams } from "react-router-dom";

import Doggo from "assets/Lizard_Doggo.png";
import { ContentLayout } from "components/Layout/ContentLayout";
import { WorldNodeList } from "features/worldNodes/components/WorldNodeList";
import { useGetResourceDetails } from "features/resources/api/getResourceDetails";
import { Card, CardContent } from "components/Elements/Card/Card";
import { H2 } from "components/Typography";

export const ResourceDetails = () => {
  const { resourceId } = useParams();
  const {
    isError,
    data: resourceDetails,
    error,
  } = useGetResourceDetails(resourceId!);

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  var description = resourceDetails!.description.split("\\r\\n");
  return (
    <ContentLayout title={resourceDetails!.name}>
      <H2>Resource Details</H2>
      <Card>
        <CardContent className="flex flex-col gap-4">
          <div className="flex flex-row gap-4">
            <img className="h-40 w-40 text-center" alt="Icon" src={Doggo}></img>
            <div>
              {description.map((line) => {
                return (
                  <p key={line} className="mb-4">
                    {line}
                  </p>
                );
              })}
            </div>
          </div>
        </CardContent>
      </Card>

      <H2 className="mt-10">Nodes</H2>
      <WorldNodeList resourceId={resourceId!}></WorldNodeList>
    </ContentLayout>
  );
};
