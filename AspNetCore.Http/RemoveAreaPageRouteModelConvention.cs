using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DashboardCode.AspNetCore
{
    /// <summary>
    /// Alternative to
    /// options.Conventions.AddAreaPageRoute("MyArea", "/Page1", "Page1");
    /// options.Conventions.AddAreaPageRoute("MyArea", "/Page2", "Page2");
    /// </summary>

    public class RemoveAreaPageRouteModelConvention : IPageRouteModelConvention
    {
        readonly string[] areas;
        public RemoveAreaPageRouteModelConvention(params string[] areas)
        {
            this.areas = areas;
        }
        public void Apply(PageRouteModel model)
        {
            foreach (var area in areas)
                if (model.RelativePath.StartsWith("/Areas/" + area))
                {
                    foreach (var selector in model.Selectors)
                    {
                        selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.Replace(area, string.Empty);
                    }
                }
        }
    }
}
