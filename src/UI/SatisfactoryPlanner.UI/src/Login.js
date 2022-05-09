import React from 'react'

export class Login extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            login: '',
            password: ''
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleLoginChange = this.handleLoginChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
    }

    handleSubmit(event) {

        //var formdata = new FormData();
        //formdata.append("client_id", "ro.client");
        //formdata.append("grant_type", "password");
        //formdata.append("username", login);
        //formdata.append("client_secret", "secret");
        //formdata.append("password", password);

        //HttpClient.postForm < AuthenticationResult > ("connect/token", formdata)
        //    .then(data => AuthenticationService.authenticate(data, login))
        //    .then(() => props.onSuccessfulLoginEvent())
        //    .catch(() => {
        //        setErrorMessage('Invalid username or password');
        //        setPassword('');
        //    });

        //event.preventDefault();
    }

    handleLoginChange(event) {
        this.setState({ login: event.target.value });
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    render() {
        const { login, password } = this.state;

        return (
            <main className="mx-auto flex min-h-screen w-full items-center justify-center bg-gray-900 text-white font-mono">
                <section className="flex w-[30rem] flex-col space-y-10">
                    <div className="text-center text-4xl font-medium">Satisfactory Planner</div>
                    <form onSubmit={(e) => this.handleSubmit(e)}>

                        {/*{errorMessage !== null &&*/}
                        {/*    <div>{errorMessage}</div>*/}
                        {/*}*/}

                        <div className="w-full transform border-b-2 bg-transparent text-lg duration-300 focus-within:border-sky-600 mb-10">
                            <input
                                type="text"
                                placeholder="Email"
                                className="w-full border-none bg-transparent outline-none focus:outline-none"
                                value={login}
                                onChange={(e) => this.handleLoginChange(e)}
                            />
                        </div>
                        <div className="w-full transform border-b-2 bg-transparent text-lg duration-300 focus-within:border-sky-600 mb-10">
                            <input
                                type="password"
                                autoComplete="password"
                                placeholder="Password"
                                className="w-full border-none bg-transparent outline-none focus:outline-none"
                                value={password}
                                onChange={(e) => this.handlePasswordChange(e)}
                            />
                        </div>
                        <button
                            className="w-full py-3 text-xl font-bold uppercase rounded bg-sky-800 duration-300 hover:bg-sky-900 mb-5"
                            type="submit"
                        >
                            Log In
                        </button>
                        <a
                            href="#"
                            className="transform text-center text-lg font-bold text-sky-600 duration-300 hover:text-sky-800 mb-10"
                        >
                            Forgot Password?
                        </a>
                        <br/>
                        <a
                            href="/registration"
                            className="transform text-center text-lg font-bold text-sky-600 duration-300 hover:text-sky-800 mb-10"
                        >
                            Create Account
                        </a>
                    </form>
                </section>
            </main>
        )
    }
}
export default Login