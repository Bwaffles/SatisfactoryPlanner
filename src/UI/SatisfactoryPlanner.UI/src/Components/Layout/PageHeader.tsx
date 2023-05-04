import React from 'react';

interface PageHeaderProps {
    text: string
}

const PageHeader = ({ text }: PageHeaderProps) => {
  return (
      <h1 className="text-3xl font-bold underline decoration-sky-800 mb-6">{text}</h1>
  );
}

export default PageHeader;