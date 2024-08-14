using Microsoft.EntityFrameworkCore;
using MTG_Project.Models;
using MTG_Project.ModelsDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MTG_Project.Services
{
    public class Services
    {
        readonly MyDBContext dbContext = new();

        public Services() { }
        
        /*public IList<Card> GetCards(string sortOrder = "Id", string cardLetters = "", string artistLetters = "", int cardType = 0, int setCode = 0, string rarityCode = "", int[] colors = null, bool colorsExact = false, int pageNumber, int pageSize)
        {

        }*/

        public IList<Card> GetAllCards(int pageNumber, int pageSize, string sortOrder)
        {
            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            return Util.Paginate(query, pageNumber, pageSize).ToList();
        }

        public IList<Card> GetAllCardsWith(string letters, int pageNumber, int pageSize, string sortOrder)
        {
            Console.WriteLine(letters);
            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            query = query.Where(x => x.Name.Contains(letters));

            return Util.Paginate(query, pageNumber, pageSize).ToList();
        }

        public IList<Card> GetAllCardsWithArtist(string artistLetters, int pageNumber, int pageSize, string sortOrder)
        {
            Console.WriteLine(artistLetters);

            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            query = query.Where(c => c.Artist.FullName.Contains(artistLetters));

            return Util.Paginate(query, pageNumber, pageSize).ToList();
        }
        
        public IList<Card> GetAllCardsWithAndWithArtist(string cardLetters, string artistLetters, int pageNumber, int pageSize, string sortOrder)
        {
            Console.WriteLine(cardLetters, artistLetters);

            IQueryable<Card> query = Util.OrderBySortOrder(dbContext.Cards, sortOrder);

            query = query
                .Where(c => c.Artist.FullName.Contains(artistLetters))
                .Where(c => c.Name.Contains(cardLetters));

            return Util.Paginate(query, pageNumber, pageSize).ToList();
        }

        public IQueryable GetCardsByArtist(int idArtist)
        {

            IQueryable cardsByArtist = dbContext.Cards.Include(c => c.Artist)
                .Where(c => c.Artist.Id == idArtist && c.OriginalImageUrl != null)
                .OrderBy(o => o.Name)
               .Select(p => new CardModel
                (
                    p.Id,
                    p.OriginalImageUrl,
                    p.Name,
                    p.Artist.FullName
                ))
               .Take(20);

            return cardsByArtist;
        }

        public IList<CardModel> GetAllCardsArtists()
        {
            IList<CardModel> cardsArtist = dbContext.Cards.Join(dbContext.Artists,
                   card => card.ArtistId,
                   artist => artist.Id,
                   (card, artist) => new
                   {
                       card.Id,
                       card.OriginalImageUrl,
                       card.Name,
                       artist.FullName
                   }).Select(p => new CardModel
                   (
                       p.Id,
                       p.OriginalImageUrl,
                       p.Name,
                       p.FullName
                   )).ToList();

            return cardsArtist;
        }
    }

}
