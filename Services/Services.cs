using Microsoft.EntityFrameworkCore;
using MTG_Project.Models;
using MTG_Project.ModelsDTO;

namespace MTG_Project.Services
{
    public class Services
    {
        readonly MyDBContext dbContext = new();

        public Services() { }

        //GetCards(string sortOrder, string cardLetters, string artistLetters, int cardType, int setCode, string rarityCode, int[] colors, bool colorsExact, int pageNumber, int pageSize))

        public IList<Card> GetAllCards(int pageNumber, int pageSize)
        {
            IQueryable<Card> query = dbContext.Cards;

            /*
            query = Util.OrderBySortOrder(query, sortOrder);

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            */

            query = query.OrderBy(c => c.Id);

            return Util.Paginate(query, pageNumber, pageSize).ToList();
        }

        public IList<Card> GetAllCardsWith(string letters, int pageNumber, int pageSize)
        {
            Console.WriteLine(letters);
            return dbContext.Cards.Where(x => x.Name.Contains(letters))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Card> GetAllCardsWithArtist(string artistLetters, int pageNumber, int pageSize)
        {
            Console.WriteLine(artistLetters);
            return dbContext.Cards.Where(c => c.Artist.FullName.Contains(artistLetters))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        public IList<Card> GetAllCardsWithAndWithArtist(string cardLetters, string artistLetters, int pageNumber, int pageSize)
        {
            Console.WriteLine(cardLetters, artistLetters);
            return dbContext.Cards
                .Where(c => c.Artist.FullName.Contains(artistLetters))
                .Where(c => c.Name.Contains(cardLetters))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
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
