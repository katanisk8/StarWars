using System.Collections.Generic;

namespace CodePepper.Domain.Dtos
{
    public class CharacterDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<EpisodeDto> Episodes { get; set; }
        public ICollection<FriendDto> Friends { get; set; }
    }
}
