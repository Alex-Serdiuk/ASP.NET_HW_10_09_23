using System.Xml.Linq;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Use(async (context, next) =>
{
    // Îòðèìóºìî ³íôîðìàö³þ ïðî êë³ºíòà
    context.Items["browserInfo"] = context.Request.Headers["User-Agent"].ToString();
    context.Items["clientIp"] = context.Connection.RemoteIpAddress.ToString();

    // ²íôîðìàö³ÿ ïðî ñåáå
    context.Items["name"] = "Îëåêñ³é";
    context.Items["surname"] = "Ñåðäþê";
    context.Items["group"] = "ÂÏÓ011ì";

    

    await next.Invoke();
});


app.MapGet("/", async (context) =>
{
    // Áóäóºìî HTML ñòîð³íêó
    var htmlContent = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Ìîÿ ³íôîðìàö³ÿ</title>
        </head>
        <body>
            <h1>Ìîÿ ³íôîðìàö³ÿ</h1>
            <p>²ì'ÿ: " + context.Items["name"] + @"</p>
            <p>Ïð³çâèùå: " + context.Items["surname"] + @"</p>
            <p>Ãðóïà: " + context.Items["group"] + @"</p>
            <h2>²íôîðìàö³ÿ ïðî êë³ºíòà:</h2>
            <p>Áðàóçåð: " + context.Items["browserInfo"] + @"</p>
            <p>IP êë³ºíòà: " + context.Items["clientIp"] + @"</p>
        </body>
        </html>";
    await context.Response.WriteAsync(htmlContent);
});

app.Run();
