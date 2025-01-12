using Microsoft.Maui.Controls;

namespace NotizenManager
{
    public partial class InfoPage : ContentPage
    {
        private const string RatingKey = "UserRating";

        public InfoPage()
        {
            InitializeComponent();
            LoadRating(); // Load saved rating on initialization
        }

        private void LoadRating()
        {
            if (Preferences.ContainsKey(RatingKey))
            {
                var savedRating = Preferences.Get(RatingKey, 5.0); // Default to 5.0 if not found
                ratingSlider.Value = savedRating;
            }
        }

        private void OnSendButtonClicked(object sender, EventArgs e)
        {
            var rating = ratingSlider.Value;
            Preferences.Set(RatingKey, rating); // Save the rating
            DisplayAlert("Feedback erhalten.", $"Danke für deine Bewertung: {rating:F0}!", "OK");
        }

        private void OnGitHubLinkTapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://github.com/Alchaph?tab=repositories"));
        }
    }
}