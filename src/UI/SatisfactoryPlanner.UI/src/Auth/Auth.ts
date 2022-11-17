import { Auth0DecodedHash, Auth0UserProfile, WebAuth } from "auth0-js";
import { NavigateFunction } from "react-router-dom";

export default class Auth {
    auth0: WebAuth;
    navigate: NavigateFunction;
    userProfile: Auth0UserProfile | null;

    constructor(navigate: NavigateFunction) {
        this.auth0 = new WebAuth({
            domain: process.env.REACT_APP_AUTH0_DOMAIN!,
            clientID: process.env.REACT_APP_AUTH0_CLIENT_ID!,
            redirectUri: process.env.REACT_APP_AUTH0_CALLBACK_URL,
            responseType: "token id_token",
            scope: "openid profile email"
        });
        this.navigate = navigate;
        this.userProfile = null;
    }

    login = () => {
        this.auth0.authorize();
    };

    handleAuthentication = () => {
        this.auth0.parseHash((err, authResult) => {
            if (authResult && authResult.accessToken && authResult.idToken) {
                this.setSession(authResult);
                this.navigate("/");
            } else if (err) {
                this.navigate("/");
                alert(`Error: ${err.error}. Check the console for further details.`);
                console.log(err);
            }
        });
    }

    setSession = (authResult: Auth0DecodedHash) => {
        const expiresAt = JSON.stringify(
            authResult.expiresIn! * 1000 + new Date().getTime()
        );

        localStorage.setItem("access_token", authResult.accessToken!);
        localStorage.setItem("id_token", authResult.idToken!);
        localStorage.setItem("expires_at", expiresAt);
    }

    isAuthenticated = () => {
        const expiresAt = JSON.parse(localStorage.getItem("expires_at")!);
        return new Date().getTime() < expiresAt;
    }

    logout = () => {
        localStorage.removeItem("access_token");
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");

        this.userProfile = null;

        this.auth0.logout({
            clientID: process.env.REACT_APP_AUTH0_CLIENT_ID!,
            returnTo: process.env.REACT_APP_AUTH0_LOGOUT_URL
        });
    }

    getAccessToken = () => {
        const accessToken = localStorage.getItem("access_token");
        if (!accessToken) {
            throw new Error("No access token found.");
        }

        return accessToken;
    }

    getProfile = (cb: any) => {
        if (this.userProfile)
            return cb(this.userProfile);

        this.auth0.client.userInfo(this.getAccessToken(), (err, profile) => {
            if (profile)
                this.userProfile = profile;

            cb(this.userProfile, err);
        });
    }
}