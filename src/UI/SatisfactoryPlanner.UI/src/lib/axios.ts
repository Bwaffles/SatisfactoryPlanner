import Axios from "axios";

import * as Config from "../config";

export const axios = Axios.create({
    baseURL: Config.API_URL,
});

axios.interceptors.request.use(async (config) => {
    var headers = config.headers!;
    headers.Accept = "application/json";
    headers["Content-Type"] = "application/json";

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

export type ErrorResponse = {
    messages: string[];
};
