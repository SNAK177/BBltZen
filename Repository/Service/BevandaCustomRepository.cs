using Database;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Service
{
    public class BevandaCustomRepository
    {
        private readonly BubbleTeaContext _context;

        public BevandaCustomRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BevandaCustomDTO>> GetAllAsync()
        {
            // Carico le dimensioni bicchiere in memoria
            var dimensioni = await _context.DimensioneBicchiere
                .Select(d => new DimensioneBicchiereDTO
                {
                    DimensioneBicchiereId = d.DimensioneBicchiereId,
                    Sigla = d.Sigla,
                    Descrizione = d.Descrizione,
                    Capienza = d.Capienza,
                    UnitaMisuraId = d.UnitaMisuraId,
                    PrezzoBase = d.PrezzoBase,
                    Moltiplicatore = d.Moltiplicatore
                })
                .ToListAsync();

            // 2️ Carico le personalizzazioni in memoria
            var personalizzazioni = await _context.PersonalizzazioneCustom
                .Select(p => new PersonalizzazioneCustomDTO
                {
                    PersCustomId = p.PersCustomId,
                    Nome = p.Nome,
                    GradoDolcezza = p.GradoDolcezza,
                    DimensioneBicchiereId = p.DimensioneBicchiereId
                })
                .ToListAsync();

            // 3 Carico le bevande custom
            var bevande = await _context.BevandaCustom.
                Select(b => new
                { 
                    b.BevandaCustomId,
                    b.ArticoloId,
                    b.PersCustomId,
                    b.Prezzo,
                })
                .ToListAsync();
            var result = bevande.Select(b => new BevandaCustomDTO
            {
                BevandaCustomId = b.BevandaCustomId,
                ArticoloId = b.ArticoloId,
                PersCustomId = b.PersCustomId,
                Prezzo = b.Prezzo,
                DimensioneBicchiere =
                    dimensioni.FirstOrDefault(d => d.DimensioneBicchiereId == b.DimensioneBicchiereId),
                Personalizzazione = personalizzazioni.FirstOrDefault(p => p.PersCustomId == b.PersCustomId)
            }).ToList();

            return result;
        }


        public async Task<BevandaCustomDTO?> GetByIdAsync(int id)
        {
            return await _context
                .BevandaCustom.Where(b => b.BevandaCustomId == id)
                .Select(b => new BevandaCustomDTO
                {
                    BevandaCustomId = b.BevandaCustomId,
                    ArticoloId = b.ArticoloId,
                    PersCustomId = b.PersCustomId,
                    Prezzo = b.Prezzo,
                    DimensioneBicchiere = new DimensioneBicchiereDTO
                    {
                        DimensioneBicchiereId = b.DimensioneBicchiere.DimensioneBicchiereId,
                        Sigla = b.DimensioneBicchiere.Sigla,
                        Descrizione = b.DimensioneBicchiere.Descrizione,
                        Capienza = b.DimensioneBicchiere.Capienza,
                        UnitaMisuraId = b.DimensioneBicchiere.UnitaMisuraId,
                        PrezzoBase = b.DimensioneBicchiere.PrezzoBase,
                        Moltiplicatore = b.DimensioneBicchiere.Moltiplicatore,
                    },
                    Personalizzazione = new PersonalizzazioneCustomDTO
                    {
                        PersCustomId = b.PersonalizzazioneCustom.PersCustomId,
                        Nome = b.PersonalizzazioneCustom.Nome,
                        GradoDolcezza = b.PersonalizzazioneCustom.GradoDolcezza,
                        DimensioneBicchiereId = b.PersonalizzazioneCustom.DimensioneBicchiereId,
                    },
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(BevandaCustom entity)
        {
            _context.BevandaCustom.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BevandaCustom entity)
        {
            _context.BevandaCustom.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.BevandaCustom.FindAsync(id);
            if (entity != null)
            {
                _context.BevandaCustom.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
}
