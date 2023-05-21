using DataAccess.Data;
using DataAccess.Models;
using System.Runtime.CompilerServices;

namespace CalculatorApi
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            //endpoints
            app.MapGet("/history", GetHistory);
            app.MapGet("/calculation/{id}", GetCalculationById);
            app.MapPost("/add/", Add);
            app.MapPost("/substract/", Substract);
            app.MapPost("/devide/", Devide);
            app.MapPost("/multiply/", Multiply);
            app.MapPost("/power", Power);
            app.MapPost("/combinedOperation", CombineOperation);
            app.MapPost("/modulo", Modulo);
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

        private static async Task<IResult> Add(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertAdd(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Substract(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertSubstract(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Devide(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertDevide(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Multiply(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertMultiply(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Power(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertPower(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Modulo(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertModulo(calculation, expression);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> CombineOperation(CalculationModel calculation, ICalculations data, string expression)
        {
            try
            {
                await data.InsertCombine(calculation, expression);
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
