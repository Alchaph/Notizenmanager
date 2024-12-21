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
                await Navigation.PushModalAsync(new SubjectView(selectedSubject));
            }
        }

        private async void OnEdit(Subject subject)
        {
            string newName = await DisplayPromptAsync("Edit Subject", "Enter new name:", initialValue: subject.Name);
            if (!string.IsNullOrWhiteSpace(newName))
            {
                subject.Name = newName;
            }
        }

        private void OnDelete(Subject subject)
        {
            Subjects.Remove(subject);
        }
    }
}