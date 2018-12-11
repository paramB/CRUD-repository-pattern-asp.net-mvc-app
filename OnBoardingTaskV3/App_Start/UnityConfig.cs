using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using OnBoardingTaskV3.DAL;
using OnBoardingTaskV3.Models;

namespace OnBoardingTaskV3
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRepository<Customer>, Repository<Customer>>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}