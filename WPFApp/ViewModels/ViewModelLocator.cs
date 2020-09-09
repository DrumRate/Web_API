using Microsoft.Extensions.DependencyInjection;

namespace WPFApp.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}
