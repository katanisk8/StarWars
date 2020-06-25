using CodePepper.Domain.Entities;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CodePepper.Domain.Pagination;

namespace CodePepper.Domain.Repositories
{
    public interface IStarWarsRepository
    {
        Task<PagedResult<Character>> GetAllAsync(Paging paging);
        Task<Character> GetAsync(int id);
        Task<Result> AddAsync(Character character);
        Task<Result> UpdateAsync(Character character);
        Task<Result> DeleteAsync(int id);
        Task<bool> CharacterAlreadyExists(int id);
    }
}
