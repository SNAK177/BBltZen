using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;

namespace Repository.Service
{
    public class DolceRepository : IDolceRepository
    {
        private readonly BubbleTeaContext _context;

        public DolceRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        private static DolceDTO MapToDTO(Dolce d) => new DolceDTO
        {
            ArticoloId = d.ArticoloId,
            Nome = d.Nome,
            Prezzo = d.Prezzo,
            Descrizione = d.Descrizione,
            ImmagineUrl = d.ImmagineUrl,
            Disponibile = d.Disponibile,
            Priorita = d.Priorita,
            DataCreazione = d.DataCreazione,
            DataAggiornamento = d.DataAggiornamento
           
        };

        public async Task<IEnumerable<DolceDTO>> GetAllAsync()
        {
            return await _context.Dolce
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<DolceDTO?> GetByIdAsync(int id)
        {
            var dolce = await _context.Dolce
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.ArticoloId == id && !d.IsDeleted);

            return dolce == null ? null : MapToDTO(dolce);
        }

        //public async Task<DolceDTO> AddAsync(DolceDTO entity)
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException(nameof(entity));
        //    if (string.IsNullOrWhiteSpace(entity.Nome))
        //        throw new ArgumentException("Nome is required");
        //    if (entity.Prezzo <= 0)
        //        throw new ArgumentException("Prezzo must be greater than 0");

        //    var dolce = new Dolce
        //    {
        //        Nome = entity.Nome,
        //        Prezzo = entity.Prezzo,
        //        Descrizione = entity.Descrizione,
        //        ImmagineUrl = entity.ImmagineUrl,
        //        Disponibile = entity.Disponibile,
        //        Priorita = entity.Priorita,
        //        DataCreazione = DateTime.Now,
        //        DataAggiornamento = DateTime.Now,
        //        IsDeleted = false
        //    };

        //    await _context.Dolce.AddAsync(dolce);
        //    await _context.SaveChangesAsync();

        //    entity.ArticoloId = dolce.ArticoloId;
        //    entity.DataCreazione = dolce.DataCreazione;
        //    entity.DataAggiornamento = dolce.DataAggiornamento;

        //    return entity;
        //}
        public async Task<DolceDTO> AddAsync(DolceDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrWhiteSpace(entity.Nome))
                throw new ArgumentException("Nome is required");
            if (entity.Prezzo <= 0)
                throw new ArgumentException("Prezzo must be greater than 0");

            // CREA PRIMA L'ARTICOLO ASSOCIATO
            var articolo = new Articolo
            {
                Tipo = "Dolce",
                DataCreazione = DateTime.Now,
                DataAggiornamento = DateTime.Now
            };
            await _context.Articolo.AddAsync(articolo);
            await _context.SaveChangesAsync();

            var dolce = new Dolce
            {
                ArticoloId = articolo.ArticoloId, // Associa la chiave esterna
                Nome = entity.Nome,
                Prezzo = entity.Prezzo,
                Descrizione = entity.Descrizione,
                ImmagineUrl = entity.ImmagineUrl,
                Disponibile = entity.Disponibile,
                Priorita = entity.Priorita,
                DataCreazione = DateTime.Now,
                DataAggiornamento = DateTime.Now,
                IsDeleted = false
            };

            await _context.Dolce.AddAsync(dolce);
            await _context.SaveChangesAsync();

            entity.ArticoloId = dolce.ArticoloId;
            entity.DataCreazione = dolce.DataCreazione;
            entity.DataAggiornamento = dolce.DataAggiornamento;

            return entity;
        }

        public async Task UpdateAsync(DolceDTO entity)
        {
            if (entity == null || entity.ArticoloId == 0)
                throw new ArgumentException("Invalid entity or entity ID");

            var existingDolce = await _context.Dolce
                .FirstOrDefaultAsync(d => d.ArticoloId == entity.ArticoloId && !d.IsDeleted);

            if (existingDolce == null)
                throw new KeyNotFoundException($"Dolce with ID {entity.ArticoloId} not found");

            existingDolce.Nome = entity.Nome;
            existingDolce.Prezzo = entity.Prezzo;
            existingDolce.Descrizione = entity.Descrizione;
            existingDolce.ImmagineUrl = entity.ImmagineUrl;
            existingDolce.Disponibile = entity.Disponibile;
            existingDolce.Priorita = entity.Priorita;
            existingDolce.DataAggiornamento = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        // Soft delete
        public async Task<bool> DeleteAsync(int id)
        {
            var dolce = await _context.Dolce.FirstOrDefaultAsync(d => d.ArticoloId == id && !d.IsDeleted);
            if (dolce == null) return false;

            dolce.IsDeleted = true;
            dolce.DataAggiornamento = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Dolce.AnyAsync(d => d.ArticoloId == id && !d.IsDeleted);
        }

        public async Task<IEnumerable<DolceDTO>> GetDisponibiliAsync()
        {
            return await _context.Dolce
                .AsNoTracking()
                .Where(d => d.Disponibile && !d.IsDeleted)
                .OrderBy(d => d.Priorita)
                .ThenBy(d => d.Nome)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }   

        public async Task<IEnumerable<DolceDTO>> GetByPrioritaAsync(int priorita)
        {
            return await _context.Dolce
                .AsNoTracking()
                .Where(d => d.Priorita == priorita && d.Disponibile && !d.IsDeleted)
                .OrderBy(d => d.Nome)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<bool> ToggleDisponibilitaAsync(int id, bool disponibile)
        {
            var dolce = await _context.Dolce.FirstOrDefaultAsync(d => d.ArticoloId == id && !d.IsDeleted);
            if (dolce == null) return false;

            dolce.Disponibile = disponibile;
            dolce.DataAggiornamento = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
