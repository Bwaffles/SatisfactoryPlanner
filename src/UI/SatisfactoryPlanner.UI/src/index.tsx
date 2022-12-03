import { Auth0Provider } from "@auth0/auth0-react";
import { createRoot } from "react-dom/client";
import * as React from "react";
import { BrowserRouter as Router, useNavigate } from "react-router-dom";

import "./index.css";
import App from "./App";

//import reportWebVitals from './reportWebVitals';

const Auth0ProviderWithRedirectCallback = ({ children, ...props }: any) => {
    const navigate = useNavigate();
    
    const onRedirectCallback = () => {
        // redirect to the callback page so that I can ensure the user is created after sign up
        navigate("/callback"); 
    };
    return (
        <Auth0Provider
            onRedirectCallback={onRedirectCallback}
            {...props}
        >
            {children}
        </Auth0Provider>
    );
};

const root = createRoot(document.getElementById("root")!);
root.render(
    //TODO disabled this because it makes the Callback effect fire twice and breaks oauth
    // https://stackoverflow.com/questions/61254372/my-react-component-is-rendering-twice-because-of-strict-mode/61897567#61897567
    //<React.StrictMode>
    <Router>
        <Auth0ProviderWithRedirectCallback
            domain={process.env.REACT_APP_AUTH0_DOMAIN!}
            clientId={process.env.REACT_APP_AUTH0_CLIENT_ID!}
            redirectUri="http://localhost:3000"
            audience="http://localhost:55915/api"
        >
            <App />
        </Auth0ProviderWithRedirectCallback>
    </Router>
    //</React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
//reportWebVitals();
