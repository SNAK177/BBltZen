import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:5100', // Porta tipica del backend C#
        changeOrigin: true
      }
    }
  },
  plugins: [react()]
})
