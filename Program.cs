var builder = WebApplication.CreateBuilder(args);

// 1. Servisleri Konteynera Ekleme (Add Services to the Container)
// Controller'ları projeye tanıtıyoruz.
builder.Services.AddControllers();

// Swagger/OpenAPI yapılandırması
// Bu iki satır Swagger dökümantasyonunu oluşturmak için gereklidir.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. HTTP İstek Hattını Yapılandırma (Configure the HTTP Request Pipeline)

// Eğer geliştirme ortamındaysak (Development), Swagger arayüzünü göster.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Tarayıcıda test edebilmen için gereken arayüz
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Controller'ları eşleştir (URL rotalarını ayarla)
app.MapControllers();

app.Run();