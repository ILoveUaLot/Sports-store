using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Models.ViewModels
{
    public class CategoryRouteDataModel
    {
        public IEnumerable<string>? Categories { get; set; }

        public object? RoutePath { get; set; }
        
    }
}
