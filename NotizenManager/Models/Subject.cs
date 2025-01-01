using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NotizenManager.Models
{
    public class Subject : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<string> files;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public ObservableCollection<string> Files
        {
            get => files;
            set
            {
                if (files != value)
                {
                    files = value;
                    OnPropertyChanged(nameof(Files));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Subject()
        {
            Files = new ObservableCollection<string>();
        }
    }
}