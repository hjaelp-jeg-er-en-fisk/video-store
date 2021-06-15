using System;
using System.Reflection;
using LightInject;
using VideoStore.Services;
using VideoStore.Services.Implement;

//Blatantly stolen from https://www.lightinject.net/#unit-testing
public class ContainerFixture : IDisposable
{
    public ContainerFixture()
    {
        var container = CreateContainer();
        Configure(container);
        container.Register<IRentalService, RentalService>();
        ServiceFactory = container.BeginScope();
        InjectPrivateFields();
    }

    private void InjectPrivateFields()
    {
        var privateInstanceFields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var privateInstanceField in privateInstanceFields)
        {
            //Small add to allow our customer instance to live.
            if (privateInstanceField.FieldType.IsInterface)
                privateInstanceField.SetValue(this, GetInstance(ServiceFactory, privateInstanceField));
        }
    }

    internal Scope ServiceFactory { get; }

    public void Dispose() => ServiceFactory.Dispose();

    public TService GetInstance<TService>(string name = "")
        => ServiceFactory.GetInstance<TService>(name);

    private object GetInstance(IServiceFactory factory, FieldInfo field)
        => ServiceFactory.TryGetInstance(field.FieldType) ?? ServiceFactory.GetInstance(field.FieldType, field.Name);

    internal virtual IServiceContainer CreateContainer() => new ServiceContainer();

    internal virtual void Configure(IServiceRegistry serviceRegistry) { }
}