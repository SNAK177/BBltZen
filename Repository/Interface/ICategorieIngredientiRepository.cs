using DTO;

namespace Repository.Service;

public interface ICategorieIngredientiRepository
{
    Task<IEnumerable<CategorieIngredientiDTO>> GetAllAsync();
    Task<CategorieIngredientiDTO?> GetByIdAsync(int id);
    Task<CategorieIngredientiDTO> AddAsync(CategorieIngredientiDTO categorieIngredienteDto);
    Task UpdateAsync(CategorieIngredientiDTO categorieIngredienteDto);
    Task DeleteAsync(int id);
}