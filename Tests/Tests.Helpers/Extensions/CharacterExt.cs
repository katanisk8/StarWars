using CodePepper.Domain.Entities;
using System.Linq;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class CharacterExt
    {
        public static Character WithDefaultData(this Character item)
        {
            item.Id = 1;
            item.Name = "Character name";
            item.Episodes = Enumerable.Range(0, 5).Select(x => new Episode().WithDefaultData().With(y => y.Id = x)).ToList();
            item.Friends = Enumerable.Range(0, 5).Select(x => new Friend().WithDefaultData().With(y => y.Id = x)).ToList();

            return item;
        }
    }
}
