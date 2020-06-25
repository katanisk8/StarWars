using CodePepper.Domain.Dtos;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class FriendDtoExt
    {
        public static FriendDto WithDefaultData(this FriendDto item)
        {
            item.Id = 1;
            item.Name = "Friend name";

            return item;
        }
    }
}
