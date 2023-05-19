using DataAccess.Models;
using DataAccess.Data;

namespace DataAccess.Data
{
    public interface ICalculations
    {
        Task DeleteCalculation(int id);
        Task<IEnumerable<CalculationModel>> GetAll();
        Task<CalculationModel?> GetCalculationById(int id);
        Task InsertCalculation(CalculationModel calculation);
        Task UpdateCalculation(CalculationModel calculation);
    }
}