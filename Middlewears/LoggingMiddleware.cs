using System;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;


    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Request.EnableBuffering();
        
        var met = httpContext.Requset.Method().ToString();
        if (met.IsNullOrEmpty(met))
        {
            met = "brak_informacji";
        }

        var pat = httpContext.Requset.Path().ToString();
        if (pat.IsNullOrEmpty(met))
        {
            pat = "brak_informacji";
        }

        var qstr = httpContext.Requset.QueryString().ToString();
        if (qstr.IsNullOrEmpty(met))
        {
            qstr = "brak_informacji";
        }

        var bod = string.Empty();

        using (var reader = new StreamReader(httpContext.Requset.Body, Encoding.UTF8, true, 1024, true))
        {
            bod = await reader.ReadToEndAsync();
        }
        if (bod.IsNullOrEmpty(met))
        {
            bod = "brak_informacji";
        }


// zapis do pliku
        string[] tab =
        {
            "Metoda HTTP -> " + met,
            "Ścieżka żądania ->" + pat,
            "Ciało żądania -> " + bod,
            "Query string -> " + qstr      
        };

        string filePath = @"C:\Users\Master\Desktop\C#\cw6\Middlewears\requestsLog.txt";
        File.AppendAllLines(filePath, tab);


        httpContext.Request.Body.Seek(0, SeekOrgin.Begin);
        await _next(httpContext);
    }
}