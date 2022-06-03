import * as React from "react";
import { useState } from "react";
import { Formik, Form } from "formik";
import { object, string } from "yup";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

import Input from "./Components/Input";
import HttpClient from "./HttpClient";

function Registration() {
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);

    return (
        <main className="mx-auto flex min-h-screen w-full items-center justify-center bg-gray-900 text-white font-mono">
            <section className="flex w-[30rem] flex-col space-y-10">
                <div className="text-center text-4xl font-medium">Satisfactory Planner</div>

                {successMessage !== null &&
                    <div className="text-center">{successMessage}</div>
                }

                {errorMessage !== null &&
                    <div className="mb-3 text-center text-red-700">{errorMessage}</div>
                }

                {successMessage === null &&
                    <Formik
                        initialValues={{ email: "", username: "", password: "" }}
                        validationSchema={object({
                            email: string()
                                .email("Invalid email address.")
                                .max(254, "Must be 254 characters or less.")
                                .required("Required."),
                            username: string()
                                .max(100, "Must be 100 characters or less.")
                                .required("Required."),
                            password: string()
                                .max(255, "Must be 255 characters or less.")
                                .required("Required.")
                        })}
                        onSubmit={(values, { setSubmitting }) => {
                            var confirmLink = `${window.location.origin}/registration-confirm/`;
                            const request = {
                                username: values.username,
                                email: values.email,
                                password: values.password,
                                confirmLink: confirmLink
                            }

                            setSubmitting(true);

                            HttpClient.post<any>("user-access/user-registrations", JSON.stringify(request))
                                .then(() => {
                                    setSuccessMessage("You're registered! Before getting started, check your email to confirm your account.");
                                    setSubmitting(false);
                                })
                                .catch(() => {
                                    setErrorMessage("Error during registration.");
                                    setSubmitting(false);
                                });
                        }}>
                        {(props: any) => (
                            <Form>
                                <div className="mb-6">
                                    <label htmlFor="email" className="block mb-1 text-gray-300">
                                        Email <span className="text-red-700">*</span>
                                    </label>
                                    <Input
                                        name="email"
                                        type="text"
                                        maxLength={254}
                                        {...props.getFieldProps("email")}
                                    />
                                    {props.touched.email && props.errors.email ? (

                                        <div className="text-red-700 mt-1">{props.errors.email}</div>

                                    ) : null}
                                </div>

                                <div className="mb-6">
                                    <label htmlFor="username" className="block mb-1 text-gray-300">
                                        Username <span className="text-red-700">*</span>
                                    </label>
                                    <Input
                                        name="username"
                                        type="text"
                                        maxLength={100}
                                        {...props.getFieldProps("username")}
                                    />
                                    {props.touched.username && props.errors.username ? (

                                        <div className="text-red-700 mt-1">{props.errors.username}</div>

                                    ) : null}
                                </div>

                                <div className="mb-6">
                                    <label htmlFor="password" className="block mb-1 text-gray-300">
                                        Password <span className="text-red-700">*</span>
                                    </label>
                                    <Input
                                        name="password"
                                        autoComplete="new-password"
                                        type="password"
                                        maxLength={255}
                                        {...props.getFieldProps("password")}
                                    />
                                    {props.touched.password && props.errors.password ? (

                                        <div className="text-red-700 mt-1">{props.errors.password}</div>

                                    ) : null}
                                </div>

                                <button
                                    className="w-full p-3 text-xl font-bold uppercase rounded bg-sky-800 duration-300 hover:bg-sky-900 inline-flex justify-center mb-5 focus:outline focus:outline-sky-500 focus:outline-offset-2"
                                    type="submit"
                                    disabled={props.isSubmitting}
                                >
                                    {props.isSubmitting && (
                                        <div>
                                            <FontAwesomeIcon icon={faSpinner} spin className="mr-3" />
                                            Registering...
                                        </div>
                                    )}
                                    {!props.isSubmitting && (
                                        <div>
                                            Register
                                        </div>
                                    )}
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
                            </Form>
                        )}
                    </Formik>
                }
            </section>
        </main>
    );
}

export default Registration;