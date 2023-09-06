import { Product } from "./product";
import { ProductPrice } from "./productPrice";

export interface CatalogueProduct {

    productId: number;
    description: string;
    product: Product;
    productPrice: ProductPrice;
    instock: number;
}