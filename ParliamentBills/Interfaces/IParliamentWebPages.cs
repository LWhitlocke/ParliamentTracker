namespace ParliamentBillsCrawler.Interfaces
{
    public interface IParliamentWebPages
    {
        void NavigateToPage();
        string WebPage { get; set; }
    }
}
