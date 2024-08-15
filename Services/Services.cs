using Microsoft.EntityFrameworkCore;
using MTG_Project.Models;
using MTG_Project.ModelsDTO;

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
                                    int cardType = 0,
                                    int setCode = 0,
                                    string rarityCode = "",
                                    string[] colors = null)
        {
            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            Console.WriteLine("CardLetters null or empty? " + string.IsNullOrEmpty(cardLetters));
            Console.WriteLine("ArtistLetters null or empty? " + string.IsNullOrEmpty(artistLetters));
            Console.WriteLine("CardType null? " + (cardType == 0));
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
                query = query.Where(c => c.CardTypes.Any(ct => ct.TypeId == cardType));
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

        public IQueryable GetCardsByArtist(int idArtist)
        {
            return dbContext.Cards.Where(c => c.ArtistId == idArtist);
        }

        public IList<CardModel> GetAllCardsArtists()
        {
            return dbContext.Cards.Include(c => c.Artist)
                                  .Select(c => new CardModel(c.Id, c.Image, c.Name, c.Artist.FullName))
                                  .ToList();
        }
    }

}
