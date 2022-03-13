using Plugin.AudioRecorder;
using PM02_EJ23_GRUPAL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02_EJ23_GRUPAL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordedAudios : ContentPage
    {

        private ListView listView;

        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDbAudio.db3");

        public RecordedAudios()
        {
            InitializeComponent();

            var db = new SQLiteConnection(dbPath);

            StackLayout stackLayout = new StackLayout();

            listView = new ListView();
            listView.ItemsSource = db.Table<AudioModel>().OrderBy(x => x.Id).ToList();
            listView.ItemSelected += listViewItem;
            stackLayout.Children.Add(listView);

            Content = stackLayout;
        }

        private void listViewItem(object sender, SelectedItemChangedEventArgs e)
        {
            AudioModel audioModel = (AudioModel)e.SelectedItem;

            audioPlayer.Play(audioModel.AudioDecod);
        }
    }
}