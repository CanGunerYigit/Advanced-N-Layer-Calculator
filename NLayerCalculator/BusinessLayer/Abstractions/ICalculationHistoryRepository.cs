using CommonLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public interface ICalculationHistoryRepository
    {
        Task AddAsync(CalculationHistory history);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<CalculationHistory>> GetAllAsync();
        Task<CalculationHistory> GetByIdAsync(Guid id);
        Task UpdateAsync(CalculationHistory history);
        Task SaveChangesAsync();
    }
}