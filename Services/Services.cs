﻿using Microsoft.EntityFrameworkCore;
using MTG_Project.Components.Pages;
using MTG_Project.Models;
using Type = MTG_Project.Models.Type;

namespace MTG_Project.Services
{
    public class Services
    {
        readonly MyDBContext dbContext = new();

        public Services() { }

        public IList<Card> GetCards(string sortOrder,
                                    int pageNumber,
                                    int pageSize,
                                    DeckBuilder.CardSearch cardSearch)
        {
            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            Console.WriteLine("RarityCode: " + cardSearch.RarityCode);
            Console.WriteLine("RarityCode null or empty? " + string.IsNullOrEmpty(cardSearch.RarityCode));
            Console.WriteLine("Colors: " + cardSearch.Colors);
            Console.WriteLine("Colors null? " + (cardSearch.Colors == null));

            if (!string.IsNullOrEmpty(cardSearch.CardSearchTerm))
            {
                query = query.Where(c => c.Name.Contains(cardSearch.CardSearchTerm));
            }

            if (!string.IsNullOrEmpty(cardSearch.ArtistSearchTerm))
            {
                query = query.Where(c => c.Artist.FullName.Contains(cardSearch.ArtistSearchTerm));
            }

            if (cardSearch.CardType != 0)
            {
                query = query.Where(c => c.CardTypes.Any(t => t.TypeId == cardSearch.CardType));
            }

            if (!string.IsNullOrEmpty(cardSearch.SetCode))
            {
                query = query.Where(c => c.SetCode == cardSearch.SetCode);
            }

            if (!string.IsNullOrEmpty(cardSearch.RarityCode))
            {
                query = query.Where(c => c.RarityCode == cardSearch.RarityCode);
            }

            if (cardSearch.Colors != null && cardSearch.Colors.Length > 0)
            {
                query = query.Include(c => c.CardColors)
                             .ThenInclude(cc => cc.Color)
                             .Where(c => c.CardColors.Any(cc => cardSearch.Colors.Contains(cc.Color.Name)));
            }

            return Util.Paginate(query, pageNumber, pageSize).ToList();
        }

        public IList<Card> GetAllCards(int pageNumber, int pageSize, string sortOrder)
        {
            return Util.Paginate(Util.OrderBySortOrder(dbContext.Cards, sortOrder), pageNumber, pageSize).ToList();
        }

        public IList<Type> GetAllTypes()
        {
            var cardTypeIds = dbContext.CardTypes.Select(ct => ct.TypeId).ToList();
            return dbContext.Types.Where(t => cardTypeIds.Contains(t.Id)).OrderBy(t => t.Name).ToList();
        }
        public IList<Set> GetAllSets()
        {
            return dbContext.Sets.OrderBy(s => s.Id).ToList();
        }

        public IList<Rarity> GetAllRarities()
        {
            return dbContext.Rarities.OrderBy(r => r.Id).ToList();
        }
    }

}
