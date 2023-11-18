import * as React from "react";

import { Head } from "./Head";
import PageHeader from "./PageHeader";

type ContentLayoutProps = {
    children: React.ReactNode;
    title: string;
};

export const ContentLayout = ({ children, title }: ContentLayoutProps) => {
    return (
        <>
            <Head title={title} />
            <div className="container mx-auto py-4 px-10 h-full min-h-screen">
                <PageHeader text={title} />
                {children}
            </div>
        </>
    );
};
