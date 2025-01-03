using Microsoft.AspNetCore.Mvc;
using FastReport;

namespace E_menu_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        // GET api/main/test
        [HttpGet("test")]
        public string GetTest()
        {
            try
            {
                // FastReport.Report nesnesi oluşturma
                using Report report = new Report();

                // Basit bir sayfa ve içerik ekleme
                report.Pages.Add(new FastReport.ReportPage());

                var page = report.Pages[0] as FastReport.ReportPage;
                if (page != null)
                {
                    page.CreateUniqueName();

                    // Sayfa boyutunu ayarla
                    page.PaperWidth = 210; // A4 genişliği mm cinsinden
                    page.PaperHeight = 297; // A4 yüksekliği mm cinsinden

                    // Basit bir TextObject ekleme
                    var text = new FastReport.TextObject
                    {
                        Left = 10,
                        Top = 10,
                        Width = 200,
                        Height = 30,
                        Text = "Bu bir test PDF dosyasıdır.",
                        Font = new System.Drawing.Font("Arial", 14),
                    };
                    text.CreateUniqueName();
                    page.ReportTitle = new FastReport.ReportTitleBand { Height = 50 };
                    page.ReportTitle.Objects.Add(text);
                }

                // Raporu hazırlama
                report.Prepare();

                // İşlem başarılıysa başarı mesajını döndür
                return "PDF başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını döndür
                return $"PDF oluşturulurken bir hata oluştu: {ex.Message}";
            }
        }
    }
}
