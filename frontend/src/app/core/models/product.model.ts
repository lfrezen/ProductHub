export interface Product {
  id: string;
  name: string;
  price: number;
  quantity: number;
  isOutOfStock: boolean;
  lastSaleDate: string | null;
}

export interface CreateProductRequest {
  name: string;
  price: number;
  quantity: number;
}

export interface UpdateProductRequest {
  name: string;
  price: number;
  quantity: number;
}

export interface RegisterSaleRequest {
  quantity: number;
}
