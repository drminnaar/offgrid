import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  reactCompiler: true,
  output: "standalone",
  images: {
    localPatterns: [
      { pathname: '/products/**' },
      { pathname: '/placeholder.png' },
    ],
  },
};

export default nextConfig;
