export interface Owner {
    idOwner: string;
    name: string;
    address: string;
    photo?: string;
}


export interface PropertyTrace {
    idPropertyTrace: string;
    dateSale: string; 
    name: string;
    value: number;
    tax: number;
    idProperty: string;
}


export interface Property {
    idProperty: string;
    name: string;
    address: string;
    price: number;
    codeInternal: string;
    year: number;
    owner: Owner;
    imageFile?: string;
    traces: PropertyTrace[];
}