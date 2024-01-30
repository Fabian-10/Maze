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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    

    public partial class InventoryWindow : Window
    {
        private bool IsEscPressed;
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private int itemSlotCount = 1;
        private int itemsAdded = 0;
        public InventoryWindow()
        {
            InitializeComponent();

            InventoryScreen.Focus();
            GameTimer.Interval = TimeSpan.FromMilliseconds(30);
            GameTimer.Tick += GameTick;
            GameTimer.Start();


            for (int i = 1; i <= 36; i++) 
            {
                string ImageName = "Item" + i;
                Image image = FindName(ImageName) as Image;

                AttachEvents(image, "Empty", "Empty");
            }

            
        }

        private void AttachEvents(Image image, string itemName, string itemDescription)
        {
            image.MouseEnter += (sender, e) =>
            {
                ToolTip toolTip = new ToolTip();
                StackPanel stackPanel = new StackPanel();

                TextBlock nameTextBlock = new TextBlock();
                nameTextBlock.FontWeight = FontWeights.Bold;
                nameTextBlock.Text = itemName;

                TextBlock descriptionTextBlock = new TextBlock();
                descriptionTextBlock.Text = itemDescription;

                stackPanel.Children.Add(nameTextBlock);
                stackPanel.Children.Add(descriptionTextBlock);

                toolTip.Content = stackPanel;
                image.ToolTip = toolTip;
            };

            image.MouseLeave += (sender, e) =>
            {
                image.ToolTip = null;
            };
        }

        private void On_click(object sender, RoutedEventArgs e)
        {

        }

        private void GameTick(object sender, EventArgs e)
        {
            if (IsEscPressed) { Close(); }

            List<Items> i = GameMain.MainPlayer.getInventory();
            while (itemsAdded != i.Count) 
            {
                string ImageName = "Item" + itemSlotCount;
                Image image = FindName(ImageName) as Image;
                AttachEvents(image, i[itemsAdded].getName(), i[itemsAdded].getDescription());
                image.Source = new BitmapImage(new Uri(i[itemsAdded].getImagePath()));

                itemsAdded++;
                itemSlotCount++;
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape) { IsEscPressed = true; }
        }

        private void KeyUnPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape) { IsEscPressed = true; }
        }

    }
}
