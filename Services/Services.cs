using Microsoft.EntityFrameworkCore;
using MTG_Project.Models;
using MTG_Project.ModelsDTO;

namespace MTG_Project.Services
{
    public class Services
    {
        readonly MyDBContext dbContext = new();

        public Services() { }

        public IList<Card> GetAllCards(int pageNumber, int pageSize)
        {
            return dbContext.Cards
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Card> GetAllCardsWith(string letters)
        {
            return dbContext.Cards.Where(x => x.Name.Contains(letters)).ToList();
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
