import React from 'react'
import { Property } from '../../../domain/models'
import { useFormat } from '../../../hooks/useFormat'

interface Props {
  property: Property | null
  onClose: () => void
  loading?: boolean
}

const PropertyDetails: React.FC<Props> = ({ property, onClose, loading = false }) => {
  const { currency, date } = useFormat()

  if (!property && !loading) return null

  return (
    <div
      style={{
        position: 'fixed',
        inset: 0,
        background: 'rgba(0,0,0,0.45)',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        zIndex: 10000,
        backdropFilter: 'blur(2px)',
        padding: '1rem',
      }}
      onClick={onClose}
    >
      <div
        style={{
          background: '#fff',
          padding: '1.25rem',
          width: '100%',
          maxWidth: 600,
          borderRadius: 12,
          position: 'relative',
          boxShadow: '0 6px 20px rgba(0,0,0,0.1)',
          overflowY: 'auto',
          maxHeight: '90vh',
          animation: 'fadeInUp 0.3s ease',
        }}
        onClick={(e) => e.stopPropagation()}
      >
        <button
          type="button"
          onClick={onClose}
          style={{
            position: 'absolute',
            top: 12,
            right: 12,
            background: '#ef4444',
            color: '#fff',
            border: 'none',
            borderRadius: '50%',
            width: 28,
            height: 28,
            cursor: 'pointer',
            fontSize: 16,
            fontWeight: 'bold',
            boxShadow: '0 2px 6px rgba(0,0,0,0.2)',
            transition: 'background 0.2s ease',
          }}
          onMouseOver={(e) => (e.currentTarget.style.background = '#dc2626')}
          onMouseOut={(e) => (e.currentTarget.style.background = '#ef4444')}
        >
          ✕
        </button>

        {loading && <p style={{ textAlign: 'center' }}>Cargando detalles…</p>}

        {!loading && property && (
          <>
            <div style={{ display: 'flex', justifyContent: 'center', marginBottom: 16 }}>
              <img
                src={property.imageFile ?? '/placeholder.png'}
                alt={property.name}
                style={{
                  width: '100%',
                  maxHeight: 220,
                  objectFit: 'cover',
                  borderRadius: 8,
                }}
              />
            </div>

            <h2
              style={{
                margin: '0 0 0.3rem 0',
                color: '#1e293b',
                fontSize: '1.4rem',
              }}
            >
              {property.name}
            </h2>
            <p style={{ margin: '0 0 0.5rem 0', color: '#475569' }}>
              📍 {property.address}
            </p>
            <p
              style={{
                margin: '0 0 0.8rem 0',
                color: '#2563eb',
                fontWeight: 600,
                fontSize: '1.1rem',
              }}
            >
              {currency(property.price)}
            </p>
            <p style={{ marginBottom: 16, color: '#64748b' }}>
              <strong>Año:</strong> {property.year}
            </p>

            <div
              style={{
                background: '#f8fafc',
                padding: '0.75rem 1rem',
                borderRadius: 8,
                display: 'flex',
                alignItems: 'center',
                gap: 12,
                marginBottom: 18,
                flexWrap: 'wrap',
              }}
            >
              <img
                src={property.owner.photo ?? '/avatar.png'}
                alt={property.owner.name}
                width={64}
                height={64}
                style={{
                  borderRadius: '50%',
                  objectFit: 'cover',
                  background: '#e2e8f0',
                }}
              />
              <div>
                <p style={{ margin: 0, fontWeight: 600, color: '#1e293b' }}>
                  {property.owner.name}
                </p>
                <p style={{ margin: 0, color: '#475569', fontSize: '0.9rem' }}>
                  {property.owner.address}
                </p>
              </div>
            </div>

            <h3 style={{ color: '#1e293b', marginBottom: 8, fontSize: '1.1rem' }}>
              Historial de ventas
            </h3>
            <div
              style={{
                display: 'flex',
                flexDirection: 'column',
                gap: 8,
              }}
            >
              {property.traces.map((t) => (
                <div
                  key={t.idPropertyTrace}
                  style={{
                    background: '#f1f5f9',
                    padding: '0.7rem 1rem',
                    borderRadius: 8,
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    fontSize: '0.9rem',
                  }}
                >
                  <div>
                    <strong>{t.name}</strong>
                    <p style={{ margin: 0, color: '#64748b', fontSize: '0.85rem' }}>
                      {date(t.dateSale)}
                    </p>
                  </div>
                  <div style={{ textAlign: 'right' }}>
                    <p style={{ margin: 0 }}>{currency(t.value)}</p>
                    <p
                      style={{
                        margin: 0,
                        color: '#64748b',
                        fontSize: '0.8rem',
                      }}
                    >
                      Tax: {currency(t.tax)}
                    </p>
                  </div>
                </div>
              ))}
            </div>
          </>
        )}
      </div>

      <style>
        {`
          @keyframes fadeInUp {
            from { transform: translateY(20px); opacity: 0; }
            to { transform: translateY(0); opacity: 1; }
          }

          @media (max-width: 768px) {
            div[style*="maxWidth: 600px"] {
              padding: 1rem;
              max-width: 95%;
            }
            h2 {
              font-size: 1.2rem !important;
            }
            img[alt][src] {
              max-height: 180px !important;
            }
          }
        `}
      </style>
    </div>
  )
}

export default PropertyDetails
