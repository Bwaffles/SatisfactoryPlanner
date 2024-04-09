import React from "react";

import { useParams } from "react-router-dom";
import { Head } from "components/Layout/Head";
import { useGetProductionLineDetails } from "features/productionLines/api/getProductionLineDetails";

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
        <h1 className="scroll-m-20 text-3xl font-extrabold tracking-tight lg:text-4xl mb-6">
          {productionLineDetails?.name}
        </h1>
      </div>
    </>
  );
};
