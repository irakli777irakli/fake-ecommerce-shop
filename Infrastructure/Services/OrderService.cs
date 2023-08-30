using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    // order collection of orderd items(product in this case)
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService
        ( 
         IUnitOfWork unitOfWork,
         IBasketRepository basketRepo
        )
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
            
            
        }
        

        public async Task<Order> CreateOrderAsync(
            string buyerEmail, int deliveryMethodId,
            string basketId, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach(var item in basket.Items) 
            {   
                // get product info from products db, basket not trusted
                var productItem = await 
                    _unitOfWork.Repository<Product>()
                        .GetByIdAsync(item.Id);
                // snapshot info of ordered product
                var itemOrdered = new ProductItemOrdered(
                    productItem.Id,productItem.Name,productItem.PictureUrl
                );
                // actual Ordered item => e.g single product
                var orderItem = new OrderItem(itemOrdered,productItem.Price,item.Quantity);
                
                items.Add(orderItem);
            }
            // delivery method created
            var deliveryMethod = await
             _unitOfWork.Repository<DeliveryMethod>()
                .GetByIdAsync(deliveryMethodId);
            // calculate subtotal, of all products in order
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            
            // create order, consists of orderItems
            // orderItems consists `orders`, orders consists of `product` snapshots 
            var order = new Order(items,buyerEmail,shippingAddress,deliveryMethod,subtotal);


            // add to db
            _unitOfWork.Repository<Order>()
                .Add(order);
            
            // save to db
            var result = await _unitOfWork.Complete();

            if(result <= 0) return null;

            await _basketRepo.DeleteBasketAsync(basketId);

            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>()
                .ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);

            return await _unitOfWork.Repository<Order>()
                .GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            // retrieves all Orders made by User
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
    
            return await _unitOfWork.Repository<Order>()
                .ListAsync(spec);
        }
    }
}