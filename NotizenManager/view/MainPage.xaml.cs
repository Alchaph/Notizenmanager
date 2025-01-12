using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using NotizenManager.Models;

namespace NotizenManager
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainPage()
        {
            InitializeComponent();

            Subjects = new ObservableCollection<Subject>
            {
                new Subject { Name = "Beispiel Fach" },
            };

            AddCommand = new Command(OnAdd);
            EditCommand = new Command<Subject>(OnEdit);
            DeleteCommand = new Command<Subject>(OnDelete);

            BindingContext = this;
        }

        private void OnAdd()
        {
            Subjects.Add(new Subject { Name = "Neues Fach" });
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Subject selectedSubject)
            {
                ((CollectionView)sender).SelectedItem = null;
                await Navigation.PushModalAsync(new SubjectView(selectedSubject));
            }
        }

        private async void OnEdit(Subject subject)
        {
            string newName = await DisplayPromptAsync("Fachname ändern", "Geben sie den neuen Namen ein", initialValue: subject.Name);
            if (!string.IsNullOrWhiteSpace(newName))
            {
                subject.Name = newName;
            }
            else
            {
                await DisplayAlert("Error", "Der Fachname darf nicht leer sein", "OK");
            }
        }

        private async void OnDelete(Subject subject)
        {
            bool confirm = await DisplayAlert("Löschen bestätigen",
                "Sind Sie sicher, dass Sie dieses Fach löschen möchten?", "Ja", "Nein");
            if (confirm)
            {
                Subjects.Remove(subject);
            }
        }
    }
}