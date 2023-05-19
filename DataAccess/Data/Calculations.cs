using DataAccess.Models;
using DataAccess.DataAccess;


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

        public Task InsertCalculation(CalculationModel calculation) =>
           _dataAccess.SafeData("dbo.spCalculation_Create", new { calculation.Type, calculation.Result, calculation.Expression, calculation.CreationDate });

        public async Task UpdateCalculation(CalculationModel calculation) =>
            await _dataAccess.SafeData("dbo.spCalculation_Update", calculation);

        public async Task DeleteCalculation(int id) =>
            await _dataAccess.SafeData("dbo.spCalculation_Delete", new { Id = id });

    }
}

