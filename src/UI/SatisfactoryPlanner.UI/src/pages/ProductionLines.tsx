import React from "react";

import { ContentLayout } from "@/components/Layout/ContentLayout";
import { ProductionLinesList } from "@/features/productionLines/components/ProductionLinesList";

export const ProductionLines = () => {
  return (
    <ContentLayout
      title="Production Lines"
      description="Set up production lines to process items into more complex parts."
    >
      <ProductionLinesList />
    </ContentLayout>
  );
};
