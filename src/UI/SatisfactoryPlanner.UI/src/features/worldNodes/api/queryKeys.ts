export const worldNodeKeys = {
  all: ["worldNodes"] as const,
  list: (resourceId: string) =>
    [...worldNodeKeys.all, "list", resourceId] as const,
  details: (id: string) => [...worldNodeKeys.all, "details", id] as const,
};
