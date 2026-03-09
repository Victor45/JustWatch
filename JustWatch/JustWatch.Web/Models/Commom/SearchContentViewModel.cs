
namespace JustWatch.Web.Models.Commom
{
    public class SearchContentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterURL { get; set; } = string.Empty;
        public int Info { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
