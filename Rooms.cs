//this contains all of the cooode for da rooms
//the rooms will at some point need to create a picture 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Final_Project
{
    public class Room
    {

        string RoomPictureSource;
        private int Locationi;
        private int Locationj;
        private bool is_locked; //if this is true, will make inaccessible
        private List<Items> Itemlist = new List<Items>(); //all items shall be stored in this
        private List<Entity> Entitylist = new List<Entity>(); //all monsters will be stored in this
        

        public Room(int Locationi, int Locationj)
        {
            this.Locationi = Locationi;
            this.Locationj = Locationj;
            SetPictureSource();
        }

        public Room(bool _is_locked)
        {
            is_locked = _is_locked;
        }


        public bool getIs_locked() { return is_locked; }

        public Items getItem(int i) { return Itemlist[i]; } //this will take in a number and return the item at that number, using array logic
        public Entity getEntity(int i) { return Entitylist[i]; } //this will take in an int and return the entity where that int is located

        public void AddItem(Items p) { Itemlist.Add(p); } //will add items to da room list
        public void AddEntity(Entity p) { Entitylist.Add(p); } //will add an entity to da list

        public void ClearItems() { Itemlist.Clear(); } //will clear the room list
        public void ClearEntities() { Entitylist.Clear(); } //will clear the monster list

        public void Lock_room() { is_locked = true; } //need a lock the doors at random 
        public void Unlock_room() //this will be used to switch a door's status to locked
        {
            if (is_locked == false) { } //needs to do nothing, should probably have this output that the door is already unlocked.
            else { is_locked = false; }
        }

        private void SetPictureSource()
        {
            Random rng = new Random();
            int n = rng.Next(2);
            RoomPictureSource = $"Rooms/Room {n+1}/" + RoomMethods.GetDoors(Locationi, Locationj) + ".jpg";
        }


        public string GetPictureSource() { return RoomPictureSource; }

        // To know if there is any enemy in this room
        // if there is no enemy, add an enemy
        // if there is a enemy we look for a different room
        // this function is call in RoomMethods in GenerateEnemy
        public int getEnemyAmount() { return Entitylist.Count; }

        // Have to match the same amount of enemies in the room
        // if the match look for a different room
        // this function is call in RoomMethods in GenerateItema
        public int getItemsAmount() { return Itemlist.Count; }




    }
}

