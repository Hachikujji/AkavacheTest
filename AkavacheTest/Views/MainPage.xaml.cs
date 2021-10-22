using Akavache;
using AkavacheTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AkavacheTest
{
    public partial class MainPage : Page
    {
        /// <summary>
        /// Collection of notes
        /// </summary>
        public ObservableCollection<Note> Notes { get; set; }

        /// <summary>
        /// User can input text
        /// </summary>
        public string UsersText { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            // just visual changes
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(400, 800));
            ApplicationView.PreferredLaunchViewSize = new Size(400, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            UsersText = String.Empty;
            Notes = new ObservableCollection<Note>();
            // setup akavache
            // Cache folder
            // %user%\AppData\Local\Packages\[package name]\LocalState\BlobCache 
            BlobCache.ApplicationName = "Akavache test app";

        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var cachedNotes = await GetCachedNotesAsync();
            foreach (var note in cachedNotes)
                Notes.Add(note);
        }

        private async Task<IEnumerable<Note>> GetCachedNotesAsync()
        {
            var cachedNotes = await BlobCache.LocalMachine.GetAllObjects<Note>();
            return cachedNotes;
        }

        private async void AddToCacheClicked(object sender, RoutedEventArgs e)
        {
            // create new note
            var key = Guid.NewGuid();
            var newNote = new Note() { Id = key,Text = UsersText, DateTimeOffset = DateTimeOffset.UtcNow };
            Notes.Add(newNote);
            // add note to cache
            await BlobCache.LocalMachine.InsertObject(key.ToString(), newNote);

        }

        private async void NoteClicked(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            var note = (Note)listView.SelectedItem;
            if (note!=null)
            {
                Notes.Remove(note);
                // delete note from cache by key
                await BlobCache.LocalMachine.Invalidate(note.Id.ToString());
            }
        }

        async protected void OnSuspending(object sender, SuspendingEventArgs args)
        {
            await BlobCache.Shutdown();
        }

    }
}
