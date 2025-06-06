import { IProduct } from "./product.model"

export interface IOrderItem {
   orderItemId : string
   orderId : string
   productId : string
   product: IProduct
   quantity : number
   subtotal : number
}
