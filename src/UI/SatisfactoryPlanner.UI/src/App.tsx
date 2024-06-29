import * as React from "react";

import { AppProvider } from "./providers/app-provider";
import { AppRoutes } from "./routes";

const App = () => {
  return (
    <AppProvider>
      <AppRoutes />
    </AppProvider>
  );
};

export default App;
