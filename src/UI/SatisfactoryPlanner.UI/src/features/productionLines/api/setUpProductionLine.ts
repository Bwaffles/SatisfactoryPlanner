import { useMutation } from "react-query";

import { queryClient } from "lib/react-query";
import { ErrorResponse, useApi } from "lib/api";
import { productionLineKeys } from "./queryKeys";
import useUser from "providers/user-provider";

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
  const { world } = useUser();

  return useMutation<
    SetUpProductionLineResponse,
    ErrorResponse,
    SetUpProductionLineRequest
  >({
    onSuccess: () => {
      queryClient.invalidateQueries(productionLineKeys.lists());
    },
    mutationFn: (variables: SetUpProductionLineRequest) => {
      return api.post(
        `/worlds/${world?.id}/production-lines/set-up`,
        variables.data
      );
    },
  });
};
