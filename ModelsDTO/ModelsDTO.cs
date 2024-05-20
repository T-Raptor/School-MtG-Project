namespace MTG_Project.ModelsDTO
{
    public record CardModel(long IdCard, string ImageInfo, string CardName, string ArtistName) { }
    public record ArtistModel(long Id, string Name) { }
}
