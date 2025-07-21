export const resourceKeys = {
  all: ["resources"] as const,
  list: () => [...resourceKeys.all, "list"] as const,
  detail: (id: string) => [...resourceKeys.all, "details", id] as const,
};
