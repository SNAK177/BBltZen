using DTO;

namespace Repository.Service;

public interface IDolceRepository
{
    Task<IEnumerable<DolceDTO>> GetAllAsync();
    Task<DolceDTO?> GetByIdAsync(int id);
    Task<DolceDTO> AddAsync(DolceDTO dto);
    Task UpdateAsync(DolceDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<DolceDTO>> GetByPrioritaAsync(int priorita);
}