import React from "react";
import { Helmet } from "react-helmet-async";

type HeadProps = {
    title?: string;
    description?: string;
};

export const Head = ({ title = "", description = "" }: HeadProps = {}) => {
    return (
        <Helmet
            title={title ? `${title} | Satisfactory Planner` : undefined}
            defaultTitle="Satisfactory Planner"
        >
            <meta
                name="description"
                content={description}
            />
        </Helmet>
    );
};
