import React from "react";

interface PageHeaderProps {
  text: string;
}

const PageHeader = ({ text }: PageHeaderProps) => {
  return (
    <h1 className="scroll-m-20 text-3xl font-extrabold tracking-tight lg:text-4xl mb-6">
      {text}
    </h1>
  );
};

export default PageHeader;
