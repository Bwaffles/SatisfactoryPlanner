import React from "react";
import { cn } from "utils";

export const H2 = React.forwardRef<
  HTMLParagraphElement,
  React.HTMLAttributes<HTMLHeadingElement>
>(({ className, ...props }, ref) => (
  <h2
    ref={ref}
    className={cn(
      "scroll-m-20 pb-4 text-2xl font-semibold tracking-tight first:mt-0",
      className
    )}
    {...props}
  />
));
H2.displayName = "H2";
