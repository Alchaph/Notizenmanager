<<<<<<< HEAD
﻿using System.Windows.Input;
using Microsoft.Maui.Controls;
=======
﻿using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
>>>>>>> cb50e8c (navigation now works, workin on uploading files)
using NotizenManager.Models;

namespace NotizenManager
{
    public partial class SubjectView : ContentPage, INotifyPropertyChanged
    {
<<<<<<< HEAD
        string subjectName;
        public ICommand BackCommand { get; }
        public string SubjectName
        {
            get { return subjectName; }
        }
=======
        private string subjectName;
        private string searchText;

        public string SubjectName
        {
            get => subjectName;
            set
            {
                if (subjectName != value)
                {
                    subjectName = value;
                    OnPropertyChanged(nameof(SubjectName));
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
                }
            }
        }

        public ObservableCollection<Document> Documents { get; set; }
        public ICommand BackCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
>>>>>>> cb50e8c (navigation now works, workin on uploading files)

        public SubjectView(Subject subject)
        {
            InitializeComponent();
<<<<<<< HEAD
            BackCommand = new Command(OnBackButtonClicked);
            BindingContext = this;
            subjectName = subject.Name;
=======
            BindingContext = this;

            BackCommand = new Command(OnBackButtonClicked);
            AddCommand = new Command(OnAddButtonClicked);
            DeleteCommand = new Command<Document>(OnDelete);
            SearchCommand = new Command(OnSearch);

            Documents = new ObservableCollection<Document>
            {
                new Document { Title = "Example Document" },
            };

            SubjectName = subject.Name;
>>>>>>> cb50e8c (navigation now works, workin on uploading files)
        }

        private async void OnBackButtonClicked()
        {
<<<<<<< HEAD
            await Navigation.PopModalAsync();
=======
            await Navigation.PopAsync();
        }

        private async void OnAddButtonClicked()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select a document",
                    FileTypes = FilePickerFileType.Pdf // You can specify the file types you want to allow
                });

                if (result != null)
                {
                    string filePath = result.FullPath;
                    string documentTitle = Path.GetFileNameWithoutExtension(filePath);

                    // Add the new document to the collection
                    Documents.Add(new Document { Title = documentTitle });

                    // Handle file upload logic here if needed
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file picking
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void OnDelete(Document document)
        {
            if (Documents.Contains(document))
            {
                Documents.Remove(document);
            }
        }

        private void OnSearch()
        {
            //TODO
            // Implement search logic here
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
>>>>>>> cb50e8c (navigation now works, workin on uploading files)
        }
    }
}