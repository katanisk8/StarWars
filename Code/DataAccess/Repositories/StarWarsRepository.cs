using CodePepper.DataAccess.Extensions;
using CodePepper.Domain.Comparers;
using CodePepper.Domain.Entities;
using CodePepper.Domain.Pagination;
using CodePepper.Domain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePepper.DataAccess.Repositories
{
    public class StarWarsRepository : IStarWarsRepository
    {
        private readonly AppDbContext _context;

        public StarWarsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<PagedResult<Character>> GetAllAsync(Paging paging) =>
            _context.Characters
                .Include(x => x.Episodes)
                .Include(x => x.Friends)
                .GetPagedResultAsync(paging);

        public Task<Character> GetAsync(int id) =>
            _context.Characters
                .Include(x => x.Episodes)
                .Include(x => x.Friends)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Result> AddAsync(Character entity)
        {
            try
            {
                _context.Characters.Add(entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                return Result.Failure(exc.GetBaseException().Message);
            }
        }

        public async Task<Result> UpdateAsync(Character entity)
        {
            try
            {
                Character existingCharacter = await GetAsync(entity.Id).ConfigureAwait(false);

                existingCharacter.Name = entity.Name;
                UpdateEpisodes(entity.Episodes, existingCharacter.Episodes);
                UpdateFriends(entity.Friends, existingCharacter.Friends);

                _context.Characters.Update(existingCharacter);

                await _context.SaveChangesAsync().ConfigureAwait(false);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                return Result.Failure(exc.GetBaseException().Message);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                Character existingCharacter = await GetAsync(id).ConfigureAwait(false);
                _context.Characters.Remove(existingCharacter);

                await _context.SaveChangesAsync().ConfigureAwait(false);

                return Result.Ok();
            }
            catch (Exception exc)
            {
                return Result.Failure(exc.GetBaseException().Message);
            }
        }

        public async Task<bool> CharacterAlreadyExists(int id) =>
            await _context.Characters
                .CountAsync(x => x.Id == id)
                .ConfigureAwait(false) > 0;

        private void UpdateEpisodes(ICollection<Episode> episodes, ICollection<Episode> existingEpisodes)
        {
            EpisodeComparer comparer = new EpisodeComparer();

            RemoveEpisodes(episodes, existingEpisodes, comparer);
            UpdateEpisodes(episodes, existingEpisodes, comparer);
            AddEpisodes(episodes, existingEpisodes);
        }

        private void RemoveEpisodes(ICollection<Episode> episodes, ICollection<Episode> existingEpisodes, EpisodeComparer comparer)
        {
            IList<Episode> existingEpisodesToDelete = existingEpisodes.Except(episodes, comparer).ToList();
            foreach (Episode existingEpisodeToDelete in existingEpisodesToDelete)
            {
                existingEpisodes.Remove(existingEpisodeToDelete);
            }
            _context.RemoveRange(existingEpisodesToDelete);
        }

        private static void UpdateEpisodes(ICollection<Episode> episodes, ICollection<Episode> existingEpisodes, EpisodeComparer comparer)
        {
            IList<Episode> existingEpisodesToUpdate = existingEpisodes.Where(x => episodes.Contains(x, comparer)).ToList();
            foreach (Episode existingEpisodeToUpdate in existingEpisodesToUpdate)
            {
                Episode episode = episodes.FirstOrDefault(x => x.Id == existingEpisodeToUpdate.Id);
                existingEpisodeToUpdate.Name = episode.Name;
            }
        }

        private static void AddEpisodes(ICollection<Episode> episodes, ICollection<Episode> existingEpisodes)
        {
            IList<Episode> episodesToAdd = episodes.Where(x => existingEpisodes.All(y => y.Id != x.Id)).ToList();

            foreach (Episode episodeToAdd in episodesToAdd)
            {
                existingEpisodes.Add(episodeToAdd);
            }
        }

        private void UpdateFriends(ICollection<Friend> friends, ICollection<Friend> existingFriends)
        {
            FriendComparer comparer = new FriendComparer();

            RemoveFriends(friends, existingFriends, comparer);
            UpdateFriends(friends, existingFriends, comparer);
            AddFriends(friends, existingFriends);
        }

        private void RemoveFriends(ICollection<Friend> friends, ICollection<Friend> existingFriends, FriendComparer comparer)
        {
            IList<Friend> existingFriendsToDelete = existingFriends.Except(friends, comparer).ToList();
            foreach (Friend existingFriendToDelete in existingFriendsToDelete)
            {
                existingFriends.Remove(existingFriendToDelete);
            }
            _context.RemoveRange(existingFriendsToDelete);
        }

        private void UpdateFriends(ICollection<Friend> friends, ICollection<Friend> existingFriends, FriendComparer comparer)
        {
            IList<Friend> existingFriendsToUpdate = existingFriends.Where(x => friends.Contains(x, comparer)).ToList();
            foreach (Friend existingFriendToUpdate in existingFriendsToUpdate)
            {
                Friend friend = friends.FirstOrDefault(x => x.Id == existingFriendToUpdate.Id);
                existingFriendToUpdate.Name = friend.Name;
            }
        }

        private void AddFriends(ICollection<Friend> friends, ICollection<Friend> existingFriends)
        {
            IList<Friend> friendsToAdd = friends.Where(x => existingFriends.All(y => y.Id != x.Id)).ToList();

            foreach (Friend friendToAdd in friendsToAdd)
            {
                existingFriends.Add(friendToAdd);
            }
        }
    }
}
