using DataAccess.Models;
using DataAccess.DataAccess;
using System.Runtime.CompilerServices;
using Core.Services;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace DataAccess.Data
{
    public class Calculations : ICalculations
    {
        private readonly ISqlAccess _dataAccess;


        public Calculations(ISqlAccess dataAccess)
        {
            _dataAccess = dataAccess;

        }

        public async Task<IEnumerable<CalculationModel>> GetAll() =>
            await _dataAccess.LoadData<CalculationModel, dynamic>("dbo.spCalculation_GetAll", new { });

        public async Task<CalculationModel?> GetCalculationById(int id)
        {
            var result = await _dataAccess.LoadData<CalculationModel, dynamic>("spCalculation_GetById", new { Id = id });

            return result.FirstOrDefault();
        }

        public Task InsertAdd(CalculationModel calculation, string expression)
        {
            var operationType = ExpressionParser.OperationType(expression);
            if (operationType.Equals("Addition"))
            {
                var result = ExpressionParser.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromException(new Exception("Only add operation is supported"));
        }

        public Task InsertSubstract(CalculationModel calculation, string expression)
        {
            var operationType = ExpressionParser.OperationType(expression);
            if (operationType.Equals("Subtract"))
            {
                var result = ExpressionParser.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromException(new Exception("Only subtract operation is supported"));
        }
        public Task InsertMultiply(CalculationModel calculation, string expression)
        {
            var operationType = ExpressionParser.OperationType(expression);
            if (operationType.Equals("Multiply"))
            {
                var result = ExpressionParser.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromException(new Exception("Only multiply operation is supported"));
        }

        public Task InsertDevide(CalculationModel calculation, string expression)
        {
            var operationType = ExpressionParser.OperationType(expression);
            if (operationType.Equals("Divide"))
            {
                var result = ExpressionParser.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromException(new Exception("Only divide operration is supported"));
        }

        public Task InsertPower(CalculationModel calculation, string expression)
        {
            var operationType = ExpressionParser.OperationType(expression);
            if (operationType.Equals("Power"))
            {
                var result = ExpressionParser.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromException(new Exception("Only power operation is supported"));
        }

        public Task InsertCombine(CalculationModel calculation, string expression)
        {

            var operationType = ExpressionParser.OperationType(expression);
            var result = ExpressionParser.Calculate(expression);

            return _dataAccess.SafeData("dbo.spCalculation_Create", new
            {
                Type = operationType,
                Result = result,
                Expression = expression,
                CreationDate = DateTime.Now
            });
        }


        public async Task UpdateCalculation(CalculationModel calculation) =>
            await _dataAccess.SafeData("dbo.spCalculation_Update", calculation);

        public async Task DeleteCalculation(int id) =>
            await _dataAccess.SafeData("dbo.spCalculation_Delete", new { Id = id });

    }
}

