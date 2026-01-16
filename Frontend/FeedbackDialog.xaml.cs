using System.Windows;

namespace RevitAIPlugin
{
    public partial class FeedbackDialog : Window
    {
        public bool IsSuccess { get; private set; }
        public string Note { get; private set; }

        public FeedbackDialog()
        {
            InitializeComponent();
        }

        public static FeedbackResult Show()
        {
            var dialog = new FeedbackDialog();
            bool? result = dialog.ShowDialog();
            
            return new FeedbackResult
            {
                IsSuccess = dialog.IsSuccess,
                Note = dialog.Note
            };
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            IsSuccess = true;
            DialogResult = true;
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            IsSuccess = false;
            DialogResult = true;
        }

        private void BtnAddNote_Click(object sender, RoutedEventArgs e)
        {
            // Logic to show note input box would go here
            // For simplicity in this scaffold, let's assume a note is captured via a TextBox in the dialog
            // Note = txtNote.Text; 
        }
    }

    public class FeedbackResult
    {
        public bool IsSuccess { get; set; }
        public string Note { get; set; }
    }
}
