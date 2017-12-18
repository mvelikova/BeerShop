
using BeerShop.Data.Models.Mapping;

namespace BeerShop.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BeerShop.Data.Models;

    public interface IBeerService:IGenericDataService<Beer>
    {
        List<Data.Models.Type> GetAllBeerTypes();
        List<Ingredient> GetAllBeerIngredients();
        void AddIngredient(string name);
        void AddType(string name);
        List<Data.Models.Type> GetBeerTypes(int id);
        List<Ingredient> GetBeerIngredients(int id);
        void AddIngredientToBeer(int id, string name);
    }
}
