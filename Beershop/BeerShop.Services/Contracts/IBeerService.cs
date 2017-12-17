
namespace BeerShop.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BeerShop.Data.Models;

    public interface IBeerService:IGenericDataService<Beer>
    {
        List<Beer> GetAllWithComments();
        Beer GetSignleWithComments(int? id);
    }
}
