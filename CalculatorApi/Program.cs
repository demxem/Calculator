
namespace CalculatorApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = AppBuilder.BuildServices(args);
            app.ConfigureApi();
            app.Run();
        }
    }
}