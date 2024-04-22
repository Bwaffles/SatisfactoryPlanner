import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import storage from "utils/storage";
import { ErrorResponse, useApi } from "lib/api";
import { productionLineKeys } from "./queryKeys";

export type RenameProductionLineRequest = {
  productionLineId: string;
  data: {
    name: string;
  };
};

export const useRenameProductionLine = () => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useMutation<void, ErrorResponse, RenameProductionLineRequest>({
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries(productionLineKeys.lists());
      return queryClient.invalidateQueries(
        productionLineKeys.detail(variables.productionLineId)
      );
    },
    mutationFn: (variables: RenameProductionLineRequest) => {
      return api.post(
        `/worlds/${worldId}/production-lines/${variables.productionLineId}/rename`,
        variables.data
      );
    },
  });
};
