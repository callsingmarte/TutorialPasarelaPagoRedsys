using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicEcommerce.Data;
using BasicEcommerce.Models;
using BasicEcommerce.Migrations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BasicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public OrdersController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product).SingleOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderDto
            {
                OrderId = order.OrderId,
                RedsysOrderId = order.RedsysOrderId,
                ClientMail = order.ClientMail,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Subtotal = oi.Subtotal,
                    Product = oi.Product != null ? new ProductDto
                    {
                        ProductId = oi.Product.ProductId,
                        Name = oi.Product.Name,
                        Description = oi.Product.Description,
                        Stock = oi.Product.Stock,
                        Price = oi.Product.Price,
                        MainImageUrl = oi.Product.MainImageUrl 
                    } : null
                }).ToList()
            };

            return orderDto;
        }

        // GET: api/Orders/redsysOrder/5
        [HttpGet("user/{userMail}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> getOrdersByUserMail(string userMail)
        {
            if (string.IsNullOrWhiteSpace(userMail))
            {
                return BadRequest("El correo electrónico no puede estar vacío.");
            }

            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product)
                .Where(o => o.ClientMail == userMail).ToListAsync();

            if (!orders.Any()) {
                return NotFound();
            }

            var orderDtos = orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                RedsysOrderId = order.RedsysOrderId,
                ClientMail = order.ClientMail,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Subtotal = oi.Subtotal,
                    Product = oi.Product != null ? new ProductDto
                    {
                        ProductId = oi.Product.ProductId,
                        Name = oi.Product.Name,
                        Description = oi.Product.Description, // Asegúrate de mapear la descripción
                        Price = oi.Product.Price,
                        Stock = oi.Product.Stock,
                        MainImageUrl = oi.Product.MainImageUrl
                    } : null
                }).ToList()
            }).ToList();

            return Ok(orderDtos);
        }


        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
