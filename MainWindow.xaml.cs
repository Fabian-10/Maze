using System.Windows;
using System.Windows.Media.Imaging;

namespace Final_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BackgroundImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "/MenuBackground.jpg"));
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            CharacterSelection characterSelection = new CharacterSelection();

            // Set the window startup location to CenterScreen
            //characterSelection.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Navigate to the CharacterSelection page within the Frame
            mainFrame.NavigationService.Navigate(characterSelection);

            //GameMain gameMain = new GameMain();
            //gameMain.Show();
            //Close();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            // Add logic to navigate to the options page
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
