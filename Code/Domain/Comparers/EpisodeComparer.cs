using CodePepper.Domain.Entities;
using System.Collections.Generic;

namespace CodePepper.Domain.Comparers
{
    public class EpisodeComparer : IEqualityComparer<Episode>
    {
        public int GetHashCode(Episode obj) => obj.Id.GetHashCode();

        public bool Equals(Episode x, Episode y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x != null && y != null && x.Id == y.Id;
        }
    }
}
