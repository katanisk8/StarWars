using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodePepper.Domain.Entities
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationRules.Character.NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public ICollection<Friend> Friends { get; set; }
    }
}
