using Database;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Service
{
    public class CategorieIngredientiRepository : ICategorieIngredientiRepository
    {
        private readonly BubbleTeaContext _context;

        public CategorieIngredientiRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategorieIngredientiDTO>> GetAllAsync()
        {
            var cat = await _context
                .CategoriaIngrediente.Select(c => new CategorieIngredientiDTO
                {
                    CategoriaId = c.CategoriaId,
                    Categoria = c.Categoria,
                })
                .ToListAsync();
            return cat;
        }

        public async Task<CategorieIngredientiDTO?> GetByIdAsync(int id)
        {
            return await _context
                .CategoriaIngrediente.AsNoTracking()
                .Where(c => c.CategoriaId == id)
                .Select(c => new CategorieIngredientiDTO
                {
                    CategoriaId = c.CategoriaId,
                    Categoria = c.Categoria,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CategorieIngredientiDTO> AddAsync(
            CategorieIngredientiDTO categorieIngredienteDto
        )
        {
            if (categorieIngredienteDto == null)
                throw new ArgumentException(
                    nameof(categorieIngredienteDto),
                    "entity cannot be null"
                );

            var catEntity = new CategoriaIngrediente
            {
                CategoriaId = categorieIngredienteDto.CategoriaId,
                Categoria = categorieIngredienteDto.Categoria,
            };
            await _context.CategoriaIngrediente.AddAsync(catEntity);
            await _context.SaveChangesAsync();
            categorieIngredienteDto.CategoriaId = catEntity.CategoriaId;
            categorieIngredienteDto.Categoria = catEntity.Categoria;
            return categorieIngredienteDto;
        }

        public async Task UpdateAsync(CategorieIngredientiDTO categorieIngredienteDto)
        {
            if (categorieIngredienteDto == null || categorieIngredienteDto.CategoriaId == 0)
                throw new ArgumentException(
                    nameof(categorieIngredienteDto),
                    "entity cannot be null"
                );
            var existingCat = await _context.CategoriaIngrediente.FirstOrDefaultAsync(c =>
                c.CategoriaId == categorieIngredienteDto.CategoriaId
            );
            if (existingCat == null)
                throw new InvalidOperationException("Entity not found in the database.");
            existingCat.Categoria = categorieIngredienteDto.Categoria;
            _context.CategoriaIngrediente.Update(existingCat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cat = await _context.CategoriaIngrediente.FindAsync(id);
            if (cat == null)
                throw new ArgumentException("Categoria non trovata");
            _context.CategoriaIngrediente.Remove(cat);
            await _context.SaveChangesAsync();
        }
    }
}
