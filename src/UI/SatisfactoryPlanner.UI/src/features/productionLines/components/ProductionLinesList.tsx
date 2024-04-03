import React from "react";
import { ProductionLine } from "../types";

export const ProductionLinesList = () => {
  var productionLines: ProductionLine[] = [
    // { id: "1", name: "Iron Ingots - Line 1" },
    // { id: "2", name: "Iron Ingots - Line 2" },
    // { id: "3", name: "Copper Main - Line 1" },
    // {
    //   id: "4",
    //   name: "A really long production line name to test the layout with really long production line names and it's still not long enough so I'm making it longer",
    // },
  ];
  return (
    <div className="border-gray-700 border rounded">
      {productionLines.length > 0 && (
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
      )}

      {productionLines.length === 0 && (
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
