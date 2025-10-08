using DTO;

namespace Repository.Service;

public interface IPersonalizzazioneCustomRepository
{
    Task<IEnumerable<PersonalizzazioneCustomDTO>> GetAllAsync();
    Task<PersonalizzazioneCustomDTO?> GetByIdAsync(int id);
    Task<PersonalizzazioneCustomDTO> AddAsync(PersonalizzazioneCustomDTO dto);
    Task<bool> UpdateAsync(PersonalizzazioneCustomDTO dto);
    Task<bool> DeleteAsync(int id);
}