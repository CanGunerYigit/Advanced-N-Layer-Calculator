using CommonLayer.Entities;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CalculationHistoryRepository : ICalculationHistoryRepository
    {
        private readonly AppDbContext _context;

        public CalculationHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        // Tüm kayıtları getir
        public async Task<IEnumerable<CalculationHistory>> GetAllAsync()
        {
            return await _context.Set<CalculationHistory>().ToListAsync();
        }

        // Id'ye göre bir kayıt getir
        public async Task<CalculationHistory> GetByIdAsync(Guid id)
        {
            return await _context.Set<CalculationHistory>().FindAsync(id);
        }

        // Yeni bir kayıt ekle
        public async Task AddAsync(CalculationHistory history)
        {
            await _context.Set<CalculationHistory>().AddAsync(history);
            await _context.SaveChangesAsync();
        }

        // Kayıt güncelle
        public async Task UpdateAsync(CalculationHistory history)
        {
            _context.Set<CalculationHistory>().Update(history);
            await _context.SaveChangesAsync();
        }

        // Kayıt sil
        public async Task DeleteAsync(Guid id)
        {
            var history = await _context.CalculationHistories.FindAsync(id);
            if (history != null)
            {
                _context.Set<CalculationHistory>().Remove(history);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
