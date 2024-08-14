using MTG_Project.Models;

namespace MTG_Project.Services
{
    public class Util
    {
        public static IQueryable<Card> OrderBySortOrder(IQueryable<Card> query, string sortOrder = "")
        {
            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(c => c.Name);
                    break;
                case "artist":
                    query = query.OrderBy(c => c.Artist.FullName);
                    break;
                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }
            return query;
        }

        public static IQueryable<Card> Paginate(IQueryable<Card> query, int pageNumber, int pageSize)
        {
            Console.WriteLine("Succes");
            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
