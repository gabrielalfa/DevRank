using System;
using System.Web.Mvc;

namespace DevRank.Services
{
    public static class AuthSessionService
    {
        public static int? GetCurrentUserId(Controller controller)
        {
            if (controller.Session["UserId"] != null)
            {
                return (int)controller.Session["UserId"];
            }

            var identity = controller.User == null ? null : controller.User.Identity;

            if (identity != null && identity.IsAuthenticated && int.TryParse(identity.Name, out var userId))
            {
                controller.Session["UserId"] = userId;
                return userId;
            }

            return null;
        }
    }
}
