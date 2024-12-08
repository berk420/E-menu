using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace E_menu_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        // GET api/main/test
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            // PDF dosyasının yolu
            var pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), "testpdf.pdf");

            // Dosya var mı kontrolü
            if (!System.IO.File.Exists(pdfFilePath))
            {
                return NotFound("PDF dosyası bulunamadı.");
            }

            // PDF dosyasını oku ve gönder
            var pdfFile = System.IO.File.ReadAllBytes(pdfFilePath);
            return File(pdfFile, "application/pdf", "testpdf.pdf");
        }
    }
}
