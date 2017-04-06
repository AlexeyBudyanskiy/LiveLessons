using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LiveLessons.BLL.Interfaces;
using LiveLessons.BLL.Services;
using Ninject;
using NLog;

namespace LiveLesson.WEB.DependencyResolvers
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
			_kernel.Bind<IUserService>().To<UserService>();
			_kernel.Bind<ICourseService>().To<CourseService>();
			_kernel.Bind<IMessageService>().To<MessageService>();
			_kernel.Bind<IAppointmentService>().To<AppointmentService>();
            _kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance);


            _kernel.Bind<ILogger>().ToMethod(p =>
            {
                if (p.Request.Target != null && p.Request.Target.Member.DeclaringType != null)
                {
                    return LogManager.GetLogger(p.Request.Target.Member.DeclaringType.ToString());
                }

                return LogManager.GetLogger("Filter logging");
            });
        }
    }
}