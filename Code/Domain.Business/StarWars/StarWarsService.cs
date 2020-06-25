using CodePepper.Domain.Dtos;
using CodePepper.Domain.Entities;
using CodePepper.Domain.Pagination;
using CodePepper.Domain.Repositories;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodePepper.Domain.Business.StarWars
{
    public interface IStarWarsService
    {
        Task<PagedResult<CharacterDto>> GetAllAsync(Paging paging);
        Task<CharacterDto> GetAsync(int id);
        Task<OperationResult> AddAsync(CharacterDto dto);
        Task<OperationResult> UpdateAsync(CharacterDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }

    public class StarWarsService : IStarWarsService
    {
        private readonly IStarWarsValidator _starWarsValidator;
        private readonly IStarWarsMapper _starWarsMapper;
        private readonly IStarWarsRepository _starWarsRepository;

        public StarWarsService(
            IStarWarsValidator starWarsValidator,
            IStarWarsMapper starWarsMapper,
            IStarWarsRepository starWarsRepository)
        {
            _starWarsValidator = starWarsValidator;
            _starWarsMapper = starWarsMapper;
            _starWarsRepository = starWarsRepository;
        }

        public async Task<PagedResult<CharacterDto>> GetAllAsync(Paging paging)
        {
            PagedResult<Character> pagedResult = await _starWarsRepository.GetAllAsync(paging).ConfigureAwait(false);
            return _starWarsMapper.Map(pagedResult);
        }

        public async Task<CharacterDto> GetAsync(int id)
        {
            Character entity = await _starWarsRepository.GetAsync(id).ConfigureAwait(false);
            return _starWarsMapper.Map(entity);
        }

        public async Task<OperationResult> AddAsync(CharacterDto dto)
        {
            Result validationResult = _starWarsValidator.ValidateAdd(dto);
            if (validationResult.IsFailure)
            {
                return validationResult.ToOperationResult();
            }

            Character entity = _starWarsMapper.Map(dto);
            Result addResult = await _starWarsRepository.AddAsync(entity).ConfigureAwait(false);

            return addResult.ToOperationResult();
        }

        public async Task<OperationResult> UpdateAsync(CharacterDto dto)
        {
            Result validationResult = await _starWarsValidator.ValidateUpdateAsync(dto).ConfigureAwait(false);
            if (validationResult.IsFailure)
            {
                return validationResult.ToOperationResult();
            }

            Character entity = _starWarsMapper.Map(dto);

            Result updateResult = await _starWarsRepository.UpdateAsync(entity).ConfigureAwait(false);

            return updateResult.ToOperationResult();
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            Result validationResult = await _starWarsValidator.ValidateDeleteAsync(id).ConfigureAwait(false);
            if (validationResult.IsFailure)
            {
                return validationResult.ToOperationResult();
            }

            Result deleteResult = await _starWarsRepository.DeleteAsync(id).ConfigureAwait(false);

            return deleteResult.ToOperationResult();
        }
    }
}
