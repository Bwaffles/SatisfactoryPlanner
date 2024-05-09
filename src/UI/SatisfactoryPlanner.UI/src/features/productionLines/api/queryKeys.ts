export const productionLineKeys = {
  all: ["productionLines"] as const,
  lists: () => [...productionLineKeys.all, "list"] as const,
  details: () => [...productionLineKeys.all, "detail"] as const,
  detail: (id: string) => [...productionLineKeys.details(), id] as const,
};

export const processedItemsKeys = {
  all: ["processedItems"] as const,
  itemsToProcess: () => [...processedItemsKeys.all, "itemsToProcess"] as const,
};
