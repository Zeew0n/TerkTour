using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using TrekTour.Areas.Admin.Models;

namespace TrekTour
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            MapConfiguration.ConfigureAutoMap();
        }
    }



    public static class MapConfiguration
    {
        public static void ConfigureAutoMap()
        {
            AutoMapper.Mapper.CreateMap<ContentsModels, Contents>().IgnoreAllNonExisting();
            AutoMapper.Mapper.CreateMap<Contents, ContentsModels>().IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<NewsRecordsModel, NewsRecords>().IgnoreAllNonExisting();
            AutoMapper.Mapper.CreateMap<NewsRecords, NewsRecordsModel>().IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<QuotesRecordsModel, QuotesRecords>().IgnoreAllNonExisting();
            AutoMapper.Mapper.CreateMap<QuotesRecords, QuotesRecordsModel>().IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<TestimonialsRecordsModel, TestimonialsRecords>().IgnoreAllNonExisting();
            AutoMapper.Mapper.CreateMap<TestimonialsRecords, TestimonialsRecordsModel>().IgnoreAllNonExisting();

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}