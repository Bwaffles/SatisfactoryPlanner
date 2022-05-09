import { FormEvent, ChangeEvent } from "react";
import * as React from "react"
import HttpClient from "./HttpClient";

type RegistrationProps = {
};

type RegistrationState = {
    login: string;
    email: string;
    password: string;
    errorMessage: string;
    successMessage: string;
};

export class Registration extends React.Component<RegistrationProps, RegistrationState> {

    constructor(props: RegistrationProps) {
        super(props);

        this.state = {
            login: "",
            email: "",
            password: "",
            errorMessage: "",
            successMessage: ""
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleUserRegistrationSuccess = this.handleUserRegistrationSuccess.bind(this);
        this.handleLoginChange = this.handleLoginChange.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
    }

    handleSubmit(event: FormEvent<HTMLFormElement>) {
        const { login, email, password } = this.state;
        var confirmLink = `${window.location.origin}/registration-confirm/`;
        const request = {
            login: login,
            email: email,
            password: password,
            confirmLink: confirmLink
        }

        HttpClient.post<any>("userAccess/userRegistrations", JSON.stringify(request))
            .then(() => this.handleUserRegistrationSuccess())
            .catch(() => {
                this.setState({ errorMessage: 'Error during registration' });
            });

        event.preventDefault();
    }

    handleUserRegistrationSuccess() {
        this.setState({ successMessage: 'Registration success' });
    }

    handleLoginChange(event: ChangeEvent<HTMLInputElement>) {
        this.setState({ login: event.target.value });
    }

    handleEmailChange(event: ChangeEvent<HTMLInputElement>) {
        this.setState({ email: event.target.value });
    }

    handlePasswordChange(event: ChangeEvent<HTMLInputElement>) {
        this.setState({ password: event.target.value });
    }

    render() {
        const { login, email, password, successMessage, errorMessage } = this.state;

        return (
            <main className="mx-auto flex min-h-screen w-full items-center justify-center bg-gray-900 text-white font-mono">
                <section className="flex w-[30rem] flex-col space-y-10">
                    <div className="text-center text-4xl font-medium">Satisfactory Planner</div>

                    {successMessage !== null &&
                        <div>{successMessage}</div>
                    }

                    {errorMessage !== null &&
                        <div>{errorMessage}</div>
                    }
                    {successMessage === "" &&

                        <form onSubmit={(e: FormEvent<HTMLFormElement>) => this.handleSubmit(e)}>

                            <div className="w-full transform border-b-2 bg-transparent text-lg duration-300 focus-within:border-sky-500 mb-10">
                                <input
                                    type="text"
                                    placeholder="Username"
                                    className="w-full border-none bg-transparent outline-none focus:outline-none"
                                    value={login}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => this.handleLoginChange(e)}
                                />
                            </div>
                            <div className="w-full transform border-b-2 bg-transparent text-lg duration-300 focus-within:border-sky-500 mb-10">
                                <input
                                    type="text"
                                    placeholder="Email"
                                    className="w-full border-none bg-transparent outline-none focus:outline-none"
                                    value={email}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => this.handleEmailChange(e)}
                                />
                            </div>
                            <div className="w-full transform border-b-2 bg-transparent text-lg duration-300 focus-within:border-sky-500 mb-10">
                                <input
                                    type="password"
                                    autoComplete="password"
                                    placeholder="Password"
                                    className="w-full border-none bg-transparent outline-none focus:outline-none"
                                    value={password}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => this.handlePasswordChange(e)}
                                />
                            </div>
                            <button
                                className="w-full py-3 text-xl font-bold uppercase rounded bg-sky-800 duration-300 hover:bg-sky-900 mb-5"
                                type="submit"
                            >
                                Register
                            </button>
                            <br />
                            <a
                                href="/login"
                                className="transform text-center text-lg font-bold text-sky-600 duration-300 hover:text-sky-800 mb-10"
                            >
                                Back to login
                            </a>
                        </form>
                    }
                </section>
            </main>
        );
    }
}

export default Registration