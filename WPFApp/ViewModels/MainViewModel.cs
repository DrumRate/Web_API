using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Commands;
using WPFApp.Models;
using WPFApp.Services;

namespace WPFApp.ViewModels
{
    public class MainViewModel :  INotifyPropertyChanged
    {
        int j;
        public MainViewModel(IRepository repository)
        {
            this.repository = repository;
            ShowEventsCommand = new AsyncRelayCommand(ShowEventsCommandExecuted);
            PageUpCommand = new AsyncRelayCommand(PageUpCommandExecuted);
            PageDownCommand = new AsyncRelayCommand(PageDownCommandExecuted);
        }

        private ObservableCollection<Event> events = new ObservableCollection<Event>();
        private readonly IRepository repository;


        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }
        public ICommand ShowEventsCommand { get; }
        public ICommand PageUpCommand { get; }
        public ICommand PageDownCommand { get; }
        public ICommand UpdateEventsCommand { get; }

     
        private async Task ShowEventsCommandExecuted()
        {
            j = 0;
            var eventsDb = await repository.GetEvents(j);
            Events = new ObservableCollection<Event>(eventsDb);
        }

        private async Task PageUpCommandExecuted()
        {
            j++;
            var eventsDb = await repository.GetEvents(j);
            Events = new ObservableCollection<Event>(eventsDb);
        }
        private async Task PageDownCommandExecuted()
        {
            if (j > 0) j--;
            var eventsDb = await repository.GetEvents(j);
            Events = new ObservableCollection<Event>(eventsDb);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
