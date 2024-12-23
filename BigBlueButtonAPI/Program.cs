using BigBlueButton.Net.Core.BigBlueButtonAPIClient;
using BigBlueButton.Net.Core.Requests;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// BigBlueButtonAPISettings yapýlandýrmasý
builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.Configure<BigBlueButtonAPISettings>(builder.Configuration.GetSection("BigBlueButtonAPISettings"));
builder.Services.AddScoped<BigBlueButtonAPIClient>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<BigBlueButtonAPISettings>>().Value;
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    return new BigBlueButtonAPIClient(settings, factory.CreateClient());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing middleware - Zorunlu
app.UseRouting();

// Yetkilendirme middleware'i
app.UseAuthorization();

// Rota eþleme
app.MapControllers(); // Tüm controller rotalarýný otomatik tanýmlar.

app.Run();
