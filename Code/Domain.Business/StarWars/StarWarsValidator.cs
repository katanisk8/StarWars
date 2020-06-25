using CodePepper.Domain.Dtos;
using CodePepper.Domain.Repositories;
using CSharpFunctionalExtensions;
using System.Linq;
using System.Threading.Tasks;

namespace CodePepper.Domain.Business.StarWars
{
    public interface IStarWarsValidator
    {
        Result ValidateAdd(CharacterDto dto);
        Task<Result> ValidateUpdateAsync(CharacterDto dto);
        Task<Result> ValidateDeleteAsync(int id);
    }

    public class StarWarsValidator : IStarWarsValidator
    {
        private readonly IStarWarsRepository _repository;

        public StarWarsValidator(IStarWarsRepository repository)
        {
            _repository = repository;
        }

        public Result ValidateAdd(CharacterDto dto) =>
            BaseValidate(dto)
                .Ensure(x => !x.Id.HasValue, "Character id cannot be added")
                .Ensure(x => x.Episodes.All(y => !y.Id.HasValue), "Episode id cannot be added")
                .Ensure(x => x.Friends.All(y => !y.Id.HasValue), "Friend id cannot be added");

        public async Task<Result> ValidateUpdateAsync(CharacterDto dto)
        {
            Result<CharacterDto> result = BaseValidate(dto)
                .Ensure(x => x.Id.HasValue, "Character id is required");

            if (result.IsFailure)
            {
                return result;
            }

            return await CharacterExists(dto.Id.Value).ConfigureAwait(false)
                ? result
                : Result.Failure($"Character with id: {dto.Id} not exists");
        }

        public async Task<Result> ValidateDeleteAsync(int id)
        {
            Result<int> result = Result.Ok(id)
                .Ensure(x => x > 0, "Character id is required");

            if (result.IsFailure)
            {
                return result;
            }

            return await CharacterExists(id).ConfigureAwait(false)
                ? result
                : Result.Failure($"Character with id: {id} not exists");
        }

        private Result<CharacterDto> BaseValidate(CharacterDto dto)
        {
            return Result.Ok(dto)
                .Ensure(x => x != null, "No input provided")
                .Ensure(x => !string.IsNullOrWhiteSpace(x.Name), "Character name is required")
                .Ensure(x => 
                    x.Name.Length <= ValidationRules.Character.NameMaxLength, 
                    $"Character name cannot be longer than {ValidationRules.Character.NameMaxLength} characters")
                .Ensure(x => x.Episodes != null, "Character episodes is required")
                .Ensure(x => x.Episodes.All(y => y != null), "Episode cannot be null")
                .Ensure(x => x.Episodes.All(y => !string.IsNullOrWhiteSpace(y.Name)), "Episode name is required")
                .Ensure(x => 
                    x.Episodes.All(y => y.Name.Length <= ValidationRules.Episode.NameMaxLength), 
                    $"Episode name cannot be longer than {ValidationRules.Episode.NameMaxLength} characters")
                .Ensure(x => x.Friends != null, "Character friends is required")
                .Ensure(x => x.Friends.All(y => y != null), "Friend cannot be null")
                .Ensure(x => x.Friends.All(y => !string.IsNullOrWhiteSpace(y.Name)), "Friend name is required")
                .Ensure(x =>
                    x.Friends.All(y => y.Name.Length <= ValidationRules.Friend.NameMaxLength),
                    $"Friend name cannot be longer than {ValidationRules.Friend.NameMaxLength} characters");
        }

        private Task<bool> CharacterExists(int id) => _repository.CharacterAlreadyExists(id);
    }
}
