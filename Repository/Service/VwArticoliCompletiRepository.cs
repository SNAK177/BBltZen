using Database;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Service
{
    public  class VwArticoliCompletiRepository
    {

        private readonly BubbleTeaContext _context;
        public VwArticoliCompletiRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VwArticoliCompletiDTO>> GetAllAsync()
        {
            return (IEnumerable<VwArticoliCompletiDTO>)await _context.VwArticoliCompleti
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<VwArticoliCompleti?> GetByIdAsync(int id)
        {
            return await _context.VwArticoliCompleti
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticoloId == id);\
        }
    }
}
