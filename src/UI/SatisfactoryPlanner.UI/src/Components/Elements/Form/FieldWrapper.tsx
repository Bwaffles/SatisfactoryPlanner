import * as React from "react";
import { FieldError } from "react-hook-form";

type FieldWrapperProps = {
    label?: string;
    className?: string;
    children: React.ReactNode;
    error?: FieldError | undefined;
};

export type FieldWrapperPassThroughProps = Omit<
    FieldWrapperProps,
    "className" | "children"
>;

export const FieldWrapper = (props: FieldWrapperProps) => {
    const { label, className, children, error } = props;
    return (
        <div className={className}>
            <label className={"inline-block text-gray-400 mb-2"}>{label}</label>
            <div>{children}</div>
            {error?.message && (
                <div
                    role="alert"
                    aria-label={error.message}
                    className="text-sm font-semibold text-red-500"
                >
                    {error.message}
                </div>
            )}
        </div>
    );
};
