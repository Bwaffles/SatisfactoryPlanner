export const warehouseKeys = {
  all: ["warehouse"] as const,
  itemStats: () => [...warehouseKeys.all, "itemStats"] as const,
};
