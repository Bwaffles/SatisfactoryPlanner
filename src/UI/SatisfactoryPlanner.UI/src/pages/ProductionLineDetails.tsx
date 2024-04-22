import React from "react";
import { useParams } from "react-router-dom";

import { Head } from "components/Layout/Head";
import { useGetProductionLineDetails } from "features/productionLines/api/getProductionLineDetails";
import { ProductionLineName } from "features/productionLines/components/ProductionLineName";

export const ProductionLineDetails = () => {
  const { productionLineId } = useParams();
  const {
    isError,
    error,
    data: productionLineDetails,
  } = useGetProductionLineDetails({
    productionLineId: productionLineId!,
  });

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  return (
    <>
      <Head title={`${productionLineDetails?.name}`} />
      <div className="container py-4">
        <ProductionLineName
          productionLineId={productionLineDetails!.id}
          name={productionLineDetails!.name}
        />
      </div>
    </>
  );
};
