import { QueryClient, DefaultOptions } from "react-query";

const queryConfig: DefaultOptions = {
    queries: {
        suspense: true,
    },
};

export const queryClient = new QueryClient({ defaultOptions: queryConfig });
