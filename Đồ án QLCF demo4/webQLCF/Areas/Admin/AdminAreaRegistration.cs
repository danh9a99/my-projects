using System.Web.Mvc;

namespace webQLCF.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                  name: "Cập nhật thông tin",
                  url: "tai-khoan/cap-nhat-thong-tin",
                  defaults: new { controller = "Product", action = "Edit", id = UrlParameter.Optional }
              );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}