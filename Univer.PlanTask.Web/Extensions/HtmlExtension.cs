using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Univer.PlanTask.Web
{
    public static class HtmlExtension
    {
        /// <summary>
        /// Текущее время
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString CurrentTime(this HtmlHelper html)
        {
            var div = $"<div class=\"info\">{DateTime.Now}</div>";
            return MvcHtmlString.Create(div);
        }
    }
}