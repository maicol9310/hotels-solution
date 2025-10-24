export function useFormat() {
    const currency = (value: number) =>
        new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', maximumFractionDigits: 0 }).format(value)


    const date = (iso?: string) => (iso ? new Date(iso).toLocaleDateString('es-CO') : '')


    return { currency, date }
}