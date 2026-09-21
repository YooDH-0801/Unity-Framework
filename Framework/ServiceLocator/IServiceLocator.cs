using System;

public interface IServiceLocator
{
    void Register<Type>(IService service) where Type : IService;
}