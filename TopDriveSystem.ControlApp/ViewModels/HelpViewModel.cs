using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Splat;

namespace TopDriveSystem.ControlApp.ViewModels
{
    [DataContract]
    public class HelpViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly ReactiveCommand<Unit, Unit> _search;
        private string _searchQuery;

        public HelpViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            var canSearch = this
                .WhenAnyValue(x => x.SearchQuery)
                .Select(query => !string.IsNullOrWhiteSpace(query));

            _search = ReactiveCommand.CreateFromTask(
                () => Task.Delay(1000),
                canSearch);
        }

        public ICommand Search => _search;

        [DataMember]
        public string SearchQuery
        {
            get => _searchQuery;
            set => this.RaiseAndSetIfChanged(ref _searchQuery, value);
        }


        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/search";
    }
}