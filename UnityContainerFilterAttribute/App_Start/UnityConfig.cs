using System;
using UnityContainerFilterAttribute.Extensions;
using Unity;
using UnityContainerFilterAttribute.Infrastructure;
using System.Web.Mvc;

namespace UnityContainerFilterAttribute
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var containerLazy = new UnityContainer();
              RegisterTypes(containerLazy);
              return containerLazy;
          });

        /// <summary>s
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterMapper();

            container.RegisterMappingProfile<MappingProfile>();
            container.RegisterType<IActionExecutionTimerService, ActionExecutionTimerService>();
            container.RegisterInstance<IFilterProvider>("FilterProvider", new FilterProvider(container));

            container.RegisterInstance<IActionFilter>("ActionExecutionTimerFilter", 
                new ActionExecutionTimerFilter((IActionExecutionTimerService)container.Resolve(typeof(IActionExecutionTimerService))));
          //  IActionFilter builtupDataService = container.BuildUp<IActionFilter>( myDataService, "ActionExecutionTimerFilter");
        }
    }
}