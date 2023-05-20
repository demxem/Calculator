using DataAccess.Data;
using DataAccess.Models;
using System.Runtime.CompilerServices;

namespace CalculatorApi
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            //mapping methods
            app.MapGet("/calculation", GetHistory);
            app.MapGet("/calculation/{id}", GetCalculationById);
            app.MapPost("/calculate/", InsertCalculations);
            app.MapPut("/calculation", UpdateCalculations);
            app.MapDelete("/calculation", DeleteCalculation);
        }
            
        private static async Task<IResult> GetHistory(ICalculations data)
        {
            try
            {
                return Results.Ok(await data.GetAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetCalculationById(int id, ICalculations data)
        {
            try
            {
                var results = await data.GetCalculationById(id);
                if (results == null) return Results.NotFound();
                return Results.Ok(results);
            }

            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> InsertCalculations(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertCalculation(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateCalculations(CalculationModel calculation, ICalculations data)
        {
            try
            {
                await data.UpdateCalculation(calculation);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteCalculation(ICalculations data, int id)
        {
            try
            {
                await data.DeleteCalculation(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
