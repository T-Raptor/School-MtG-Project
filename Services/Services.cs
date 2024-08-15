using Microsoft.EntityFrameworkCore;
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
                                    string cardLetters,
                                    string artistLetters,
                                    int cardType,
                                    int setCode = 0,
                                    string rarityCode = "",
                                    string[] colors = null)
        {
            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            Console.WriteLine("CardLetters null or empty? " + string.IsNullOrEmpty(cardLetters));
            Console.WriteLine("ArtistLetters null or empty? " + string.IsNullOrEmpty(artistLetters));
            Console.WriteLine("CardType null? " + (cardType == null));
            Console.WriteLine("SetCode: " + setCode);
            Console.WriteLine("SetCode null? " + (setCode == 0));
            Console.WriteLine("RarityCode: " + rarityCode);
            Console.WriteLine("RarityCode null or empty? " + string.IsNullOrEmpty(rarityCode));
            Console.WriteLine("Colors: " + colors);
            Console.WriteLine("Colors null? " + (colors == null));

            if (!string.IsNullOrEmpty(cardLetters))
            {
                query = query.Where(c => c.Name.Contains(cardLetters));
            }

            if (!string.IsNullOrEmpty(artistLetters))
            {
                query = query.Where(c => c.Artist.FullName.Contains(artistLetters));
            }

            if (cardType != 0)
            {
                Console.WriteLine("CardType: " + cardType);
                query = query.Where(c => c.CardTypes.Any(t => t.TypeId == cardType));
            }

            if (setCode != 0)
            {
                query = query.Where(c => c.SetCode == setCode.ToString());
            }

            if (!string.IsNullOrEmpty(rarityCode))
            {
                query = query.Where(c => c.RarityCode == rarityCode);
            }

            if (colors != null && colors.Length > 0)
            {
                query = query.Include(c => c.CardColors)
                             .ThenInclude(cc => cc.Color)
                             .Where(c => c.CardColors.Any(cc => colors.Contains(cc.Color.Name)));
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
