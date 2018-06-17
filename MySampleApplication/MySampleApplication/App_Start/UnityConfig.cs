using MySampleApplication.Services;
using MySampleApplication.Services.Cryptography;
using MySampleApplication.Services.Interfaces;
using Unity;
using Unity.Lifetime;

namespace MySampleApplication.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ICryptographyService, Base64StringCryptographyService>(new ContainerControlledLifetimeManager());
        }
    }
}