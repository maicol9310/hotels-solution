'use client'

import dynamic from 'next/dynamic'

const PropertiesContainer = dynamic(
  () =>
    import('../features/properties/containers/PropertiesContainer').then(
      (mod) => mod.default
    ),
  {
    ssr: false,
    loading: () => <p>Cargando propiedades...</p>,
  }
)

export default function PropertiesWrapper() {
  return <PropertiesContainer />
}
