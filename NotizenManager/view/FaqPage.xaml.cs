using Microsoft.Maui.Controls;

namespace NotizenManager
{
    public partial class FaqPage : ContentPage
    {
        public FaqPage()
        {
            InitializeComponent();
        }

        private void OnQuestionTapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            var index = int.Parse(label.GestureRecognizers.OfType<TapGestureRecognizer>().First().CommandParameter.ToString());
            var answerLabel = this.FindByName<Label>($"Answer{index}");
            answerLabel.IsVisible = !answerLabel.IsVisible;
        }
    }
}