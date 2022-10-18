namespace Slipp.Services.BlazorServices
{
    public class PageHistoryState
    {
        private readonly List<string> previousPages;

        public PageHistoryState()
        {
            previousPages = new List<string>();
        }

        public void AddPageToHistory(string pageName)
        {
            previousPages.Add(pageName);
        }

        public string GetGoBackPage()
        {
            if (previousPages.Count > 1)
            {
                return previousPages.ElementAt(previousPages.Count - 2); //BUG - Doesn't work with components. should be ``return previousPages.ElementAt(previousPages.Count - 1);`` when component /JF.
            }

            return previousPages.FirstOrDefault();
        }


    }
}
