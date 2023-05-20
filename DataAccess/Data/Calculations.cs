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

        public Task InsertCalculation(CalculationModel calculation, string expression)
        {
            var result = _calculator.Calculate(expression);
            var type = _calculator.OperationType(expression);

            return _dataAccess.SafeData("dbo.spCalculation_Create", new { Type = type, Result = result, calculation.Expression, calculation.CreationDate });
        }

        public async Task UpdateCalculation(CalculationModel calculation) =>
            await _dataAccess.SafeData("dbo.spCalculation_Update", calculation);

        public async Task DeleteCalculation(int id) =>
            await _dataAccess.SafeData("dbo.spCalculation_Delete", new { Id = id });

    }
}

