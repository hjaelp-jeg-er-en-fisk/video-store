using LightInject;
using VideoStore.Services;
using VideoStore.Services.Implement;

namespace VideoStore
{
    public class DependencyRegistry : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IPriceCalculatorService, PriceCalculatorService>();
            serviceRegistry.Register<IRentalService, RentalService>();
            serviceRegistry.Register<IFrequentRenterPointsService, FrequentRenterPointsService>();
        }
    }
}