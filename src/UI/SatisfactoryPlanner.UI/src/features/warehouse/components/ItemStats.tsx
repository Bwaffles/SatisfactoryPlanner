import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faChevronDown,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";

import { formatNumber } from "utils/format";
import { cn } from "utils";
import { WarehouseItem, useGetItemStats } from "../api/getItemStats";
import { Spinner } from "components/Elements";

const GridTemplate = "grid grid-cols-[40px_repeat(6,_minmax(0,_1fr))]";

export const ItemStats = () => {
  const { data: response, isSuccess, isError, isLoading } = useGetItemStats();
  const items = response!.data.items;

  return (
    <div className="border rounded-lg">
      {isSuccess && items.length > 0 && (
        <>
          <div
            className={cn(
              GridTemplate,
              "border-b p-4 py-3 text-muted-foreground"
            )}
          >
            <div className="col-start-2">Item</div>
            <div className="text-right">Produced</div>
            <div className="text-right">Exported</div>
            <div className="text-right">Available</div>
            <div className="text-right">Imported</div>
            <div className="text-right">Consumed</div>
          </div>
          {items.map((warehouseItem: WarehouseItem) => (
            <WarehouseItemRow
              key={warehouseItem.itemId}
              warehouseItem={warehouseItem}
            />
          ))}
        </>
      )}
      {isSuccess && items.length == 0 && (
        <div className="py-10 px-4 text-center">
          No stats to report. Get to work, Pioneer!
        </div>
      )}
      {isLoading && (
        <div className="py-10 px-4 flex-col items-center">
          <Spinner size="lg" variant="light" />
        </div>
      )}
      {isError && (
        <div className="py-10 px-4 text-center">
          Looks like we blew a fuse trying to get your stats. We'll send someone
          to reset it shortly, try again later.
        </div>
      )}
    </div>
  );
};

interface WarehouseItemRowProps {
  warehouseItem: WarehouseItem;
}
const WarehouseItemRow = (props: WarehouseItemRowProps) => {
  const [expanded, setExpanded] = useState<boolean>(false);
  const warehouseItem = props.warehouseItem;

  return (
    <>
      <div className={cn(GridTemplate, "border-b p-4")}>
        <div>
          <button
            type="button"
            title={expanded ? "Collapse" : "Expand"}
            onClick={() => setExpanded(!expanded)}
          >
            <FontAwesomeIcon icon={expanded ? faChevronDown : faChevronRight} />
          </button>
        </div>
        <div>{warehouseItem.itemName}</div>
        <div className="text-right">
          {formatNumber(warehouseItem.amountProduced)}
        </div>
        <div className="text-right">
          {formatNumber(warehouseItem.amountExported)}
        </div>
        <div
          className={cn(
            "text-right",
            warehouseItem.amountAvailable >= 0 ? "" : "text-destructive-text"
          )}
        >
          {formatNumber(warehouseItem.amountAvailable)}
        </div>
        <div className="text-right">
          {formatNumber(warehouseItem.amountImported)}
        </div>
        <div className="text-right">
          {formatNumber(warehouseItem.amountConsumed)}
        </div>
      </div>
      {expanded && RenderDetails(warehouseItem)}
    </>
  );
};

function RenderDetails(warehouseItem: WarehouseItem) {
  return (
    <div className="border-b p-4 flex flex-col gap-4">
      <div className={cn(GridTemplate, "text-muted-foreground")}>
        <div className="col-start-2">Produced</div>
      </div>
      {warehouseItem.producedAt.map((productionSource) => {
        return (
          <div key={productionSource.name} className={cn(GridTemplate)}>
            <div className="col-start-2 ml-6">{productionSource.name}</div>
            <div className="text-right">
              {formatNumber(productionSource.amountProduced)}
            </div>
            <div className="text-right">
              {formatNumber(productionSource.amountExported)}
            </div>
            <div
              className={cn(
                "text-right",
                productionSource.amountAvailable >= 0
                  ? ""
                  : "text-destructive-text"
              )}
            >
              {formatNumber(productionSource.amountAvailable)}
            </div>
            <div className="text-right">-</div>
            <div className="text-right">-</div>
          </div>
        );
      })}
      {warehouseItem.consumedAt.length > 0 && (
        <>
          <div className={cn(GridTemplate, "text-muted-foreground")}>
            <div className="col-start-2">Consumed</div>
          </div>
          {warehouseItem.consumedAt.map((consumptionSource) => {
            return (
              <div key={consumptionSource.name} className={cn(GridTemplate)}>
                <div className="col-start-2 ml-6">{consumptionSource.name}</div>
                <div className="text-right">-</div>
                <div className="text-right">-</div>
                <div className="text-right">-</div>
                <div className="text-right">
                  {formatNumber(consumptionSource.amountImported)}
                </div>
                <div className="text-right">
                  {formatNumber(consumptionSource.amountConsumed)}
                </div>
              </div>
            );
          })}
        </>
      )}
    </div>
  );
}
