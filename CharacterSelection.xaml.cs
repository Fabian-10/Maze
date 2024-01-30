using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for CharacterSelection.xaml
    /// </summary>
    public partial class CharacterSelection : Page
    {
        private int char_id = 0;
        private readonly int num_char = 4;
        public CharacterSelection()
        {
            InitializeComponent();
            characterImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $"/Player/Player 0/Front.png"));

        }

        private void PreviousCharacter_Click(object sender, RoutedEventArgs e)
        {
            char_id--;
            if (char_id < 0 ) { char_id = num_char-1; }
            characterImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $"/Player/Player {char_id}/Front.png"));
        }

        private void NextCharacter_Click(object sender, RoutedEventArgs e)
        {
            char_id++;
            if (char_id == num_char) { char_id = 0; }
            characterImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $"/Player/Player {char_id}/Front.png"));
        }

        private void SelectCharacter_Click(object sender, RoutedEventArgs e)
        {
            string name = playerNameTextBox.Text;
            if(name == "Enter Player Name")
            {
                MessageBox.Show("Player Name not provided!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                GameMain.MainPlayer = new Player(char_id, name, 100 , 20, 100);
                GameMain gameMain = new GameMain();
                gameMain.Show();
                Window.GetWindow(this).Close();
            }            
        }

        private void playerNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the default text when the TextBox gets focus
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Enter Player Name")
            {
                textBox.Text = "";
            }
        }

        private void playerNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Restore the default text if no input is provided
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter Player Name";
            }
        }


    }
}
