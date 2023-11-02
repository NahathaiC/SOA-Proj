export interface Product {
  productId: number;
  productName: string;
  quantityPerUnit: string;
  unitPrice: number;
  unitsInStock: number;
  unitsOnOrder: number;
  reorderLevel: number;
  discontinued: boolean;
  category: Category;
  supplier: Supplier;
}

export interface Category {
  categoryId: number;
  categoryName: string;
}

export interface Supplier {
  supplierId: number;
  companyName: string;
}
