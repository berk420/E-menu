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
                using var report = new Report();
              

                return $"Rapor başarıyla oluşturuldu:";
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }
    }
}
