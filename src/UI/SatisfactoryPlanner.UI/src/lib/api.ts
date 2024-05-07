import Axios, { AxiosInstance } from "axios";

import * as Config from "../config";
import { useAuth0 } from "@auth0/auth0-react";

export const useApi = () => {
  const { getAccessTokenSilently } = useAuth0();

  const axios = Axios.create({
    baseURL: Config.API_URL,
  });

  axios.interceptors.request.use(async (config) => {
    const accessToken = await getAccessTokenSilently({
      authorizationParams: {
        audience: Config.API_URL,
      },
    });

    var headers = config.headers!;
    headers.Accept = "application/json";
    headers["Content-Type"] = "application/json";

    if (typeof config.headers.Authorization === "undefined") {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }

    return config;
  });

  axios.interceptors.response.use(
    (response) => {
      return response.data;
    },
    (error) => {
      var type = error.response.data.type as string;
      var response: ErrorResponse = {
        messages: [],
      };

      switch (type) {
        case "https://somedomain/validation-error":
          response.messages = error.response.data.errors;
          break;
        case "https://somedomain/business-rule-validation-error":
          response.messages.push(error.response.data.detail);
          break;
      }

      return Promise.reject(response);
    }
  );

  return new ApiClient(axios);
};

export type ErrorResponse = {
  messages: string[];
};

export class ApiClient {
  private _axiosInstance: AxiosInstance;
  public constructor(axiosInstance: AxiosInstance) {
    this._axiosInstance = axiosInstance;
  }

  get<TResponseData = any, TRequestData = any>(
    url: string,
    data?: TRequestData
  ): Promise<TResponseData> {
    return this._axiosInstance.get(url, { data: data });
  }
  post<TResponseData = any, TRequestData = any>(
    url: string,
    data?: TRequestData
  ): Promise<TResponseData> {
    return this._axiosInstance.post(url, data);
  }
}
