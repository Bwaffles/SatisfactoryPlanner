import { Auth0DecodedHash, WebAuth } from "auth0-js";
import { NavigateFunction } from "react-router-dom";

export default class Auth {
    auth0: WebAuth;

    constructor() {
        this.auth0 = new WebAuth({
            domain: process.env.REACT_APP_AUTH0_DOMAIN!,
            clientID: process.env.REACT_APP_AUTH0_CLIENT_ID!,
            redirectUri: process.env.REACT_APP_AUTH0_CALLBACK_URL,
            responseType: "token id_token",
            scope: "openid profile email"
        });
    }

    login = () => {
        this.auth0.authorize();
    };

    handleAuthentication(navigate: NavigateFunction) {
        this.auth0.parseHash((err, authResult) => {
            if (authResult && authResult.accessToken && authResult.idToken) {
                this.setSession(authResult);
                navigate("/");
            } else if (err) {
                navigate("/");
                alert(`Error: ${err.error}. Check the console for further details.`);
                console.log(err);
            }
        })
    }

    setSession(authResult: Auth0DecodedHash) {
        const expiresAt = JSON.stringify(
            authResult.expiresIn! * 1000 + new Date().getTime()
        );

        localStorage.setItem("access_token", authResult.accessToken!);
        localStorage.setItem("id_token", authResult.idToken!);
        localStorage.setItem("expires_at", expiresAt);
    }

    isAuthenticated() {
        const expiresAt = JSON.parse(localStorage.getItem("expires_at")!);
        return new Date().getTime() < expiresAt;
    }
}