using CodePepper.Domain.Dtos;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class EpisodeDtoExt
    {
        public static EpisodeDto WithDefaultData(this EpisodeDto item)
        {
            item.Id = 1;
            item.Name = "Episode name";

            return item;
        }
    }
}
