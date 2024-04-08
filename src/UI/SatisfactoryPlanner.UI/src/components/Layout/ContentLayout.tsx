import * as React from "react";

import { Head } from "./Head";
import PageHeader from "./PageHeader";

type ContentLayoutProps = {
  children: React.ReactNode;
  title: string;
  description?: string;
};

export const ContentLayout = ({
  children,
  title,
  description,
}: ContentLayoutProps) => {
  return (
    <>
      <Head title={title} />
      <div className="container py-4">
        <PageHeader text={title} />
        {description && (
          <p className="text-muted-foreground mb-6">{description}</p>
        )}
        {children}
      </div>
    </>
  );
};
