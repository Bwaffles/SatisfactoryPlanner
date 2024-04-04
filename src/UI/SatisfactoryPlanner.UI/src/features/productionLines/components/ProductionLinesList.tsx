import React from "react";

import { useGetProductionLines } from "../api/getProductionLines";

export const ProductionLinesList = () => {
  const {
    isError,
    error,
    isSuccess,
    data: productionLines,
  } = useGetProductionLines();

  if (isError) {
    return <span>Error: {(error as Error).message}</span>;
  }

  return (
    <div className="border-gray-700 border rounded">
      {isSuccess && productionLines.length ? (
        <ul>
          {productionLines.map((productionLine) => {
            return (
              <li
                key={productionLine.id}
                className="p-5 border-gray-700 border-b last:border-b-0"
              >
                <a
                  href={`production-lines/${productionLine.id}`}
                  title={`View the details of ${productionLine.name}.`}
                  className="font-semibold hover:text-sky-700"
                >
                  {productionLine.name}
                </a>
              </li>
            );
          })}
        </ul>
      ) : (
        <div className="text-center py-20">
          <h2 className="font-bold text-lg mb-3">
            There are no production Lines.
          </h2>
          <p className="text-gray-400">
            Get started by{" "}
            <a
              href="/production-lines/set-up"
              className="text-sky-600 hover:underline"
            >
              setting up a new production line
            </a>
            .
          </p>
        </div>
      )}
    </div>
  );
};
