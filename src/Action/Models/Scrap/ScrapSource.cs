namespace Action.Models.Scrap
{
    public class ScrapSource
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Url { get; set; }
        public int Dept { get; set; } = 3;
    }
}