using CodePepper.Domain.Business.StarWars;
using CodePepper.Domain.Dtos;
using CodePepper.Domain.Entities;
using CodePepper.Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CodePepper.Tests.Helpers.Extensions;

namespace CodePepper.Domain.Business.Tests.StarWars
{
    public class StarWarsServiceTests
    {
        private readonly IStarWarsValidator _starWarsValidator;
        private readonly IStarWarsMapper _starWarsMapper;
        private readonly IStarWarsRepository _starWarsRepository;

        private readonly IStarWarsService _starWarsService;

        public StarWarsServiceTests()
        {
            _starWarsValidator = Substitute.For<IStarWarsValidator>();
            _starWarsMapper = Substitute.For<IStarWarsMapper>();
            _starWarsRepository = Substitute.For<IStarWarsRepository>();

            _starWarsService = new StarWarsService(_starWarsValidator, _starWarsMapper, _starWarsRepository);
        }

        [Fact]
        public async Task get_should_return_null_when_entity_not_exists()
        {
            // Arrange
            int id = 1;
            Character entity = null;
            CharacterDto dto = null;

            _starWarsRepository.GetAsync(id).Returns(entity);
            _starWarsMapper.Map(Arg.Any<Character>()).Returns(dto);

            // Act
            CharacterDto actual = await _starWarsService.GetAsync(id).ConfigureAwait(false);

            // Assert
            await _starWarsRepository.ReceivedWithAnyArgs(1).GetAsync(id).ConfigureAwait(false);
            _starWarsMapper.ReceivedWithAnyArgs(1).Map(entity);
            actual.Should().BeNull();
        }

        [Fact]
        public async Task get_should_return_mapped_entity_to_dto()
        {
            // Arrange
            Character entity = new Character().WithDefaultData();
            CharacterDto dto = new CharacterDto().WithDefaultData();

            _starWarsRepository.GetAsync(entity.Id).Returns(entity);
            _starWarsMapper.Map(Arg.Any<Character>()).Returns(dto);

            // Act
            CharacterDto actual = await _starWarsService.GetAsync(entity.Id).ConfigureAwait(false);

            // Assert
            await _starWarsRepository.ReceivedWithAnyArgs(1).GetAsync(entity.Id).ConfigureAwait(false);
            _starWarsMapper.ReceivedWithAnyArgs(1).Map((Character)null);
            actual.Should().Be(dto);
        }
    }
}
