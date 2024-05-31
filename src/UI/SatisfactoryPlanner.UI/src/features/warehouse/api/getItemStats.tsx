import { useQuery } from "react-query";

import { useApi } from "lib/api";
import { warehouseKeys } from "./queryKeys";
import storage from "utils/storage";

export const useGetItemStats = () => {
  const api = useApi();
  const worldId = storage.getWorldId();
  return useQuery<GetItemStatsResponse>({
    queryKey: warehouseKeys.itemStats(),
    queryFn: async () => api.get(`worlds/${worldId}/warehouse/item-stats`),
  });
};

export type GetItemStatsResponse = {
  data: GetItemStatsResult;
};

export type GetItemStatsResult = {
  items: WarehouseItem[];
};

export type WarehouseItem = {
  itemId: string;
  itemName: string;
  amountProduced: number;
  amountExported: number;
  amountAvailable: number;
  amountConsumed: number;
  amountImported: number;
  producedAt: ProductionSource[];
  consumedAt: ConsumptionSource[];
};

export type ProductionSource = {
  name: string;
  amountProduced: number;
  amountExported: number;
  amountAvailable: number;
};
export type ConsumptionSource = {
  name: string;
  amountConsumed: number;
  amountImported: number;
};
