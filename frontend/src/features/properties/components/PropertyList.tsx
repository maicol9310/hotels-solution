import React from 'react'
import { Property } from '../../../domain/models'
import PropertyItem from './PropertyItem'


interface Props {
    properties: Property[]
    onShowDetails: (id: string) => void
}


const PropertyList: React.FC<Props> = ({ properties, onShowDetails }) => {
    if (!properties.length) return <p>No hay propiedades para mostrar.</p>


    return (
        <ul style={{ listStyle: 'none', padding: 0 }}>
            {properties.map((p) => (
                <li key={p.idProperty} style={{ marginBottom: 12 }}>
                    <PropertyItem property={p} onShowDetails={() => onShowDetails(p.idProperty)} />
                </li>
            ))}
        </ul>
    )
}


export default PropertyList