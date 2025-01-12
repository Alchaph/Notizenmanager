using System.Windows.Input;
using Microsoft.Maui.Controls;
using NotizenManager.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotizenManager
{
    public partial class SubjectView : ContentPage
    {
        private Subject subject;
        public ICommand BackCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteFileCommand { get; }

        private string searchText;
        private Color searchBarBackgroundColor = Colors.LightGray;
        private bool isSortAlphabetically;

        public bool IsSortAlphabetically
        {
            get => isSortAlphabetically;
            set
            {
                if (isSortAlphabetically != value)
                {
                    isSortAlphabetically = value;
                    OnPropertyChanged(nameof(IsSortAlphabetically));
                    FilterFiles();
                }
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterFiles();
                }
            }
        }

        public ObservableCollection<string> FilteredFiles { get; }

        public SubjectView(Subject subject)
        {
            InitializeComponent();
            this.subject = subject;
            Title = subject.Name;
            BackCommand = new Command(OnBackButtonClicked);
            AddCommand = new Command(OnAddFile);
            DeleteFileCommand = new Command<string>(OnDeleteFile);
            FilteredFiles = new ObservableCollection<string>(subject.Files);
            BindingContext = this;
        }

        public Color SearchBarBackgroundColor
        {
            get => searchBarBackgroundColor;
            set
            {
                if (searchBarBackgroundColor != value)
                {
                    searchBarBackgroundColor = value;
                    OnPropertyChanged(nameof(SearchBarBackgroundColor));
                }
            }
        }

        public string SubjectName => subject.Name;

        public ObservableCollection<string> Files => subject.Files;

        private async void OnBackButtonClicked()
        {
            await Navigation.PopModalAsync();
        }

        private async void OnAddFile()
        {
            var result = await FilePicker.PickAsync();
            if (result != null)
            {
                var filePath = result.FullPath;
                if (File.Exists(filePath))
                {
                    subject.Files.Add(filePath);
                    FilteredFiles.Add(filePath);
                }
                else
                {
                    await DisplayAlert("Error", "Selected file does not exist", "OK");
                }
            }
        }

        private async void OnDeleteFile(string fileName)
        {
            bool confirm = await DisplayAlert("Löschen bestätigen",
                "Sind Sie sicher, dass Sie diese Datei löschen möchten?", "Ja", "Nein");
            if (confirm)
            {
                if (subject.Files.Contains(fileName))
                {
                    subject.Files.Remove(fileName);
                    FilteredFiles.Remove(fileName);
                }
            }
        }

        private void OnFileSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                string selectedFile = e.CurrentSelection[0] as string;
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    OpenFile(selectedFile);
                }
            }
        }

        private void OpenFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    Debug.WriteLine($"Attempting to open file: {fileName}");
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fileName,
                        UseShellExecute = true
                    });
                    Debug.WriteLine("Process started successfully.");
                }
                else
                {
                    Debug.WriteLine($"File does not exist: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening file: {ex.Message}");
            }
        }

        private void FilterFiles()
        {
            FilteredFiles.Clear();
            var files = subject.Files.Where(file => string.IsNullOrEmpty(SearchText) ||
                                                    file.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

            if (IsSortAlphabetically)
            {
                files = files.OrderBy(file => file);
            }

            foreach (var file in files)
            {
                FilteredFiles.Add(file);
            }

            SearchBarBackgroundColor = FilteredFiles.Count == 0 ? Colors.Red : Colors.LightGray;
        }
    }
}