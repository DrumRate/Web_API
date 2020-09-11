using System;
using System.Threading.Tasks;

namespace WPFApp.Commands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> callback;

        public AsyncRelayCommand(Func<Task> callback) 
        {
            this.callback = callback;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await callback();
        }
    }
}
