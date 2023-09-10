using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Use(async (context, next) =>
{
    // �������� ���������� ��� �볺���
    context.Items["browserInfo"] = context.Request.Headers["User-Agent"].ToString();
    context.Items["clientIp"] = context.Connection.RemoteIpAddress.ToString();

    // ���������� ��� ����
    context.Items["name"] = "������";
    context.Items["surname"] = "������";
    context.Items["group"] = "���011�";

    

    await next.Invoke();
});


app.MapGet("/", async (context) =>
{
    // ������ HTML �������
    var htmlContent = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>��� ����������</title>
        </head>
        <body>
            <h1>��� ����������</h1>
            <p>��'�: " + context.Items["name"] + @"</p>
            <p>�������: " + context.Items["surname"] + @"</p>
            <p>�����: " + context.Items["group"] + @"</p>
            <h2>���������� ��� �볺���:</h2>
            <p>�������: " + context.Items["browserInfo"] + @"</p>
            <p>IP �볺���: " + context.Items["clientIp"] + @"</p>
        </body>
        </html>";
    await context.Response.WriteAsync(htmlContent);
});

app.Run();
