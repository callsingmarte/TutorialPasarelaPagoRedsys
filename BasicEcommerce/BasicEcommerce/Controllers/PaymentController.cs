using BasicEcommerce.Data;
using BasicEcommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
// ¡Asegúrate de que estos 'using' estén al inicio de tu archivo de controlador!
using RedsysTPV; // Para RedsysParameters
using RedsysTPV.Models; // Para RedsysParameters
using RedsysTPV.Enums;

namespace BasicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _configuration;

        // Declara las instancias de los managers de Redsys
        private readonly MerchantParametersManager _merchantParametersManager;
        private readonly SignatureManager _signatureManager;

        public PaymentController(EcommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            // Inicializa las instancias de los managers en el constructor
            // Por lo que vimos en el explorador de objetos, no requieren parámetros en su constructor.
            _merchantParametersManager = new MerchantParametersManager();
            _signatureManager = new SignatureManager();
        }


        // POST: api/Payment/GenerateRedsysData
        // Este método recibe la solicitud de compra y genera los datos para Redsys
        [HttpPost("GenerateRedsysData")]
        public async Task<ActionResult<RedsysPaymentDataDto>> GenerateRedsysData([FromBody] PurchaseRequestDto purchaseRequest)
        {
            if (purchaseRequest == null || purchaseRequest.ProductId == Guid.Empty || purchaseRequest.Quantity <= 0)
            {
                return BadRequest("La solicitud de compra no es válida.");
            }

            var product = await _context.Products.FindAsync(purchaseRequest.ProductId);
            if (product == null)
            {
                return NotFound("Producto no encontrado.");
            }

            decimal totalAmountDecimal = product.Price * purchaseRequest.Quantity;

            string merchantCode = _configuration["Redsys:MerchantCode"] ?? throw new InvalidOperationException("Redsys:MerchantCode no configurado.");
            string terminal = _configuration["Redsys:Terminal"] ?? throw new InvalidOperationException("Redsys:Terminal no configurado.");
            string signatureKeyBase64 = _configuration["Redsys:SignatureKey"] ?? throw new InvalidOperationException("Redsys:SignatureKey no configurada.");
            string redsysTpvsUrl = _configuration["Redsys:TpvsUrl"] ?? throw new InvalidOperationException("Redsys:TpvsUrl no configurada.");

            // --- Creación del Pedido y OrderItem ---
            Order order = new Order()
            {
                ClientMail = purchaseRequest.UserMail,
                OrderDate = DateTime.UtcNow,
                Status = "PendingPayment",
                TotalPrice = totalAmountDecimal
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            OrderItem orderItem = new OrderItem()
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = purchaseRequest.Quantity,
                Subtotal = product.Price * purchaseRequest.Quantity
            };

            string redsysOrderId = order.OrderId.ToString("N").Substring(0, 12);
            order.RedsysOrderId = redsysOrderId;

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            // --- Generación de parámetros y firma usando RedsysTpv.NetStandard ---
            string dsMerchantMerchantUrl = _configuration["Redsys:MerchantNotificationUrl"] ?? string.Empty;

            // 1. Mapear tus datos a la clase PaymentRequest de la librería RedsysTPV.Models
            var paymentRequest = new PaymentRequest(
                merchantCode,
                terminal,
                TransactionType.Authorization,
                totalAmountDecimal,
                Currency.EUR,
                redsysOrderId,
                dsMerchantMerchantUrl,
                $"{_configuration["Redsys:UrlOk"]}?orderId={order.OrderId}",
                $"{_configuration["Redsys:UrlKo"]}?orderId={order.OrderId}",
                Language.Spanish
            );

            // 2. Obtener el parámetro Ds_MerchantParameters cifrado/codificado
            // Se llama al método de instancia GetMerchantParameters()
            string dsMerchantParameters = _merchantParametersManager.GetMerchantParameters(paymentRequest);

            // 3. Obtener el parámetro Ds_Signature
            string dsSignature = _signatureManager.GetSignature(dsMerchantParameters, redsysOrderId, signatureKeyBase64);

            // --- Devolver los datos a Angular ---
            // Estos son los datos que tu frontend Angular necesita para construir el formulario POST
            var response = new RedsysPaymentDataDto
            {
                Ds_SignatureVersion = "HMAC_SHA256_V1", // Versión de la firma (fija para esta integración)
                Ds_MerchantParameters = dsMerchantParameters, // Parámetros de comercio codificados
                Ds_Signature = dsSignature, // Firma calculada
                RedsysTpvsUrl = redsysTpvsUrl // URL del TPV de Redsys
            };

            return Ok(response);
        }
    }
}
