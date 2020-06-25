using CodePepper.Domain.Dtos;
using CodePepper.Domain.Entities;
using CodePepper.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodePepper.Domain.Business.StarWars
{
    public interface IStarWarsMapper
    {
        PagedResult<CharacterDto> Map(PagedResult<Character> pagedResult);
        CharacterDto Map(Character entity);
        Character Map(CharacterDto dto);
    }

    public class StarWarsMapper : IStarWarsMapper
    {
        public PagedResult<CharacterDto> Map(PagedResult<Character> pagedResult) =>
            new PagedResult<CharacterDto>
            {
                Page = pagedResult.Page,
                PageSize = pagedResult.PageSize,
                TotalRows = pagedResult.TotalRows,
                Data = Map(pagedResult.Data)
            };

        private List<CharacterDto> Map(List<Character> data) => data.Select(x => Map(x)).ToList();

        public CharacterDto Map(Character entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new CharacterDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Episodes = Map(entity.Episodes),
                Friends = Map(entity.Friends)
            };
        }

        public Character Map(CharacterDto dto) =>
            new Character
            {
                Id = dto.Id ?? default,
                Name = dto.Name,
                Episodes = Map(dto.Episodes),
                Friends = Map(dto.Friends)
            };

        private ICollection<EpisodeDto> Map(ICollection<Episode> entities) =>
            entities.Select(x => new EpisodeDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        private ICollection<FriendDto> Map(ICollection<Friend> entities) =>
            entities.Select(x => new FriendDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        private ICollection<Episode> Map(ICollection<EpisodeDto> dtos) =>
            dtos.Select(x => new Episode
            {
                Id = x.Id ?? default,
                Name = x.Name
            })
            .ToList();

        private ICollection<Friend> Map(ICollection<FriendDto> dtos) =>
            dtos.Select(x => new Friend
            {
                Id = x.Id ?? default,
                Name = x.Name
            })
            .ToList();
    }
}
