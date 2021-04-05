namespace Learn.Helix.Foundation.ORM.Models
{
    using Sitecore.XA.Foundation.Mvc.Models;

    public class RenderingGlassModel<T> : RenderingModelBase where T : class
    {
        public T GlassModel { get; set; }
    }
}