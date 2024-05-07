import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import viteTsconfigPaths from "vite-tsconfig-paths";
import checker from "vite-plugin-checker";

export default defineConfig({
  base: "/",
  plugins: [react(), viteTsconfigPaths(), checker({ typescript: true })],
  server: {
    open: true,
    port: 3000,
  },
});
