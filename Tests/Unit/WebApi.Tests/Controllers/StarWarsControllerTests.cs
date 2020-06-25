using CodePepper.Domain.Business.StarWars;
using CodePepper.Domain.Dtos;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CodePepper.Tests.Helpers.Extensions;
using CodePepper.WebUi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodePepper.Domain.Business.Tests.StarWars
{
    public class StarWarsControllerTests
    {
        private readonly IStarWarsService _starWarsService;

        private readonly StarWarsController _starWarsController;

        public StarWarsControllerTests()
        {
            _starWarsService = Substitute.For<IStarWarsService>();

            _starWarsController = new StarWarsController(_starWarsService);
        }

        [Fact]
        public async Task get_should_return_not_foung_when_entity_not_exists()
        {
            // Arrange
            int id = 1;
            CharacterDto dto = null;
            _starWarsService.GetAsync(id).Returns(dto);

            // Act
            IActionResult actionResult = await _starWarsController.Get(id).ConfigureAwait(false);
            NotFoundObjectResult actual = (NotFoundObjectResult)actionResult;

            // Assert
            await _starWarsService.ReceivedWithAnyArgs(1).GetAsync(id).ConfigureAwait(false);
            actual.Value.Should().Be($"Character with id: {id} not exists");
        }

        [Fact]
        public async Task get_should_return_mapped_entity_to_dto()
        {
            // Arrange
            CharacterDto dto = new CharacterDto().WithDefaultData();
            _starWarsService.GetAsync(dto.Id.Value).Returns(dto);

            // Act
            IActionResult actionResult = await _starWarsController.Get(dto.Id.Value).ConfigureAwait(false);
            CharacterDto actual = ((OkObjectResult)actionResult).Value as CharacterDto;

            // Assert
            await _starWarsService.ReceivedWithAnyArgs(1).GetAsync(dto.Id.Value).ConfigureAwait(false);
            actual.Should().Be(dto);
        }
    }
}
