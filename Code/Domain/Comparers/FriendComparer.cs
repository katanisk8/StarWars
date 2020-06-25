using CodePepper.Domain.Entities;
using System.Collections.Generic;

namespace CodePepper.Domain.Comparers
{
    public class FriendComparer : IEqualityComparer<Friend>
    {
        public int GetHashCode(Friend obj) => obj.Id.GetHashCode();

        public bool Equals(Friend x, Friend y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x != null && y != null && x.Id == y.Id;
        }
    }
}
