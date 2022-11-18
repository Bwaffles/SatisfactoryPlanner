import * as React from "react";
import { Link } from "react-router-dom";

import Auth from "./Auth/Auth";
import PageHeader from "./PageHeader";

interface HomeProps {
    auth: Auth;
}

const Home = ({ auth }: HomeProps) =>
    <div>
        <PageHeader text="Home" />
        {auth.isAuthenticated()
            ? <Link to="/profile">View Profile</Link>
            : <button onClick={auth.login}>Log In</button>
        }
    </div>;

export default Home;