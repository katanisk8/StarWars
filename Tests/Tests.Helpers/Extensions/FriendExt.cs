using CodePepper.Domain.Entities;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class FriendExt
    {
        public static Friend WithDefaultData(this Friend item)
        {
            item.Id = 1;
            item.Name = "Friend name";

            return item;
        }
    }
}
