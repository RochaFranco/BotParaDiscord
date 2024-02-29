

using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        string publicKeyString = Environment.GetEnvironmentVariable("PUBLIC_KEY");
        string discodTokenString = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
        string idAppString = Environment.GetEnvironmentVariable("APP_ID");


        if (publicKeyString != null)
        {
            Console.WriteLine("El valor de PUBLIC_KEY es: " + publicKeyString);
        }
        else
        {
            Console.WriteLine("La variable de PUBLIC_KEY no esta definida");
        }

        if (idAppString != null)
        {
            Console.WriteLine("El valor de ID_APP es: " + idAppString);
        }
        else
        {
            Console.WriteLine("La variable de ID_APP no esta definida");
        }

        if (discodTokenString != null)
        {
            Console.WriteLine("El valor de DISCORD_TOKEN es: " + discodTokenString);
        }
        else
        {
            Console.WriteLine("La variable de DISCORD_TOKEN no esta definida");
        }

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}