import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { ErrorResponse, useApi } from "lib/api";
import { productionLineKeys } from "./queryKeys";
import useUser from "providers/user-provider";

export type RenameProductionLineRequest = {
  productionLineId: string;
  data: {
    name: string;
  };
};

export const useRenameProductionLine = () => {
  const api = useApi();
  const { world } = useUser();

  return useMutation<void, ErrorResponse, RenameProductionLineRequest>({
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries(productionLineKeys.lists());
      return queryClient.invalidateQueries(
        productionLineKeys.detail(variables.productionLineId)
      );
    },
    mutationFn: (variables: RenameProductionLineRequest) => {
      return api.post(
        `/worlds/${world?.id}/production-lines/${variables.productionLineId}/rename`,
        variables.data
      );
    },
  });
};
