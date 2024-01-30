using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace Final_Project
{
    /// <summary>
    /// Interaction logic for CombatWindow.xaml
    /// </summary>
    public partial class CombatWindow : Window
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private int turncount = -1;

        public CombatWindow()
        {
            InitializeComponent();

            playerImage.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}/Player/Player {GameMain.MainPlayer.getId()}/Back.png"));
            enemyImage.Source = new BitmapImage(new Uri(RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).GetPic()));

            playersName.Text = GameMain.MainPlayer.getUsername();
            enemyLVL.Text = "Enemy LVL" + RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).GetL();

            Combat.Focus();
            GameTimer.Interval = TimeSpan.FromMilliseconds(30);
            GameTimer.Tick += GameTick;
            GameTimer.Start();            
        }

        private void Attack(object sender, RoutedEventArgs e)
        {
            battleStatus.Text = $"The enemy took {GameMain.MainPlayer.getStrengthPoints()} points of Damage!";
            RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).decrHP(GameMain.MainPlayer.getStrengthPoints());
            turncount++;
            GameTimer.Start();
        }

        private void Defend(object sender, RoutedEventArgs e)
        {
            
            //this will handle what happens when the third button is clicked
            battleStatus.Text = "You chose to block!";
            GameMain.MainPlayer.UpdateStatus(GameMain.MainPlayer.getDefensePoints() + 20, 'D'); //probably need to make this go back down at the end fo the turn
            turncount++;
            GameTimer.Start();
        }

        private void Inventory(object sender, RoutedEventArgs e)
        {
            InventoryWindow inventoryWindow = new InventoryWindow();
            inventoryWindow.Show();
            GameTimer.Start();
        }

        private void Run(object sender, RoutedEventArgs e)
        {
            Random rng = new Random();
            int n = rng.Next(20);
            GameMain.MainPlayer.UpdateStatus(GameMain.MainPlayer.getHealthPoints() - n, 'H');
            battleStatus.Text = "You chose to run! Take a little damage for being a coward.";
            Thread.Sleep(2000);            
            Close();          

        }

        private void GameTick(object sender, EventArgs e)
        {
            playerHealthBar.Value = GameMain.MainPlayer.getHealthPoints();
            enemyHealthBar.Value = RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).GetHP();

            
            if (turncount == -1)
            {
                GameTimer.Stop();
                //going to use a message box prompt for  the player engaging or fleeing combat becuase substantially easier
                MessageBoxResult result = MessageBox.Show("You have confronted a Level " + RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).GetL() + "Do you wish to continue?", "Yes/No MessageBox Example", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No) { Close(); } //User chooses to decline combat, exiting function. In the future, could mb make this damage the player
                                                                //at this point, the player has chosen to fight, and can run the main loop now
                else { turncount++;}
            }
            else if (turncount % 2 == 0 && GameMain.MainPlayer.getHealthPoints() >= 0)
            {
                battleStatus.Text = "It is your turn, which action would you like to take?";
                GameTimer.Stop();
            }
            else if (turncount % 2 != 0 && RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).GetHP() >= 0)
            { 
                RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).Attack(GameMain.MainPlayer);                
                turncount++; 
            }

            if (RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].getEntity(0).GetHP() < 0) //checking to see if monster has perished
            {
                GameTimer.Stop();
                MessageBoxResult m = MessageBox.Show("The Monster has been Vanquished!","VICTORY", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //RoomMethods.DaRooms[GameMain.MainPlayer.GetX(), GameMain.MainPlayer.GetY()].ClearEntities();
                Close();            
            }
            else if (GameMain.MainPlayer.getHealthPoints() < 0)  //checking to see if player is alive
            {
                GameTimer.Stop();
                MessageBox.Show("You have fallen in battle!", "Defeat", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                
            }
        }


    }
}