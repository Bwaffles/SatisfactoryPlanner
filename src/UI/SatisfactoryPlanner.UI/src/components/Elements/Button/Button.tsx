import * as React from "react";

import { Spinner } from "../Spinner";

const variants = {
    primary: "bg-sky-800 text-white",
    secondary:
        "bg-transparent text-sky-700 border border-sky-700 hover:bg-sky-700 hover:text-white",
};

const sizes = {
    sm: "py-2 px-2 text-sm",
    md: "py-2 px-4 text-md",
    lg: "py-2 px-4 text-lg",
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
                    "rounded disabled:opacity-70 disabled:cursor-not-allowed hover:opacity-80 " +
                    variants[variant] +
                    " " +
                    sizes[size] +
                    " " +
                    className
                }
                disabled={props.disabled || isLoading}
                {...props}
            >
                <div className="flex items-center">
                    <span className="mx-2">{props.children}</span>
                    {isLoading && (
                        <Spinner
                            size="sm"
                            className="text-current"
                        />
                    )}
                </div>
            </button>
        );
    }
);

Button.displayName = "Button";
