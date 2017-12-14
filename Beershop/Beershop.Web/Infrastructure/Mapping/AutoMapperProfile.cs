using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using BeerShop.Data.Models;
using BeerShop.Web.Areas.Beers.Models;

namespace BeerShop.Web.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Beer, EditBeerViewModel>();
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains("BeerShop"))
                .SelectMany(a => a.GetTypes());

            allTypes
                .Where(t => t.IsClass && !t.IsAbstract && t
                                .GetInterfaces()
                                .Where(i => i.IsGenericType)
                                .Select(i => i.GetGenericTypeDefinition())
                                .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                        .GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapFrom<>))
                        .SelectMany(i => i.Arguments)
                        .First(),
                })
                .ToList()
                .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            allTypes
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToList()
                .ForEach(mapping => mapping.Configure(this));
        }
//        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
//            (this IMappingExpression<TSource, TDestination> expression)
//        {
//            var flags = BindingFlags.Public | BindingFlags.Instance;
//            var sourceType = typeof(TSource);
//            var destinationProperties = typeof(TDestination).GetProperties(flags);
//
//            foreach (var property in destinationProperties)
//            {
//                if (sourceType.GetProperty(property.Name, flags) == null)
//                {
//                    expression.ForMember(property.Name, opt => opt.Ignore());
//                }
//            }
//            return expression;
//        }
    }
}