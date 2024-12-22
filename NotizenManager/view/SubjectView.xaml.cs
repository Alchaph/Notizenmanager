using System.Windows.Input;
using Microsoft.Maui.Controls;
using NotizenManager.Models;

namespace NotizenManager
{
    public partial class SubjectView : ContentPage
    {
        string subjectName;
        public ICommand BackCommand { get; }
        public string SubjectName
        {
            get { return subjectName; }
        }

        public SubjectView(Subject subject)
        {
            InitializeComponent();
            BackCommand = new Command(OnBackButtonClicked);
            BindingContext = this;
            subjectName = subject.Name;
        }

        private async void OnBackButtonClicked()
        {
            await Navigation.PopModalAsync();
        }
    }
}