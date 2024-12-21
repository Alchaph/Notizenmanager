using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using NotizenManager.Models;

namespace NotizenManager
{
    public partial class SubjectView : ContentPage
    {
        public ObservableCollection<Document> Documents { get; set; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public SubjectView(Subject subject)
        {
            InitializeComponent();

            Documents = new ObservableCollection<Document>
            {
                new Document { Title = "Beispiel Dokument" },
            };

            AddCommand = new Command(OnAdd);
            EditCommand = new Command<Document>(OnEdit);
            DeleteCommand = new Command<Document>(OnDelete);

            BindingContext = subject;
        }

        private void OnAdd()
        {
            Documents.Add(new Document { Title = "Neues Dokument" });
        }

        private async void OnEdit(Document document)
        {
            string newTitle = await DisplayPromptAsync("Edit Document", "Enter new title:", initialValue: document.Title);
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                document.Title = newTitle;
            }
        }

        private void OnDelete(Document document)
        {
            Documents.Remove(document);
        }
    }
}