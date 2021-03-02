using Autofac;
using Autofac.Integration.WebApi;
using Real_Estate_Rest_API.Data;
using Real_Estate_Rest_API.objects_Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Real_Estate_Rest_API.App_Start
{
    public class AutofacConfig
    {

        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {

            bldr.RegisterType<RealEstateContext>()
              .InstancePerRequest();

            bldr.RegisterType<RealEstateRepo>()
              .As<IRealEstateRepo>()
              .InstancePerRequest();

            bldr.RegisterType<ObjectsMapper>()
                .As<IObjectsMapper>()
                .InstancePerRequest();
        }

    }
}