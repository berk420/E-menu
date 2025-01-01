using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Export.PdfSimple;
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
            // Rapor şablonunun yolu
            
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "template.frx");

            if (!System.IO.File.Exists(reportPath))
            {
                return NotFound("Rapor şablonu bulunamadı.");
            }

            // FastReport ile PDF oluştur
            using var report = new Report();
            // Şablonu yükle
            report.Load(reportPath);

            // Verilerle doldurma (sabit veri ekleme)
            report.SetParameterValue("Container1", "Bu sabit bir veridir");

            // Raporu hazırlama
            report.Prepare();

            // PDF dosyasına yazma
            using var pdfStream = new MemoryStream();
            var pdfExport = new PDFSimpleExport();
            report.Export(pdfExport, pdfStream);
            pdfStream.Position = 0;

            // PDF dosyasını geri döndür
            return File(pdfStream.ToArray(), "application/pdf", "testpdf.pdf");
        }
    }
}
