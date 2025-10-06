using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Service
{
    public class IngredienteRepository : IIngredienteRepository
    {
        private readonly BubbleTeaContext _context;
        public IngredienteRepository(BubbleTeaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<IngredienteDTO>> GetAllAsync()
        {
            return await _context.Ingrediente
                .Select(i => new IngredienteDTO
                {
                    IngredienteId = i.IngredienteId,
                    CategoriaId = i.CategoriaId,
                    Disponibile = i.Disponibile,
                    PrezzoAggiunto = i.PrezzoAggiunto,
                    Ingrediente1 = i.Ingrediente1
                    // Map other properties as needed
                })
                .ToListAsync();
        }

        public async Task<IngredienteDTO> GetByIdAsync(int id)
        {
            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente == null) return null;

            return new IngredienteDTO
            {
                IngredienteId = ingrediente.IngredienteId,
                CategoriaId = ingrediente.CategoriaId,
                Disponibile = ingrediente.Disponibile,
                PrezzoAggiunto = ingrediente.PrezzoAggiunto,
                Ingrediente1 = ingrediente.Ingrediente1
                // Map other properties as needed
            };
        }

        public async Task AddAsync(IngredienteDTO ingredienteDto)
        {
            var ingrediente = new Ingrediente
            {
                IngredienteId = ingredienteDto.IngredienteId,
                CategoriaId = ingredienteDto.CategoriaId,
                Disponibile = ingredienteDto.Disponibile,
                PrezzoAggiunto = ingredienteDto.PrezzoAggiunto,
                Ingrediente1 =  ingredienteDto.Ingrediente1
                // Map other properties as needed
            };

            await _context.Ingrediente.AddAsync(ingrediente);
            await _context.SaveChangesAsync();

            // Return the generated ID to the DTO
            ingredienteDto.IngredienteId = ingrediente.IngredienteId;
        }

        public async Task UpdateAsync(IngredienteDTO ingredienteDto)
        {
            var ingrediente = await _context.Ingrediente.FindAsync(ingredienteDto.IngredienteId);
            if (ingrediente == null)
                throw new ArgumentException("Ingrediente not found");

            ingrediente.Ingrediente1 = ingredienteDto.Ingrediente1;
            ingrediente.PrezzoAggiunto = ingredienteDto.PrezzoAggiunto;
            ingrediente.IngredienteId = ingredienteDto.IngredienteId;
            ingrediente.CategoriaId = ingredienteDto.CategoriaId;
            ingrediente.Disponibile = ingredienteDto.Disponibile;
            // Update other properties as needed

            _context.Ingrediente.Update(ingrediente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente != null)
            {
                _context.Ingrediente.Remove(ingrediente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Ingrediente.AnyAsync(i => i.IngredienteId == id);
        }

        public async Task<IEnumerable<IngredienteDTO>> GetByCategoriaAsync(int categoriaId)
        {
            return await _context.Ingrediente
                .Where(i => i.CategoriaId == categoriaId)
                .Select(i => new IngredienteDTO
                {
                    IngredienteId = i.IngredienteId,
                    PrezzoAggiunto = i.PrezzoAggiunto,
                    CategoriaId = i.CategoriaId,
                    Disponibile = i.Disponibile,
                    Ingrediente1 =  i.Ingrediente1
                    // Map other properties as needed
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<IngredienteDTO>> GetDisponibiliAsync(bool disponibile)
        {
            return await _context.Ingrediente
                .Where(i => i.Disponibile)
                .Select(i => new IngredienteDTO
                {
                    IngredienteId = i.IngredienteId,
                    PrezzoAggiunto = i.PrezzoAggiunto,
                    CategoriaId = i.CategoriaId,
                    Disponibile = i.Disponibile,
                    Ingrediente1 =  i.Ingrediente1
                    // Map other properties as needed
                })
                .ToListAsync();
        }
    }
}
