import terser from "@rollup/plugin-terser";
import typescript from "rollup-plugin-typescript2";

export default {
  input: "src/main.ts",
  output: [
    {
      file: "dist/index.js",
      format: "iife"
    },
    {
      file: "dist/index.min.js",
      format: "iife",
      plugins: [terser()]
    }
  ],
  plugins: [typescript()]
};
