using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Interface;

namespace Repository.Service
{
    public class DolceRepository : IDolceRepository
    {
        private readonly BubbleTeaContext _context;

        public DolceRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DolceDTO>> GetAllAsync()
        {
            //var dolci = await _context.Dolce.AsNoTracking().ToListAsync();

            return await  _context.Dolce
                .Select(d => new DolceDTO
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
            }).ToListAsync();
        }

        public async Task<DolceDTO?> GetByIdAsync(int id)
        {
            var dolce = await _context.Dolce.FindAsync(id);
            if (dolce == null) return null;
           //var art = new ArticoloDTO { Tipo = "Dolce" };
            //await _context.Articolo.FindAsync(art.ArticoloId);
            //await _context.SaveChangesAsync();
            
            return new DolceDTO
            {
                ArticoloId = dolce.ArticoloId,
                Nome = dolce.Nome,
                Prezzo = dolce.Prezzo,
                Descrizione = dolce.Descrizione,
                ImmagineUrl = dolce.ImmagineUrl,
                Disponibile = dolce.Disponibile,
                Priorita = dolce.Priorita,
                DataCreazione = dolce.DataCreazione,
                DataAggiornamento = dolce.DataAggiornamento
            };
        }

        public async Task<DolceDTO> AddAsync(DolceDTO dto)
        {
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
                Nome = dto.Nome,
                Prezzo = dto.Prezzo,
                Descrizione = dto.Descrizione,
                ImmagineUrl = dto.ImmagineUrl,
                Disponibile = dto.Disponibile,
                Priorita = dto.Priorita,
                DataCreazione = DateTime.Now,
                DataAggiornamento = DateTime.Now,
                ArticoloId = articolo.ArticoloId
            };

            await _context.Dolce.AddAsync(dolce);
            await _context.SaveChangesAsync();

            return new DolceDTO
            {
                ArticoloId = dolce.ArticoloId,
                Nome = dolce.Nome,
                Prezzo = dolce.Prezzo,
                Descrizione = dolce.Descrizione,
                ImmagineUrl = dolce.ImmagineUrl,
                Disponibile = dolce.Disponibile,
                Priorita = dolce.Priorita,
                DataCreazione = dolce.DataCreazione,
                DataAggiornamento = dolce.DataAggiornamento
            };
        }

        public async Task UpdateAsync(DolceDTO dto)
        {
            var dolce = await _context.Dolce.FindAsync(dto.ArticoloId);
            var articolo = await _context.Articolo.FindAsync(dto.ArticoloId);
            if (articolo != null)
            {
                articolo.DataAggiornamento= DateTime.Now;
            }
            await _context.SaveChangesAsync();
            
            if (dolce == null) throw new KeyNotFoundException("Dolce non trovato");

            dolce.Nome = dto.Nome;
            dolce.Prezzo = dto.Prezzo;
            dolce.Descrizione = dto.Descrizione;
            dolce.ImmagineUrl = dto.ImmagineUrl;
            dolce.Disponibile = dto.Disponibile;
            dolce.Priorita = dto.Priorita;
            dolce.DataAggiornamento = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dolce = await _context.Dolce.FindAsync(id);
            if (dolce == null) return false;
            // Prima rimuovi l'entità dipendente (Dolce)
            _context.Dolce.Remove(dolce);
            
            var articolo = await _context.Articolo.FindAsync(id);
            if (articolo != null)
                _context.Articolo.Remove(articolo);

            //_context.Dolce.Remove(dolce);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DolceDTO>> GetByPrioritaAsync(int priorita)
        {
            /*var dolci = await _context.Dolce
                .Where(d => d.Priorita == priorita)
                .ToListAsync();

            return dolci.Select(d => new DolceDTO
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
            }).ToList();*/
            return await _context.Dolce
                .Where(d=> d.Priorita == priorita)
                .Select(d=> new DolceDTO
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
                })
                .ToListAsync();
        }
    }
}
