using DataAccess.Models;

namespace DataAccess.Data
{
    public interface ICalculations
    {
        Task DeleteCalculation(int id);
        Task<IEnumerable<CalculationModel>> GetAll();
        Task<CalculationModel?> GetCalculationById(int id);
        Task InsertCalculation(CalculationModel calculation, string expression);
        Task UpdateCalculation(CalculationModel calculation);
    }
}