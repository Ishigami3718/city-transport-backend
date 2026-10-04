using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityTransport.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {

        [HttpGet("generate")]
        public IActionResult GenerateQRCode([FromQuery] string data)
        {
            throw new NotImplementedException("QR code generation is not implemented yet.");
        }

        /// <summary>
        /// Scans the provided QR code data on mobile.
        /// </summary>
        /// <param name="qrCodeData"></param>
        /// <returns></returns>
        [HttpPost("scan")]
        public IActionResult ScanQRCode([FromBody] string qrCodeData)
        {
            throw new NotImplementedException("QR code scanning is not implemented yet.");
        }
    }
}
