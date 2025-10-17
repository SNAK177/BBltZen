using Database;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Service
{
    public  class VwArticoliCompletiRepository
    {

        private readonly BubbleTeaContext _context;
        public VwArticoliCompletiRepository(BubbleTeaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VwArticoliCompletiDTO>>
    }
}
