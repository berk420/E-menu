public class Program
{
    public static void Main(string[] args)
    {
        // Uygulamanýn baþlatýlmasý
        var builder = WebApplication.CreateBuilder(args);

        // Servisleri ekle
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost3000", policy =>
            {
                policy.AllowAnyOrigin()   // Tüm IP'lere izin verir.
                      .AllowAnyMethod()   // Tüm HTTP yöntemlerine (GET, POST, vb.) izin verir.
                      .AllowAnyHeader();  // Tüm baþlýklara izin verir.
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // WebApplication oluþturuluyor
        var app = builder.Build();

        // Cors middleware
        app.UseCors("AllowLocalhost3000");

        // Swagger yalnýzca geliþtirme ortamýnda
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // HTTPS yönlendirmesi
        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Controller'larý baðlama
        app.MapControllers();

        // Ana sayfa map'lemesi
        app.MapGet("/", () => "Hello World!");

        // Uygulamanýn çalýþtýrýlmasý
        app.Run();
    }
}
