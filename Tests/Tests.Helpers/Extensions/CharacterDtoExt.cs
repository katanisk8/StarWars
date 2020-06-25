using CodePepper.Domain.Dtos;
using System.Linq;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class CharacterDtoExt
    {
        public static CharacterDto WithDefaultData(this CharacterDto item)
        {
            item.Id = 1;
            item.Name = "Character name";
            item.Episodes = Enumerable.Range(0, 5).Select(x => new EpisodeDto().WithDefaultData().With(y => y.Id = x)).ToList();
            item.Friends = Enumerable.Range(0, 5).Select(x => new FriendDto().WithDefaultData().With(y => y.Id = x)).ToList();

            return item;
        }
    }
}
