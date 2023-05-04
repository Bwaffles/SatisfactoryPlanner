import * as React from "react";
import { FieldError } from "react-hook-form";

type FieldWrapperProps = {
    className?: string;
    label?: string;
    /** Specifies which form element the label is bound to. */
    labelHtmlFor?: string | undefined;
    children: React.ReactNode;
    error?: FieldError | undefined;
};

export type FieldWrapperPassThroughProps = Omit<
    FieldWrapperProps,
    "className" | "children"
>;

export const FieldWrapper = (props: FieldWrapperProps) => {
    const { className, label, labelHtmlFor, children, error } = props;
    return (
        <div className={className}>
            <label
                className="inline-block text-gray-400 mb-2"
                htmlFor={labelHtmlFor}
            >
                {label}
            </label>
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
