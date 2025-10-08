using Database;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Service
{
    public class ArticoloRepository : IArticoloRepository
    {
        private readonly BubbleTeaContext _context;

        public ArticoloRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticoloDTO>> GetAllAsync()
        {
            //var articolo = await _context.Articolo
            return await _context.Articolo
                .Select(a => new ArticoloDTO
                {
                    ArticoloId = a.ArticoloId,
                    Tipo = a.Tipo,
                    DataCreazione = a.DataCreazione,
                    DataAggiornamento = a.DataAggiornamento
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ArticoloDTO?> GetByIdAsync(int id)
        {
            var a = await _context.Articolo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ArticoloId == id);
            if (a == null)
                return null;
            return new ArticoloDTO
            {
                ArticoloId = a.ArticoloId,
                Tipo = a.Tipo,
                DataCreazione = a.DataCreazione,
                DataAggiornamento = a.DataAggiornamento
            };
        }

        public async Task<ArticoloDTO> AddAsync(ArticoloDTO articolo)
        {
            if(articolo == null)
                throw new ArgumentNullException(nameof(articolo), "Entity cannot be null.");
            var articoloEntity = new Articolo
            {
                Tipo = articolo.Tipo,
                DataCreazione = articolo.DataCreazione,
                DataAggiornamento = articolo.DataAggiornamento
            };
            await _context.Articolo.AddAsync(articoloEntity);
            await _context.SaveChangesAsync();
            articolo.ArticoloId = articoloEntity.ArticoloId;
            articolo.DataCreazione = articoloEntity.DataCreazione;
            articolo.DataAggiornamento = articoloEntity.DataAggiornamento;
            return articolo;
        }
        public async Task<bool> UpdateAsync(ArticoloDTO articolo)
        {
            if (articolo == null || articolo.ArticoloId == 0)
                throw new ArgumentException("Invalid entity or entity ID.");
            var articoloEntity = await _context.Articolo
                .FirstOrDefaultAsync(a => a.ArticoloId == articolo.ArticoloId);
                //.FindAsync(articolo.ArticoloId);
            if (articoloEntity == null)
                throw new KeyNotFoundException("Articolo not found.");
            articoloEntity.Tipo = articolo.Tipo;
            articoloEntity.DataAggiornamento = DateTime.Now;
            articoloEntity.DataCreazione = articolo.DataCreazione;
            articoloEntity.ArticoloId= articolo.ArticoloId;
            _context.Articolo.Update(articoloEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Articolo.FindAsync(id);
            if(entity==null)
                return false;
            _context.Articolo.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}