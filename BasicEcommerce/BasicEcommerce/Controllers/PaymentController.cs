using BasicEcommerce.Data;
using BasicEcommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BasicEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _configuration;

        public PaymentController(EcommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

            // Calculamos el importe en céntimos antes de crear el pedido
            decimal totalAmountDecimal = product.Price * purchaseRequest.Quantity;
            int totalAmountCents = (int)(totalAmountDecimal * 100); // Redsys espera el importe en céntimos

            // Asegúrate de que estos valores son EXACTOS de tu configuración de Redsys (entorno de pruebas).
            string merchantCode = _configuration["Redsys:MerchantCode"] ?? throw new InvalidOperationException("Redsys:MerchantCode no configurado.");
            string terminal = _configuration["Redsys:Terminal"] ?? throw new InvalidOperationException("Redsys:Terminal no configurado.");
            // ESTA ES LA CLAVE DE LA FIRMA. ¡DEBE SER LA CLAVE BASE64 REAL DE TU TPV VIRTUAL DE PRUEBAS!
            // No uses "TU_CLAVE_DE_FIRMA_SECRETA". La clave de prueba común para HMAC SHA256 es como "sq7HjrUoSSC6opPkkQ9gKxgyuNEzTknz" (ejemplo Base64)
            string signatureKeyBase64 = _configuration["Redsys:SignatureKey"] ?? throw new InvalidOperationException("Redsys:SignatureKey no configurada.");
            string redsysTpvsUrl = _configuration["Redsys:TpvsUrl"] ?? throw new InvalidOperationException("Redsys:TpvsUrl no configurada.");

            // --- Creación del Pedido y OrderItem ---
            // Aquí es donde se establece el OrderId que luego usaremos para Redsys
            Order order = new Order()
            {
                ClientMail = purchaseRequest.UserMail,
                OrderDate = DateTime.UtcNow,
                Status = "PendingPayment", 
                TotalPrice = totalAmountDecimal 
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(); 

            // Añadir elemento del Pedido
            OrderItem orderItem = new OrderItem()
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = purchaseRequest.Quantity,
                Subtotal = product.Price * purchaseRequest.Quantity
            };

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            // --- Parámetros de Redsys (¡Otro punto crítico a revisar!) ---
            // El ID del pedido para Redsys debe ser una cadena alfanumérica y a menudo sin guiones.
            // Guid.ToString("N") quita los guiones.
            string redsysOrderId = order.OrderId.ToString("N");
            // Algunos TPVs virtuales antiguos de Redsys requerían que el OrderId tuviera un mínimo de caracteres
            // o un formato específico (ej. los primeros 4 dígitos deben ser numéricos).
            // Si el problema persiste, verifica la documentación específica de tu TPV.

            var redsysMerchantParameters = new
            {
                DS_MERCHANT_AMOUNT = totalAmountCents.ToString(), // Importe en céntimos (SIEMPRE como STRING)
                DS_MERCHANT_ORDER = redsysOrderId, // ID del pedido SIN GUIONES
                DS_MERCHANT_MERCHANTCODE = merchantCode,
                DS_MERCHANT_CURRENCY = "978", // EUR
                DS_MERCHANT_TRANSACTIONTYPE = "0", // 0 para autorización/venta estándar
                DS_MERCHANT_TERMINAL = terminal,
                // Las URLs OK/KO también se pueden enviar para que Redsys redirija allí después del pago
                DS_MERCHANT_URLOK = $"{_configuration["Redsys:UrlOk"]}?orderId={order.OrderId}",
                DS_MERCHANT_URLKO = $"{_configuration["Redsys:UrlKo"]}?orderId={order.OrderId}",
                DS_MERCHANT_CONSUMERLANGUAGE = "001", // 001 para Español
            };

            string jsonParams = JsonSerializer.Serialize(redsysMerchantParameters);

            string encodedParams = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonParams));

            // --- Generación de la Firma HMAC-SHA256 (El algoritmo parece correcto) ---
            // La clave de firma (signatureKeyBase64) DEBE ser la cadena Base64 que te da Redsys.
            // 'Convert.FromBase64String' la decodifica a bytes, que es lo que HMACSHA256 espera.
            byte[] keyBytes = Convert.FromBase64String(signatureKeyBase64);
            byte[] dataBytes = Encoding.UTF8.GetBytes(encodedParams);

            byte[] hash;
            using (var hmac = new HMACSHA256(keyBytes))
            {
                hash = hmac.ComputeHash(dataBytes);
            }
            string signature = Convert.ToBase64String(hash); // La firma final debe ser Base64 también

            // --- Devolver los datos a Angular ---
            var response = new RedsysPaymentDataDto
            {
                Ds_SignatureVersion = "HMAC_SHA256_V1", // Confirma que tu TPV virtual usa esta versión de firma
                Ds_MerchantParameters = encodedParams,
                Ds_Signature = signature,
                RedsysTpvsUrl = redsysTpvsUrl
            };

            return Ok(response);
        }
    }
}
