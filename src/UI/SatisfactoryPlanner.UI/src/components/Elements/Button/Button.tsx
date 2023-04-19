import * as React from "react";

import { Spinner } from "../Spinner";

const variants = {
    primary: "bg-sky-800 text-white",
};

const sizes = {
    sm: "py-2 px-4 text-sm",
    md: "p-4 text-md",
    lg: "py-4 px-8 text-lg",
};

export type ButtonProps = React.ButtonHTMLAttributes<HTMLButtonElement> & {
    variant?: keyof typeof variants;
    size?: keyof typeof sizes;
    isLoading?: boolean;
};

export const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
    (
        {
            type = "button",
            className = "",
            variant = "primary",
            size = "md",
            isLoading = false,
            ...props
        },
        ref
    ) => {
        return (
            <button
                ref={ref}
                type={type}
                className={
                    "flex justify-center items-center rounded disabled:opacity-70 disabled:cursor-not-allowed hover:opacity-80 " +
                    variants[variant] +
                    " " +
                    sizes[size] +
                    " " +
                    className
                }
                disabled={props.disabled || isLoading}
                {...props}
            >
                <span className="mx-2">{props.children}</span>{" "}
                {isLoading && (
                    <Spinner
                        size="sm"
                        className="text-current"
                    />
                )}
            </button>
        );
    }
);

Button.displayName = "Button";
