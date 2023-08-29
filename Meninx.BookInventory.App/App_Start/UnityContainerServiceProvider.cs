﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Hosting;
using Unity;

namespace Meninx.BookInventory.App
{
    internal class UnityContainerServiceProvider : IServiceProvider, IRegisteredObject, IDisposable
    {
        private IUnityContainer _container;

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        private const int TypesCannontResolveCacheCap = 100000;

        private readonly ConcurrentDictionary<Type, bool> _typesCannotResolve = new ConcurrentDictionary<Type, bool>();

        internal IDictionary<Type, bool> TypeCannotResolveDictionary
        {
            get { return _typesCannotResolve; }
        }

        internal IServiceProvider NextServiceProvider { get; }

        public UnityContainerServiceProvider(IServiceProvider next, IUnityContainer container)
        {
            NextServiceProvider = next;
            _container = container;

            HostingEnvironment.RegisterObject(this);
        }

        public object GetService(Type serviceType)
        {
            // Try unresolvable types
            if (_typesCannotResolve.ContainsKey(serviceType)) { return DefaultCreateInstance(serviceType); }

            // Try the container
            object result = null;

            try
            { result = Container.Resolve(serviceType); }
            catch (ResolutionFailedException) { } // Ignore and continue

            // Try the next provider
            if (result == null)
            {
                result = NextServiceProvider?.GetService(serviceType);
            }

            // Default activation
            if (result == null && (result = DefaultCreateInstance(serviceType)) != null)
            {
                // Cache it
                if (_typesCannotResolve.Count < TypesCannontResolveCacheCap)
                {
                    _typesCannotResolve.TryAdd(serviceType, true);
                }
            }

            return result;
        }

        public void Stop(bool immediate)
        {
            HostingEnvironment.UnregisterObject(this);

            Container.Dispose();
        }

        protected virtual object DefaultCreateInstance(Type type)
        {
            return Activator.CreateInstance(
                type,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance,
                null,
                null,
                null);
        }

        protected virtual object DefaultCreateInstance(Type type, BindingFlags bindingFlags, Binder binder, object[] args, CultureInfo cultureInfo)
        {
            return Activator.CreateInstance(
                type,
                bindingFlags,
                binder,
                args,
                cultureInfo);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}