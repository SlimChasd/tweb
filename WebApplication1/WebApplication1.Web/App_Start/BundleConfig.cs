using System.Web;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/css/bootstrap.min.css",
                "~/css/style.css",
                "~/css/responsive.css",
                "~/css/jquery.mCustomScrollbar.min.css"
            ));
            
            bundles.Add(new StyleBundle("~/bundles/css2").Include(
                "~/css/owl.carousel.min.css",
                "~/css/owl.theme.default.min.css",
                "~/css/jquery.fancybox.min.css"
            ));
        }
    }
}