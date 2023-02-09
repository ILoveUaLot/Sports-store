using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Models.ViewModels
{
    public class CategoryRouteDataModel
    {
        public IEnumerable<string>? Categories { get; set; }

        public string? RoutePath { get; set; }
        
    }
}
