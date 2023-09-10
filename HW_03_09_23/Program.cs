using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Use(async (context, next) =>
{
    // Отримуємо інформацію про клієнта
    context.Items["browserInfo"] = context.Request.Headers["User-Agent"].ToString();
    context.Items["clientIp"] = context.Connection.RemoteIpAddress.ToString();

    // Інформація про себе
    context.Items["name"] = "Олексій";
    context.Items["surname"] = "Сердюк";
    context.Items["group"] = "ВПУ011м";



    await next.Invoke();
});


app.MapGet("/", async (context) =>
{
    // Будуємо HTML сторінку
    var htmlContent = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Моя інформація</title>
        </head>
        <body>
            <h1>Моя інформація</h1>
            <p>Ім'я: " + context.Items["name"] + @"</p>
            <p>Прізвище: " + context.Items["surname"] + @"</p>
            <p>Група: " + context.Items["group"] + @"</p>
            <h2>Інформація про клієнта:</h2>
            <p>Браузер: " + context.Items["browserInfo"] + @"</p>
            <p>IP клієнта: " + context.Items["clientIp"] + @"</p>
        </body>
        </html>";
    await context.Response.WriteAsync(htmlContent);
});

app.Run();
