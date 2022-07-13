import * as React from 'react';
import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";

import { HttpClient } from "./HttpClient";

export function ConfirmRegistration() {
    const { registrationId } = useParams();
    const [isConfirmSuccessful, setIsConfirmSuccessful] = useState(false);

    useEffect(() => {
        confirmRegistration();
    });

    function confirmRegistration() {
        HttpClient.post<any>(`user-access/user-registrations/${registrationId}/confirm`, "")
            .then(() => setIsConfirmSuccessful(true));
    }

    return (
        <main className="mx-auto flex min-h-screen w-full items-center justify-center bg-gray-900">
            {isConfirmSuccessful &&
                <div className="text-center text-white font-mono text-xl">
                    <div className="mb-3">
                        Your registration is confirmed!
                    </div>
                    <div>
                        You can now <a href="/login" className="transform text-sky-600 duration-300 hover:text-sky-800">log in</a> to the application and get started.
                    </div>
                </div>
            }
        </main>
    );
}

export default ConfirmRegistration;