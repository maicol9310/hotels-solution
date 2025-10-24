import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { Property } from '../../domain/models'


export const propertiesApi = createApi({
    reducerPath: 'propertiesApi',
    baseQuery: fetchBaseQuery({ baseUrl: 'https://localhost:7283' }),
    tagTypes: ['Properties'],
    endpoints: (builder) => ({
        getPublicProperties: builder.query<Property[], void>({
            query: () => ({ url: '/api/Properties/public', method: 'GET' }),
            providesTags: (result) =>
                result
                    ? [...result.map(({ idProperty }) => ({ type: 'Properties' as const, id: idProperty })), { type: 'Properties', id: 'LIST' }]
                    : [{ type: 'Properties', id: 'LIST' }],
            keepUnusedDataFor: 120, // segundos: caching
        }),
        getPropertyById: builder.query<Property | undefined, string>({
            query: (id) => ({ url: `/api/Properties/public`, method: 'GET' }),
            transformResponse: (baseQueryReturnValue: Property[], meta, arg) => baseQueryReturnValue.find(p => p.idProperty === arg),
            providesTags: (result, error, id) => [{ type: 'Properties', id }],
            keepUnusedDataFor: 120,
        }),
    }),
})


export const { useGetPublicPropertiesQuery, useGetPropertyByIdQuery } = propertiesApi