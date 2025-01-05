public class Program
{
    public static void Main(string[] args)
    {
        // Uygulaman�n ba�lat�lmas�
        var builder = WebApplication.CreateBuilder(args);

        // Servisleri ekle
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost3000", policy =>
            {
                policy.AllowAnyOrigin()   // T�m IP'lere izin verir.
                      .AllowAnyMethod()   // T�m HTTP y�ntemlerine (GET, POST, vb.) izin verir.
                      .AllowAnyHeader();  // T�m ba�l�klara izin verir.
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // WebApplication olu�turuluyor
        var app = builder.Build();

        // Cors middleware
        app.UseCors("AllowLocalhost3000");

        // Swagger yaln�zca geli�tirme ortam�nda
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // HTTPS y�nlendirmesi
        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Controller'lar� ba�lama
        app.MapControllers();

        // Ana sayfa map'lemesi
        app.MapGet("/", () => "Hello World!");

        // Uygulaman�n �al��t�r�lmas�
        app.Run();
    }
}
