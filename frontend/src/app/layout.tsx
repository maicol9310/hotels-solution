import ReduxProvider from './providers/ReduxProvider'
import PropertiesWrapper from '../components/PropertiesWrapper'

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="es">
      <body>
        <ReduxProvider>
          {}
          <PropertiesWrapper />
          {children}
        </ReduxProvider>
      </body>
    </html>
  )
}
