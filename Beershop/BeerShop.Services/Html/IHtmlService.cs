using System;
using System.Collections.Generic;
using System.Text;

namespace BeerShop.Services.Html
{
    public interface IHtmlService
    {
        string Sanitize(string htmlContent);
    }
}
