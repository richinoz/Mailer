using System;
using StructureMap;

namespace ExceptionMailer.Core.Dependency
{
    /// <summary>
    /// Adds basic IOC/DI functionality via a wrapper
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private static bool _dependenciesRegistered;

        /// <summary>
        /// Register all Dependencies across the project.
        /// </summary>
        private static void RegisterDependencies()
        {
            ObjectFactory.Initialize(x => x.Scan(y =>
            {
                y.AssemblyContainingType<CoreRegistry>();
                y.LookForRegistries();
            }));

        }

        private static readonly object Sync = new object();

        internal void ConfigureOnStartup()
        {
            RegisterDependencies();
        }

        /// <summary>
        /// Resolves type via Generics and StructureMap
        /// </summary>
        /// <typeparam name="T">Generic type to be resolved</typeparam>
        /// <returns>Resolved Type</returns>
        public static T Resolve<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }

        /// <summary>
        /// Resolves type via StructureMap
        /// </summary>
        /// <param name="modelType">>type to be resolved</param>
        /// <returns>Resolved Type as object</returns>
        public static object Resolve(Type modelType)
        {
            return ObjectFactory.GetInstance(modelType);
        }

        /// <summary>
        /// Resolves named type via Generics and StructureMap
        /// </summary>
        /// <typeparam name="T">Generic type to be resolved</typeparam>
        /// <param name="key">Named Typed</param>
        /// <returns>Resolved Type</returns>
        public static T Resolve<T>(string key)
        {
            return (T)ObjectFactory.GetNamedInstance(typeof(T), key);
        }

        /// <summary>
        /// Checks as to whether a type has been resolved.
        /// </summary>
        /// <param name="type">type to check against</param>
        /// <returns>bool</returns>
        public bool Registered(Type type)
        {
            EnsureDependenciesRegistered();
            return ObjectFactory.GetInstance(type) != null;
        }

        /// <summary>
        /// Registers Dependencies. If already registered skips
        /// </summary>
        public void EnsureDependenciesRegistered()
        {
            if (_dependenciesRegistered) return;
            lock (Sync)
            {
                if (_dependenciesRegistered) return;
                RegisterDependencies();
                _dependenciesRegistered = true;
            }
        }

        /// <summary>
        /// Gets Current StructureMap container
        /// </summary>
        /// <returns>IContainer</returns>
        public static IContainer GetContainer()
        {
            return ObjectFactory.Container;
        }
    }
}