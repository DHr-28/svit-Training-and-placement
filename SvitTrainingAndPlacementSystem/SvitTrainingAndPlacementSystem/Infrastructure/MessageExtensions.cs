using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Infrastructure
{
    public static class MessageExtensions
    {


        public static void ShowMessage(this Controller controller, String messageType, string message)
        {
            if (messageType == "Success")
            {
                message = "<div class='alert alert-success'><strong> Success!</ strong >" +
                   message.ToString() + "</div>";
            }
            else
            {
                message = "<div class='alert alert-danger'><strong> Error!</ strong >" +
                  message.ToString() + "</div>";
            }
            controller.TempData["Notification"] = message;
        }


        public static HtmlString RenderMessages(this HtmlHelper htmlHelper)
        {
            string messages = String.Empty;

            object message = htmlHelper.ViewContext.TempData.ContainsKey("Notification")
                    ? htmlHelper.ViewContext.TempData["Notification"]
                    : null;
            if (message != null)
            {
                messages = message.ToString();
                htmlHelper.ViewContext.TempData["Notification"] = null;
            }
            return MvcHtmlString.Create(messages);
        }

        

    }
}