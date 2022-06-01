import * as React from "react";

type InputProps = {
    name?: string | undefined;
    autoComplete?: string | undefined;
    placeholder?: string | undefined;
    type?: React.HTMLInputTypeAttribute | undefined;
    value?: string | ReadonlyArray<string> | number | undefined;
    onChange?: React.ChangeEventHandler<HTMLInputElement> | undefined;
}

const Input = ({ ...otherProps }: InputProps) => {

    return (
        <input
            className="w-full block p-2.5 rounded-md bg-gray-700 placeholder-gray-400 text-white focus:outline focus:outline-offset-2 focus:outline-2 focus:outline-sky-700"
            {...otherProps}
        />
    );
};

export default Input;