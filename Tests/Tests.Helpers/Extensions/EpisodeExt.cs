using CodePepper.Domain.Entities;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class EpisodeExt
    {
        public static Episode WithDefaultData(this Episode item)
        {
            item.Id = 1;
            item.Name = "Episode name";

            return item;
        }
    }
}
