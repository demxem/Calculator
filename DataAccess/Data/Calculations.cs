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
        private readonly ICalculator _calculator;

        public Calculations(ISqlAccess dataAccess, ICalculator calculator)
        {
            _dataAccess = dataAccess;
            _calculator = calculator;
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
            var operationType = _calculator.OperationType(expression);
            if (operationType.Equals("Addition"))
            {
                var result = _calculator.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromResult(false);
        }

        public Task InsertSubstract(CalculationModel calculation, string expression)
        {
            var operationType = _calculator.OperationType(expression);
            if (operationType.Equals("Subtract"))
            {
                var result = _calculator.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromResult(0);
        }
        public Task InsertMultiply(CalculationModel calculation, string expression)
        {
            var operationType = _calculator.OperationType(expression);
            if (operationType.Equals("Multiply"))
            {
                var result = _calculator.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromResult(0);
        }

        public Task InsertDevide(CalculationModel calculation, string expression)
        {
            var operationType = _calculator.OperationType(expression);
            if (operationType.Equals("Divide"))
            {
                var result = _calculator.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromResult(0);
        }

        public Task InsertPower(CalculationModel calculation, string expression)
        {
            var operationType = _calculator.OperationType(expression);
            if (operationType.Equals("Power"))
            {
                var result = _calculator.Calculate(expression);
                return _dataAccess.SafeData("dbo.spCalculation_Create", new
                {
                    Type = operationType,
                    Result = result,
                    Expression = expression,
                    CreationDate = DateTime.Now
                });
            }

            return Task.FromResult(0);
        }

        public Task InsertCombine(CalculationModel calculation, string expression)
        {

            var operationType = _calculator.OperationType(expression);
            var result = _calculator.Calculate(expression);

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

