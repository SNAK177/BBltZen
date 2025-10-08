using DTO;

namespace Repository.Service;

public interface IArticoloRepository
{
    Task<IEnumerable<ArticoloDTO>> GetAllAsync();
    Task<ArticoloDTO?> GetByIdAsync(int id);
    Task<ArticoloDTO> AddAsync(ArticoloDTO articolo);
    Task<bool> UpdateAsync(ArticoloDTO articolo);
    Task<bool> DeleteAsync(int id);
}