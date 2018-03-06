﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace UnityContainerFilterAttribute.Extensions
{
    public static class RegisterAutoMapperExtension
    {
        private static IUnityContainer RegisterMappingProfile(IUnityContainer container, Type profileType)
        {
            return container.RegisterType(typeof(Profile), profileType, profileType.FullName,
                new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterMappingProfileType(IUnityContainer container, Type profileType)
        {
            return container.RegisterType(typeof(Profile), profileType, profileType.FullName,
                new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterMappingProfilesFromAssembly(this IUnityContainer container,
            Assembly assembly)
        {
            foreach (var type in GetAccessibleTypes(assembly)
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && typeof(Profile).IsAssignableFrom(t)))
            {
                RegisterMappingProfile(container, type);
            }

            return container;
        }

        public static IUnityContainer RegisterMappingFromAllLoadedAssemblies(this IUnityContainer container,
            string namespaceMatch)
        {
            try
            {
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var t in a.GetTypes())
                    {
                        if (t.Namespace != null && t.Namespace.StartsWith(namespaceMatch))
                        {
                            RegisterMappingProfilesFromAssembly(container, a);
                            break;
                        }
                    }
                }
            }
            // ReSharper disable once UnusedVariable
            catch (AppDomainUnloadedException appDomainUnloadedException)
            {
                // continue
            }
            // ReSharper disable once UnusedVariable
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                // continue
            }

            return container;
        }

        public static IUnityContainer RegisterMappingProfile<T>(this IUnityContainer container)
            where T : Profile
        {
            return RegisterMappingProfile(container, typeof(T));
        }

        public static IUnityContainer RegisterMapper(this IUnityContainer container)
        {
            return container
                .RegisterType<MapperConfiguration>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionFactory(c =>
                        new MapperConfiguration(configuration =>
                        {
                            configuration.ConstructServicesUsing(t => container.Resolve(t));
                            foreach (var profile in c.ResolveAll<Profile>())
                            {
                                configuration.AddProfile(profile);
                            }
                        })))
                .RegisterType<IConfigurationProvider>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionFactory(c => c.Resolve<MapperConfiguration>()))
                .RegisterType<IMapper>(
                    new InjectionFactory(c => c.Resolve<MapperConfiguration>().CreateMapper()));
        }

        private static IEnumerable<Type> GetAccessibleTypes(this Assembly assembly)
        {
            try
            {
                return assembly.DefinedTypes.Select(t => t.AsType());
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
        }
    }
}