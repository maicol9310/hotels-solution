import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  reactCompiler: true,
  reactStrictMode: true,
  // Permitir acceso desde localhost y tu IP de red local en desarrollo
  allowedDevOrigins: ['http://localhost:3000', 'http://192.168.0.102:3000'],
};

export default nextConfig;
