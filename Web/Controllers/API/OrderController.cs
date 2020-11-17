using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    using System.Web.Mvc;
    using Infrastructure;
    using Models;

    public class OrderController : ApiController
    {
        //[HttpGet]
        //public IEnumerable<Order> GetOrders(int id = 1)
        //{
        //    var data = new OrderService();

        //    return data.GetOrdersForCompany(id);
        //}

        //Following is from Sachin
        [HttpGet]
        public IEnumerable<OrderViewModel> GetOrders(int id = 1)
        {
            var data = new OrderService();

            var orderDetails = data.GetCompanyOrders(id);
            var ordersByGroup = orderDetails.GroupBy(item => item.OrderDescription)
                  .Select(group => new OrderViewModel
                  {
                      OrderDescription = orderDetails.First().OrderDescription,
                      OrderTotal = group.Sum(m => m.ProductPrice * m.ProductQuantity),
                      Products = group.ToList().Select(m => new ProductDetails
                      {
                          ProductName = m.ProductName,
                          ProductPrice = m.ProductPrice,
                          ProductQuantity = m.ProductQuantity
                      }).ToList()
                  })
                  .ToList();
            return ordersByGroup;
        }
    }
}
