

using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {

        MostrarVariablesDeEntorno("PUBLIC_KEY");
        MostrarVariablesDeEntorno("APP_ID");
        MostrarVariablesDeEntorno("DISCORD_TOKEN");


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

    public static void MostrarVariablesDeEntorno(string nombreDeVariable)
    {
        var valorDeVariable = Environment.GetEnvironmentVariable(nombreDeVariable);

        if (valorDeVariable != null)
        {
            Console.WriteLine("El valor de " + nombreDeVariable + " es: " + valorDeVariable);
        }
        else
        {
            Console.WriteLine("La variable de" + nombreDeVariable + " no esta definida");
        }
    }

}