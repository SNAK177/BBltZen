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

        // Restituisce tutti i dolci
        public async Task<IEnumerable<DolceDTO>> GetAllAsync()
        {
            return await _context.Dolce
                .AsNoTracking()
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<DolceDTO?> GetByIdAsync(int id)
        {
            var dolce = await _context.Dolce
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.ArticoloId == id);

            return dolce == null ? null : MapToDTO(dolce);
        }

        public async Task<DolceDTO> AddAsync(DolceDTO entity)
        {
            var dolce = new Dolce
            {
                Nome = entity.Nome,
                Prezzo = entity.Prezzo,
                Descrizione = entity.Descrizione,
                ImmagineUrl = entity.ImmagineUrl,
                Disponibile = entity.Disponibile,
                Priorita = entity.Priorita,
                DataCreazione = DateTime.Now,
                DataAggiornamento = DateTime.Now
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
                .FirstOrDefaultAsync(d => d.ArticoloId == entity.ArticoloId);

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

        public async Task<bool> DeleteAsync(int id)
        {
            var dolce = await _context.Dolce.FirstOrDefaultAsync(d => d.ArticoloId == id);
            if (dolce == null) return false;

            _context.Dolce.Remove(dolce);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Dolce.AnyAsync(d => d.ArticoloId == id);
        }

        public async Task<IEnumerable<DolceDTO>> GetDisponibiliAsync()
        {
            return await _context.Dolce
                .AsNoTracking()
                .Where(d => d.Disponibile)
                .OrderBy(d => d.Priorita)
                .ThenBy(d => d.Nome)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<IEnumerable<DolceDTO>> GetByPrioritaAsync(int priorita)
        {
            return await _context.Dolce
                .AsNoTracking()
                .Where(d => d.Priorita == priorita)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<bool> ToggleDisponibilitaAsync(int id, bool disponibile)
        {
            var dolce = await _context.Dolce.FirstOrDefaultAsync(d => d.ArticoloId == id);
            if (dolce == null) return false;

            dolce.Disponibile = disponibile;
            dolce.DataAggiornamento = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
