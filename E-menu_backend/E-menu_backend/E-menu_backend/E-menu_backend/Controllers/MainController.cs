using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Export.PdfSimple;
using System.Text;

namespace E_menu_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        // FRX içeriğini string olarak saklayın
        private const string FrxContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Report ScriptLanguage=""CSharp"" ReportInfo.Created=""12/17/2024 13:15:52"" ReportInfo.Modified=""12/17/2024 13:20:04"" ReportInfo.CreatorVersion=""2025.1.0.0"">
  <Dictionary/>
  <ReportPage Name=""Page1"" Watermark.Font=""Arial, 60pt"">
    <ReportTitleBand Name=""ReportTitle1"" Width=""718.2"" Height=""37.8""/>
    <PageHeaderBand Name=""PageHeader1"" Top=""41.8"" Width=""718.2"" Height=""28.35""/>
    <DataBand Name=""Data1"" Top=""74.15"" Width=""718.2"" Height=""75.6"">
      <TextObject Name=""ExampleParam"" Left=""274.05"" Top=""9.45"" Width=""94.5"" Height=""18.9"" Text=""ExampleParam"" Font=""Arial, 10pt""/>
    </DataBand>
    <PageFooterBand Name=""PageFooter1"" Top=""153.75"" Width=""718.2"" Height=""18.9""/>
  </ReportPage>
</Report>";

        // GET api/main/test
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            try
            {
                // FastReport ile PDF oluştur
                using var report = new Report();

                // Şablonu doğrudan string üzerinden yükle
                report.LoadFromString(FrxContent);

                // Verilerle doldurma (sabit veri ekleme)
                report.SetParameterValue("Container1", "Bu sabit bir veridir");

                // Raporu hazırlama
                report.Prepare();

                // PDF dosyasına yazma
                using var pdfStream = new MemoryStream();
                var pdfExport = new PDFSimpleExport();
                report.Export(pdfExport, pdfStream);
                pdfStream.Position = 0;

                // PDF dosyasını tarayıcıya geri döndürme
                return File(pdfStream.ToArray(), "application/pdf", "report.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Hata oluştu: {ex.Message}" });
            }
        }
    }
}
