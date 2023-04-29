import * as React from "react";

type FieldWrapperProps = {
    label?: string;
    className?: string;
    children: React.ReactNode;
};

export type FieldWrapperPassThroughProps = Omit<
    FieldWrapperProps,
    "className" | "children"
>;

export const FieldWrapper = (props: FieldWrapperProps) => {
    const { label, className, children } = props;
    return (
        <div className={className}>
            <label className={"inline-block text-gray-400 mb-2"}>{label}</label>
            <div>{children}</div>
        </div>
    );
};
