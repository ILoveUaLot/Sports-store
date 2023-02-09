using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(new CategoryRouteDataModel
            {
                Categories = repository.Products
                            .Select(p => p.Category)
                            .Distinct()
                            .OrderBy(p => p),
                RoutePath = RouteData?.Values["category"]
            });
        }
    }
}
