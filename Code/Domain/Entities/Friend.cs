﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodePepper.Domain.Entities
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationRules.Friend.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }

        public virtual Character Character { get; set; }
    }
}