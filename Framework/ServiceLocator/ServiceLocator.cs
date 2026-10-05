using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly ConcurrentDictionary<Type, IService> _container
        = new ConcurrentDictionary<Type, IService>();

    public static bool Add<T>(T service) where T : class, IService
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));

        Type key = typeof(T);
        if (_container.TryAdd(key, service) == false)
            return false;

        return true;
    }

    public static bool Remove<T>(T service) where T : class, IService
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));

        Type key = typeof(T);
        if (_container.TryGetValue(key, out IService existingService) == false)
            return false;

        if (ReferenceEquals(existingService, service) == true)
            return _container.TryRemove(key, out existingService);

        return false;
    }

    public static bool Get<T>(out T service) where T : class, IService
    {
        Type key = typeof(T);
        if (_container.TryGetValue(key, out IService existingService) == false)
        {
            service = default(T);
            return false;
        }

        service = (T)existingService;
        return true;
    }

    public static void Clear()
    {
        _container.Clear();
    }
}
