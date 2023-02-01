using Xunit;
using Moq;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SportsStore.Tests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            var urlHelper = new Mock<IUrlHelper>();
            //urlHelper contains a sequence of absolute Url path
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");
            
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f =>
                f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            var viewContext = new Mock<ViewContext>();
            PageLinkTagHelper helper =
                new PageLinkTagHelper(urlHelperFactory.Object)
                {
                    PageModel = new PagingInfo
                    {
                        CurrentPage = 2,
                        TotalItems = 28,
                        ItemsPerPage = 10
                    },
                    ViewContext = viewContext.Object,
                    PageAction = "Test"
                };

            TagHelperContext ctx = new TagHelperContext(
                    new TagHelperAttributeList(),
                    new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            //Act
            
        }
    }
}
