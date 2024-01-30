using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Final_Project
{
    public class Player
    {
        private int x; //specifies the row of the room the player is in 
        private int y;  //specifies the column of the room the player is in
        private int grid_x; // Specifies the row the player is in respect of the grid
        private int grid_y; // Specifies the row the player is in respect of the grid
        private int id;
        private string username;
        private int healthPoints;
        private int strengthPoints;
        private int defensePoints;
        private int score;
        private List<Items> inventory;
        private bool hasKey;

        public Player(int id, string username, int healthPoints = 100, int strengthPoints = 100, int defensePoints = 100)
        {
            this.id = id;
            this.username = username;
            this.healthPoints = healthPoints;
            this.strengthPoints = strengthPoints;
            this.defensePoints = defensePoints;
            score = 0;
            inventory = new List<Items>();
            hasKey = false;
        }
        public void SetX(int X) { x = X; }
        public int GetX() { return x; }
        public void SetY(int Y) { y = Y; }
        public int GetY() { return y; }
        public void SetGrid(int x, int y) { grid_x = x; grid_y = y; }
        public int GetGridX() {  return grid_x; }
        public int GetGridY() { return grid_y; }
        public int getId() { return id; } // Id Getter
        public string getUsername() { return username; } //username getter
        public int getHealthPoints() { return healthPoints; } //hp getter
        public int getStrengthPoints() { return strengthPoints; } //sp getter
        public int getDefensePoints() { return defensePoints; } //dp getter
        public int getScore() { return score; } //score getter
        public List<Items> getInventory() { return inventory; } //inventory getter
        public void AddItem(Items I) { inventory.Add(I); } // add items to inventory
        public void RemoveItem(Items I) { inventory.Remove(I); } //remove items from inventory
        public void Unlock() { hasKey = true; } //Change when player gets key
        
        public void UpdateStatus(int newStatus, char kind) //Updates player status (Health, Defense, Strength and Score)
        {
            switch (kind)
            {
                case 'H': healthPoints = newStatus; break;
                case 'D': defensePoints = newStatus; break;
                case 'S': strengthPoints = newStatus; break;
                case 'C': score = newStatus; break;
            }
        }

        //Get any Item of type T
        //Returns a list of Items of Type T
        public List<Items> GetItemByType(Type T)
        {
            return inventory.Where(items => items.GetType() == T).ToList();
        }

        // If Item is found, collect it and do the item's action
        public void CollectItem(Items I)
        {
            List<Items> sameTypeItems = GetItemByType(I.GetType());

            // Check if an item of the same type is already in the inventory
            if (sameTypeItems != null && I.GetType() != typeof(Potion))
            {
                string msg = $"You already have a {I.GetType().Name} in your inventory.\n";
                msg += $"Current {I.GetType().Name} Power Level: {I.getPowerLvl()}\n";
                msg += $"New {sameTypeItems[0].GetType().Name} Power Level: {sameTypeItems[0].getPowerLvl()}\n";
                msg += $"Accepting the new {I.GetType().Name} will replace your current one.\n";
                msg += "Do you want to proceed?";
                MessageBoxResult result = MessageBox.Show(msg, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes) 
                {
                    sameTypeItems[0].ReverseAction(this); //Remove effect from previous Item
                    RemoveItem(sameTypeItems[0]); // Remove previous Item from inventory
                    AddItem(I); // Add new Item to the inventory
                    I.DoAction(this); //Add the effect of the new Item to players
                }                
            }
            else if (I.GetType() == typeof(Potion)) // In case of potions, it will be stacked in the inventory and wont be used immediately
            {
                AddItem(I);
            }
            else
            {
                AddItem(I);
                I.DoAction(this);
            }
           }
        

    }
}
