using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Service
{
    public class BevandaCustomRepository
    {
        private readonly BubbleTeaContext _context;

        public BevandaCustomRepository(BubbleTeaContext context)
        {
            _context = context;
        }

    }
}
