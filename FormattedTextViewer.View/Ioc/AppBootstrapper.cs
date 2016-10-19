using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using FormattedTextViewer.View.Services;

namespace FormattedTextViewer.View.Ioc
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DetectTextFormatService>().As<IDetectTextFormatService>();
            builder.RegisterType<TextFormattingFactory>().As<ITextFormattingFactory>();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();

            container = builder.Build();
        }
        /*
        protected override object GetInstance(Type serviceType, string key)
        {
            return container.Resolve(serviceType, new Autofac.Core.Parameter[] { key });
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.b(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }*/

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var viewModel = container.Resolve<IMainWindowViewModel>();
            IWindowManager windowManager;
            try
            {
                windowManager = IoC.Get<IWindowManager>();
            }
            catch
            {
                windowManager = new WindowManager();
            }
            windowManager.ShowWindow(viewModel);
        }

    }
}
