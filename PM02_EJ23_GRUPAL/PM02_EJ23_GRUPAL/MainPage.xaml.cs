using Plugin.AudioRecorder;
using PM02_EJ23_GRUPAL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM02_EJ23_GRUPAL
{
    public partial class MainPage : ContentPage
    {

        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        public String myDate = DateTime.Now.ToString();

        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDbAudio.db3");

        public MainPage()
        {
            InitializeComponent();
        }

        async void recordAudio_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
            var status2 = await Permissions.RequestAsync<Permissions.StorageRead>();
            var status3 = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status != PermissionStatus.Granted || status2 != PermissionStatus.Granted || status3 != PermissionStatus.Granted)
                return;

            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                audioPlayer.Play(audioRecorderService.GetAudioFilePath());

                var audioPath = audioRecorderService.GetAudioFilePath();

                var db = new SQLiteConnection(dbPath);
                db.CreateTable<AudioModel>();

                var maxPK = db.Table<AudioModel>().OrderByDescending(c => c.Id).FirstOrDefault();

                AudioModel audioModel = new AudioModel()
                {
                    Id = (maxPK == null ? 1 : maxPK.Id + 1),
                    FechaHora = myDate,
                    AudioDecod = audioPath
                };

                db.Insert(audioModel);

                await DisplayAlert(null, "Grabación Guardada " + audioPath, "OK");
            }
            else
            {
                await audioRecorderService.StartRecording();
                await DisplayAlert(null, "Grabando", "OK");
            }
        }

        async void savedAudios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordedAudios());
        }
    }
}
