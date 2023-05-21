using DataAccess.Models;

namespace DataAccess.Data
{
    public interface ICalculations
    {
        Task DeleteCalculation(int id);
        Task<IEnumerable<CalculationModel>> GetAll();
        Task<CalculationModel?> GetCalculationById(int id);
        Task InsertAdd(CalculationModel calculation, string expression);
        Task InsertCombine(CalculationModel calculation, string expression);
        Task InsertDevide(CalculationModel calculation, string expression);
        Task InsertMultiply(CalculationModel calculation, string expression);
        Task InsertPower(CalculationModel calculation, string expression);
        Task InsertSubstract(CalculationModel calculation, string expression);
        Task InsertModulo(CalculationModel calculation, string expression);
        Task UpdateCalculation(CalculationModel calculation);
    }
}