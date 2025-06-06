import { IOrderItem } from "./orderItem.model"

export interface IOrder {
  orderId : string,
  redsysOrderId : string
  clientMail : string
  totalPrice : number
  status : string
  orderDate : Date
  orderItems: IOrderItem[]
}
