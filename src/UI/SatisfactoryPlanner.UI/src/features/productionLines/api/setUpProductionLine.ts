import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import storage from "utils/storage";
import { ErrorResponse, useApi } from "lib/api";

export type SetUpProductionLineRequest = {
  data: {
    name: string;
  };
};

export type SetUpProductionLineResponse = {
  productionLineId: string;
};

export const useSetUpProductionLine = () => {
  const api = useApi();
  const worldId = storage.getWorldId();

  return useMutation<
    SetUpProductionLineResponse,
    ErrorResponse,
    SetUpProductionLineRequest
  >({
    onSuccess: () => {
      queryClient.invalidateQueries("getProductionLines");
    },
    mutationFn: (variables: SetUpProductionLineRequest) => {
      return api.post(
        `/worlds/${worldId}/production-lines/set-up`,
        variables.data
      );
    },
  });
};
