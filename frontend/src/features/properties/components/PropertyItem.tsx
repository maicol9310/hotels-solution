import React from 'react'
import { Property } from '../../../domain/models'
import { useFormat } from '../../../hooks/useFormat'

interface Props {
    property: Property
    onShowDetails: () => void
}

const PropertyItem: React.FC<Props> = ({ property, onShowDetails }) => {
    const { currency } = useFormat()

    return (
        <div
            style={{
                display: 'flex',
                gap: 16,
                alignItems: 'center',
                background: '#ffffff',
                padding: 16,
                borderRadius: 12,
                boxShadow: '0 4px 10px rgba(0,0,0,0.06)',
                transition: 'transform 0.2s ease, box-shadow 0.2s ease',
                cursor: 'pointer',
            }}
            onMouseEnter={(e) => {
                e.currentTarget.style.transform = 'translateY(-3px)'
                e.currentTarget.style.boxShadow = '0 6px 14px rgba(0,0,0,0.1)'
            }}
            onMouseLeave={(e) => {
                e.currentTarget.style.transform = 'translateY(0)'
                e.currentTarget.style.boxShadow = '0 4px 10px rgba(0,0,0,0.06)'
            }}
        >

            <img
                src={property.imageFile ?? '/placeholder.png'}
                alt={property.name}
                width={140}
                height={100}
                style={{
                    objectFit: 'cover',
                    borderRadius: 10,
                    flexShrink: 0,
                    background: '#f1f5f9',
                }}
            />

            <div style={{ flex: 1 }}>
                <h3
                    style={{
                        margin: '0 0 6px 0',
                        color: '#1e293b',
                        fontSize: '1.2rem',
                        fontWeight: 600,
                    }}
                >
                    {property.name}
                </h3>
                <p style={{ margin: '4px 0', color: '#475569' }}>{property.address}</p>
                <p style={{ margin: '6px 0', fontWeight: 700, color: '#2563eb' }}>
                    {currency(property.price)}
                </p>
            </div>

            <button
                onClick={onShowDetails}
                style={{
                    background: '#2563eb',
                    color: '#fff',
                    border: 'none',
                    borderRadius: 6,
                    padding: '8px 16px',
                    cursor: 'pointer',
                    fontWeight: 500,
                    transition: 'background 0.3s ease, transform 0.1s ease',
                }}
                onMouseOver={(e) => (e.currentTarget.style.background = '#1e40af')}
                onMouseOut={(e) => (e.currentTarget.style.background = '#2563eb')}
                onMouseDown={(e) => (e.currentTarget.style.transform = 'scale(0.97)')}
                onMouseUp={(e) => (e.currentTarget.style.transform = 'scale(1)')}
            >
                Ver detalles
            </button>
        </div>
    )
}

export default PropertyItem
