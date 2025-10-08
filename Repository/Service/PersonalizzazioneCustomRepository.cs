using Database;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Service
{
    public class PersonalizzazioneCustomRepository : IPersonalizzazioneCustomRepository
    {
        private readonly BubbleTeaContext _context;

        public PersonalizzazioneCustomRepository(BubbleTeaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonalizzazioneCustomDTO>> GetAllAsync()
        {
            // Prendi tutte le personalizzazioni in memoria
            var list = await _context.PersonalizzazioneCustom.AsNoTracking().ToListAsync();

            // Proietta in DTO in memoria
            return list.Select(p => new PersonalizzazioneCustomDTO
            {
                PersCustomId = p.PersCustomId,
                Nome = p.Nome,
                GradoDolcezza = p.GradoDolcezza,
                DimensioneBicchiereId = p.DimensioneBicchiereId,
                BevandaCustomDTO =
                    p.BevandaCustom.FirstOrDefault() == null
                        ? null
                        : new BevandaCustomDTO
                        {
                            BevandaCustomId = p.BevandaCustom.First().BevandaCustomId,
                            ArticoloId = p.BevandaCustom.First().ArticoloId,
                            PersCustomId = p.BevandaCustom.First().PersCustomId,
                            Prezzo = p.BevandaCustom.First().Prezzo,
                        },
            });
        }

        public async Task<PersonalizzazioneCustomDTO?> GetByIdAsync(int id)
        {
            return await _context
                .PersonalizzazioneCustom.AsNoTracking()
                .Where(p => p.PersCustomId == id)
                .Select(p => new PersonalizzazioneCustomDTO
                {
                    PersCustomId = p.PersCustomId,
                    Nome = p.Nome,
                    GradoDolcezza = p.GradoDolcezza,
                    DimensioneBicchiereId = p.DimensioneBicchiereId,
                    BevandaCustomDTO =
                        p.BevandaCustom.FirstOrDefault() != null
                            ? new BevandaCustomDTO
                            {
                                BevandaCustomId = p.BevandaCustom.First().BevandaCustomId,
                                ArticoloId = p.BevandaCustom.First().ArticoloId,
                                PersCustomId = p.BevandaCustom.First().PersCustomId,
                                Prezzo = p.BevandaCustom.First().Prezzo,
                            }
                            : null,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PersonalizzazioneCustomDTO> AddAsync(PersonalizzazioneCustomDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Entity cannot be null.");

            var entity = new PersonalizzazioneCustom
            {
                Nome = dto.Nome,
                GradoDolcezza = dto.GradoDolcezza,
                DimensioneBicchiereId = dto.DimensioneBicchiereId,
            };

            await _context.PersonalizzazioneCustom.AddAsync(entity);
            await _context.SaveChangesAsync();

            dto.PersCustomId = entity.PersCustomId;
            return dto;
        }

        public async Task<bool> UpdateAsync(PersonalizzazioneCustomDTO dto)
        {
            var entity = await _context.PersonalizzazioneCustom.FindAsync(dto.PersCustomId);
            if (entity == null)
                return false;

            entity.Nome = dto.Nome;
            entity.GradoDolcezza = dto.GradoDolcezza;
            entity.DimensioneBicchiereId = dto.DimensioneBicchiereId;

            _context.PersonalizzazioneCustom.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PersonalizzazioneCustom.FindAsync(id);
            if (entity == null)
                return false;

            _context.PersonalizzazioneCustom.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
