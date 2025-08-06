import { QueryClient, DefaultOptions, MutationCache } from "@tanstack/react-query";

const queryConfig: DefaultOptions = {
  queries: {
    suspense: true,
    staleTime: 2 * 60 * 1000,
  },
};

export const queryClient = new QueryClient({
  defaultOptions: queryConfig,
  mutationCache: new MutationCache({
    onSuccess: (_data, _variables, _context) => {
      queryClient.invalidateQueries();
    },
  }),
});
