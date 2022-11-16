import * as React from "react";
import Auth from "./Auth/Auth";
import { Link } from "react-router-dom";

interface HomeProps {
    auth: Auth;
}

const Home = ({ auth }: HomeProps) =>
    <div>
        <h1>Home</h1>
        {auth.isAuthenticated()
            ? <Link to="/profile">View Profile</Link>
            : <button onClick={auth.login}>Log In</button>
        }
    </div>;

export default Home;