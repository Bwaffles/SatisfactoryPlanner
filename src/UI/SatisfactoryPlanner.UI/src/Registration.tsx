import * as React from "react"
import HttpClient from "./HttpClient";
import Input from "./Components/Input";
import { Formik } from "formik";
import { object, string } from "yup";

type RegistrationProps = {
};

type RegistrationState = {
    errorMessage: string;
    successMessage: string;
};

export class Registration extends React.Component<RegistrationProps, RegistrationState> {

    constructor(props: RegistrationProps) {
        super(props);

        this.state = {
            errorMessage: "",
            successMessage: ""
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleUserRegistrationSuccess = this.handleUserRegistrationSuccess.bind(this);
    }

    handleSubmit(values: { email: string, login: string, password: string }) {

        var confirmLink = `${window.location.origin}/registration-confirm/`;
        const request = {
            login: values.login,
            email: values.email,
            password: values.password,
            confirmLink: confirmLink
        }

        HttpClient.post<any>("userAccess/userRegistrations", JSON.stringify(request))
            .then(() => this.handleUserRegistrationSuccess())
            .catch(() => {
                this.setState({ errorMessage: "Error during registration" });
            });
    }

    handleUserRegistrationSuccess() {
        this.setState({ successMessage: "Registration success" });
    }

    render() {
        const { successMessage, errorMessage } = this.state;

        return (
            <main className="mx-auto flex min-h-screen w-full items-center justify-center bg-gray-900 text-white font-mono">
                <section className="flex w-[30rem] flex-col space-y-10">
                    <div className="text-center text-4xl font-medium">Satisfactory Planner</div>

                    {successMessage !== "" &&
                        <div>{successMessage}</div>
                    }

                    {errorMessage !== "" &&
                        <div>{errorMessage}</div>
                    }

                    {successMessage === "" &&
                        <Formik
                            initialValues={{ email: "", login: "", password: "" }}
                            validationSchema={object({
                                login: string()
                                    .max(100, "Must be 100 characters or less.")
                                    .required("Required"),
                                email: string()
                                    .email("Invalid email address.")
                                    .max(254, "Must be 254 characters or less.")
                                    .required("Required"),
                                password: string()
                                    .max(255, "Must be 255 characters or less.")
                                    .required("Required"),

                            })}
                            onSubmit={(values) => {
                                this.handleSubmit(values);
                            }}
                        >
                            {(formik: any) => (
                                <form onSubmit={formik.handleSubmit}>
                                    <div className="mb-6">
                                        <label htmlFor="email" className="block mb-2 text-gray-300">
                                            Email <span className="text-red-700">*</span>
                                        </label>
                                        <Input
                                            name="email"
                                            type="text"
                                            {...formik.getFieldProps("email")}
                                        />
                                        {formik.touched.email && formik.errors.email ? (

                                            <div className="text-red-700 mt-2">{formik.errors.email}</div>

                                        ) : null}
                                    </div>

                                    <div className="mb-6">
                                        <label htmlFor="login" className="block mb-2 text-gray-300">
                                            Username <span className="text-red-700">*</span>
                                        </label>
                                        <Input
                                            name="login"
                                            type="text"
                                            {...formik.getFieldProps("login")}
                                        />
                                        {formik.touched.login && formik.errors.login ? (

                                            <div className="text-red-700 mt-2">{formik.errors.login}</div>

                                        ) : null}
                                    </div>

                                    <div className="mb-6">
                                        <label htmlFor="password" className="block mb-2 text-gray-300">
                                            Password <span className="text-red-700">*</span>
                                        </label>
                                        <Input
                                            name="password"
                                            autoComplete="new-password"
                                            type="password"
                                            {...formik.getFieldProps("password")}
                                        />
                                        {formik.touched.password && formik.errors.password ? (

                                            <div className="text-red-700 mt-2">{formik.errors.password}</div>

                                        ) : null}
                                    </div>

                                    <button
                                        className="w-full py-3 text-xl font-bold uppercase rounded bg-sky-800 duration-300 hover:bg-sky-900 mb-5"
                                        type="submit"
                                    >
                                        Register
                                    </button>
                                    <br />
                                    <div className="w-full text-center">
                                        <a
                                            href="/login"
                                            className="transform text-lg font-bold text-sky-600 duration-300 hover:text-sky-800 mb-10"
                                        >
                                            Back to login
                                        </a>
                                    </div>
                                </form>
                            )}
                        </Formik>
                    }
                </section>
            </main>
        );
    }
}

export default Registration