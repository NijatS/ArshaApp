using Arsha.Core.Models;

namespace Arsha.App.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<PortfolioCategory> Categories { get; set; }
        public IEnumerable<PortfolioItem> Items { get; set; }

    }
}
