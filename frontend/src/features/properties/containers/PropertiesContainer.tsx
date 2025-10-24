'use client'

import React from 'react'
import { skipToken } from '@reduxjs/toolkit/query'
import {
    useGetPublicPropertiesQuery,
    useGetPropertyByIdQuery,
} from '../../../store/api/propertiesApi'
import PropertyList from '../components/PropertyList'
import PropertyDetails from '../components/PropertyDetails'
import { Property } from '../../../domain/models'

const PropertiesContainer: React.FC = () => {
    const {
        data: properties = [],
        isLoading,
        isFetching,
        isError,
        refetch,
    } = useGetPublicPropertiesQuery()

    const [selectedId, setSelectedId] = React.useState<string | null>(null)
    const [showModal, setShowModal] = React.useState(false)
    const [searchName, setSearchName] = React.useState('')
    const [searchAddress, setSearchAddress] = React.useState('')
    const [minPrice, setMinPrice] = React.useState('')
    const [maxPrice, setMaxPrice] = React.useState('')

    const { data: selectedProperty, isLoading: isLoadingProperty } = useGetPropertyByIdQuery(
        selectedId ? selectedId : skipToken
    )

    const onShowDetails = (id: string) => {
        setSelectedId(id)
        setShowModal(true)
    }

    const onClose = () => {
        setShowModal(false)
        setSelectedId(null)
    }

    const filteredProperties = React.useMemo(() => {
        return (properties as Property[]).filter((p) => {
            const nameMatch = p.name.toLowerCase().includes(searchName.toLowerCase())
            const addressMatch = p.address.toLowerCase().includes(searchAddress.toLowerCase())
            const priceMatch =
                (!minPrice || p.price >= parseFloat(minPrice)) &&
                (!maxPrice || p.price <= parseFloat(maxPrice))
            return nameMatch && addressMatch && priceMatch
        })
    }, [properties, searchName, searchAddress, minPrice, maxPrice])

    return (
        <section
            style={{
                padding: '2rem',
                fontFamily: 'system-ui, sans-serif',
                background: '#f7f9fb',
                minHeight: '100vh',
            }}
        >

            <header
                style={{
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    background: '#ffffff',
                    padding: '1rem 1.5rem',
                    borderRadius: '10px',
                    boxShadow: '0 2px 8px rgba(0,0,0,0.08)',
                    marginBottom: '2rem',
                }}
            >
                <h1 style={{ fontSize: '1.8rem', color: '#1e293b', margin: 0 }}>🏘️ Propiedades</h1>
                <div>
                    <button
                        onClick={() => refetch()}
                        style={{
                            background: '#2563eb',
                            color: '#fff',
                            border: 'none',
                            padding: '0.6rem 1.2rem',
                            borderRadius: 6,
                            cursor: 'pointer',
                            fontWeight: 500,
                            transition: 'background 0.3s ease',
                        }}
                        onMouseOver={(e) => (e.currentTarget.style.background = '#1e40af')}
                        onMouseOut={(e) => (e.currentTarget.style.background = '#2563eb')}
                    >
                        Refrescar
                    </button>
                    {isFetching && (
                        <span style={{ marginLeft: 12, color: '#475569' }}>Actualizando...</span>
                    )}
                </div>
            </header>

            <div
                style={{
                    display: 'flex',
                    flexWrap: 'wrap',
                    gap: 10,
                    background: '#ffffff',
                    padding: 16,
                    borderRadius: 10,
                    marginBottom: 24,
                    boxShadow: '0 2px 6px rgba(0,0,0,0.06)',
                }}
            >
                <input
                    type="text"
                    placeholder="Buscar por nombre"
                    value={searchName}
                    onChange={(e) => setSearchName(e.target.value)}
                    style={filterInput}
                />
                <input
                    type="text"
                    placeholder="Buscar por dirección"
                    value={searchAddress}
                    onChange={(e) => setSearchAddress(e.target.value)}
                    style={filterInput}
                />
                <input
                    type="number"
                    placeholder="Precio mínimo"
                    value={minPrice}
                    onChange={(e) => setMinPrice(e.target.value)}
                    style={filterInput}
                />
                <input
                    type="number"
                    placeholder="Precio máximo"
                    value={maxPrice}
                    onChange={(e) => setMaxPrice(e.target.value)}
                    style={filterInput}
                />
                <button
                    type="button"
                    onClick={() => {
                        setSearchName('')
                        setSearchAddress('')
                        setMinPrice('')
                        setMaxPrice('')
                    }}
                    style={{
                        background: '#e2e8f0',
                        border: 'none',
                        borderRadius: 6,
                        padding: '0.6rem 1rem',
                        cursor: 'pointer',
                        fontWeight: 500,
                        color: '#1e293b',
                        transition: 'background 0.3s ease',
                    }}
                    onMouseOver={(e) => (e.currentTarget.style.background = '#cbd5e1')}
                    onMouseOut={(e) => (e.currentTarget.style.background = '#e2e8f0')}
                >
                    Limpiar filtros
                </button>
            </div>

            {isLoading && <p style={{ color: '#475569' }}>Cargando propiedades…</p>}
            {isError && <p style={{ color: 'red' }}>Error al cargar las propiedades.</p>}

            {!isLoading && !isError && (
                <PropertyList
                    properties={filteredProperties}
                    onShowDetails={onShowDetails}
                />
            )}

            {showModal && (
                <PropertyDetails
                    property={selectedProperty ?? null}
                    onClose={onClose}
                    loading={isLoadingProperty}
                />
            )}
        </section>
    )
}

const filterInput: React.CSSProperties = {
    flex: '1 1 180px',
    padding: '0.6rem 0.8rem',
    border: '1px solid #cbd5e1',
    borderRadius: 6,
    fontSize: '0.95rem',
    outline: 'none',
    transition: 'border 0.3s ease, box-shadow 0.3s ease',
    background: '#f9fafb',
}

export default PropertiesContainer
