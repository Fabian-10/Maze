using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameMain : Window
    {
        readonly int MAX_RIGHT = 9;
        readonly int MAX_DOWN = 9;
        readonly int SPEED = 50; //How fast the characters goes. The greater the number the slower it goes. I know it's counterintuitive.
        public bool isCombatWindowOpen = false;

        public static Player MainPlayer;

        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpKeyPress, DownKeyPress, RightKeyPress, LeftKeyPress;


        // Let us identify when the player is pressing a key
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.W || e.Key == System.Windows.Input.Key.Up)
            {
                UpKeyPress = true;
            }
            if (e.Key == System.Windows.Input.Key.A || e.Key == System.Windows.Input.Key.Left)
            {
                LeftKeyPress = true;
            }
            if (e.Key == System.Windows.Input.Key.D || e.Key == System.Windows.Input.Key.Right)
            {
                RightKeyPress = true;
            }
            if (e.Key == System.Windows.Input.Key.S || e.Key == System.Windows.Input.Key.Down)
            {
                DownKeyPress = true;
            }

        }

        // Identify when the user release the key
        private void KeyUnpressed(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.W || e.Key == System.Windows.Input.Key.Up)
            {
                UpKeyPress = false;
            }
            if (e.Key == System.Windows.Input.Key.A || e.Key == System.Windows.Input.Key.Left)
            {
                LeftKeyPress = false;
            }
            if (e.Key == System.Windows.Input.Key.D || e.Key == System.Windows.Input.Key.Right)
            {
                RightKeyPress = false;
            }
            if (e.Key == System.Windows.Input.Key.S || e.Key == System.Windows.Input.Key.Down)
            {
                DownKeyPress = false;
            }
        }

        // Event that decide what direction the player is moving (up, right, left, or down)
        private void GameTick(object sender, EventArgs e)
        {
            if (isCombatWindowOpen)
            {
                // Combat window is open, do not update the game state
                return;
            }

            if (Grid.GetRow(Player) == Grid.GetRow(Enemy) && Grid.GetColumn(Player) == Grid.GetColumn(Enemy))
            {
                CombatWindow combatWindow = new CombatWindow();
                GameTimer.Stop();
                isCombatWindowOpen = true;
                combatWindow.Closed += (s, args) =>
                {
                    isCombatWindowOpen = false;
                    GameTimer.Start();
                };
                combatWindow.Show();               
            }

            

            if (UpKeyPress && Grid.GetRow(Player) - 1 >= 1)
            {
                Grid.SetRow(Player, Grid.GetRow(Player) - 1); // set the position on screen
                MainPlayer.SetGrid(Grid.GetRow(Player), Grid.GetColumn(Player)); // set position on player class
                Player.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}/Player/Player {MainPlayer.getId()}/Back.png"));
                Thread.Sleep(SPEED); //Sleep for a bit so the movement is not so fast
                RoomChange();
            }
            if (DownKeyPress && Grid.GetRow(Player) + 1 <= MAX_DOWN)
            {
                Grid.SetRow(Player, Grid.GetRow(Player) + 1);
                MainPlayer.SetGrid(Grid.GetRow(Player), Grid.GetColumn(Player));
                Player.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}/Player/Player {MainPlayer.getId()}/Front.png"));
                Thread.Sleep(SPEED);
                RoomChange();
            }
            if (LeftKeyPress && Grid.GetColumn(Player) - 1 > 0)
            {
                Grid.SetColumn(Player, Grid.GetColumn(Player) - 1);
                MainPlayer.SetGrid(Grid.GetRow(Player), Grid.GetColumn(Player));
                Player.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}/Player/Player {MainPlayer.getId()}/Left.png"));
                Thread.Sleep(SPEED);
                RoomChange();
            }
            if (RightKeyPress && Grid.GetColumn(Player) + 1 <= MAX_RIGHT)
            {
                Grid.SetColumn(Player, Grid.GetColumn(Player) + 1);
                MainPlayer.SetGrid(Grid.GetRow(Player), Grid.GetColumn(Player));
                Player.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}/Player/Player {MainPlayer.getId()}/Right.png"));
                Thread.Sleep(SPEED);
                RoomChange();
            }
        }

        private void InventoryButtonClick(object sender, RoutedEventArgs e)
        {
            InventoryWindow inventoryWindow = new InventoryWindow();
            inventoryWindow.Show();
        }

        //Controls what image is displayed when character moves rooms
        // The numbers used are arbitrary based on testing
        private void RoomChange()
        {
            //Where are the door situated in cur room
            string doors = RoomMethods.GetDoors(MainPlayer.GetX(), MainPlayer.GetY());

            // If the x door is in the room Player need too meet location criteria in order to open it
            if (doors.Contains("North") && MainPlayer.GetGridX() == 1 && MainPlayer.GetGridY() == 5)
            {
                RoomMethods.UpMove(MainPlayer);
                Grid.SetRow(Player, MAX_DOWN);
                MainPlayer.SetGrid(Grid.GetRow(Player), MainPlayer.GetGridY());
                LoadRoomImage(MainPlayer.GetX(), MainPlayer.GetY());
                LoadEnemies(MainPlayer.GetX(), MainPlayer.GetY());
            }
            else if (doors.Contains("East") && MainPlayer.GetGridY() == MAX_RIGHT && MainPlayer.GetGridX() == 5)
            {
                RoomMethods.RightMove(MainPlayer);
                Grid.SetColumn(Player, 1);
                MainPlayer.SetGrid(MainPlayer.GetGridX(), Grid.GetColumn(Player));
                LoadRoomImage(MainPlayer.GetX(), MainPlayer.GetY());
                LoadEnemies(MainPlayer.GetX(), MainPlayer.GetY());
            }
            else if (doors.Contains("South") && MainPlayer.GetGridX() == MAX_DOWN && MainPlayer.GetGridY() == 5)
            {
                RoomMethods.DownMove(MainPlayer);
                Grid.SetRow(Player, 1);
                MainPlayer.SetGrid(Grid.GetRow(Player), MainPlayer.GetGridY());
                LoadRoomImage(MainPlayer.GetX(), MainPlayer.GetY());
                LoadEnemies(MainPlayer.GetX(), MainPlayer.GetY());
            }
            else if (doors.Contains("West") && MainPlayer.GetGridY() == 1 && MainPlayer.GetGridX() == 5)
            {
                RoomMethods.LeftMove(MainPlayer);
                Grid.SetColumn(Player, MAX_RIGHT);
                MainPlayer.SetGrid(MainPlayer.GetGridX(), Grid.GetColumn(Player));
                LoadRoomImage(MainPlayer.GetX(), MainPlayer.GetY());
                LoadEnemies(MainPlayer.GetX(), MainPlayer.GetY());
            }
        }

        private void LoadRoomImage(int x, int y)
        {
            string path = Environment.CurrentDirectory;
            string image = RoomMethods.GetRooms()[x, y].GetPictureSource();
            string pic = $@"{path}/{image}";

            ImageRoom.Source = new BitmapImage(new Uri(pic));
        }

       private void LoadEnemies(int x, int y)
        {
            Entity e = RoomMethods.GetRooms()[x, y].getEntity(0);
            string a = e.GetPic();
            Enemy.Source = new BitmapImage(new Uri(RoomMethods.GetRooms()[x,y].getEntity(0).GetPic()));
            Random rng = new Random();
            int row = rng.Next(9);
            int column = rng.Next(9);

            Grid.SetRow(Enemy, row);
            Grid.SetColumn(Enemy, column);


        }




        public GameMain()
        {
            InitializeComponent();

            RoomMethods.Roomsetup(); // Set up Rooms
            RoomMethods.GenerateEnemies(); // set the enemies in each room
            RoomMethods.GenerateItem(); // set the items in each room 
            

            MainPlayer.AddItem(new Sword("Power", "Respect", 1, Environment.CurrentDirectory + "/Items/Sword 1.png"));

            MainPlayer.SetX(0); // Put player in the first room
            MainPlayer.SetY(0); // Put player in the first room 
            Player.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}/Player/Player {MainPlayer.getId()}/Front.png")); // Get initial player image
            LoadRoomImage(0, 0); // Load first room Image

            GameScreen.Focus();
            GameTimer.Interval = TimeSpan.FromMilliseconds(30);
            GameTimer.Tick += GameTick;
            GameTimer.Start();

        }



    }

}