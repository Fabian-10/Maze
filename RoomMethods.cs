
//this contain rooms for the 2d array logic

//this will do the main stuff with the rooms, and will contain the 2d array
//array stuff is taken from pg 122 of textbook
namespace Final_Project
{

    public static class RoomMethods
    {
        private static int rowsize = 4;
        private static int colsize = 4;
        public static Room[,] DaRooms = new Room[rowsize, colsize];

        

        //this will initialize the rooms as a 2d array
        //array stuff is taken from pg 122 of textbook
        //definately not taken from chatgpt
        static public void Roomsetup()
        {            
            for (int i = 0; i < rowsize; i++)
            {
                for (int j = 0; j < colsize; j++)
                {
                    DaRooms[i, j] = new Room(i,j);
                }
            }
        }

        public static Room[,] GetRooms() { return DaRooms; }

        

        //this function will return the total doors, as well as set the doors of the specific room. 
        static public string GetDoors(int x, int y) //this take in the rooms, as well as the index for the specific room
        {
            string Total_doors = ""; //all of the strings will be concatenated to the total doors
            if (x != 0) { Total_doors += "North"; } //if x is 0, we are in the first row and won't need north doors      

            if (y != (colsize - 1)) { Total_doors += "East"; }//on the last column, and wont need east door

            if (x != (rowsize - 1)) { Total_doors += "South"; } //on the last row, and as such will not the south door 

            if (y != 0) { Total_doors += "West"; } //if y is 0, we are in the first column and thus don't need the west door 

            return Total_doors;
        }

        //note that the room moves update the index inside of player
        //this will be used to schmoove the room, and will return an updated index 
        public static void UpMove(Player p) {p.SetX(p.GetX() - 1);}

        //downschmoove siuuu   
        public static void DownMove(Player p) { p.SetX(p.GetX() + 1); }

        public static void RightMove(Player p) { p.SetY(p.GetY() + 1);}

        public static void LeftMove(Player p) { p.SetY(p.GetY() - 1); }


    // Generate enemies in a random room
    /*public static void GenerateEnemy()
    {
        Random rng = new Random();


        // Take a random room
        int randomI;
        int randomJ;

        int numEnemy = 0;

    /*
        Facilitates in the future changing the amount of enemies  
        There is 3 level enemies + the boss  

        as a example you want a max of 6 enemies, so
        maxL1 = 2 --> in a loop will run from index 0 - 1 making 2 enemies lv1
        maxL2 = 4 --> in a loop will run from index 2 - 3 making 2 enemies lv2
        maxl3 = 5 --> in a loop will run on index 4 making 1 enemies lv3
        boss = 6 --> in a loop will run on index 5 making 1 boss                  
         
        int maxL1 = 2;
        int maxL2 = 4;
        int maxL3 = 5;
        int boss = 6;


            int maxKey = 1;
            int keyN = 0;

        foreach (var r in DaRooms)
        {
            randomI = rng.Next(0, 4);
            randomJ = rng.Next(0, 4);



            // if the room already have an enemy look of another room
            // and if the chosen room was room #1 look for another room
            while (DaRooms[randomI, randomJ].getEnemyAmount() < 1 && (randomI != 0 && randomJ != 0))
            {
                randomI = rng.Next(0, 4);
                randomJ = rng.Next(0, 4);
            }


            if (numEnemy < maxL1) // Create 2 enemies of level 1
            {
                DaRooms[randomI, randomJ].AddEntity(new EnemyL1());
                numEnemy++;
            }
            else if (numEnemy < maxL2) // Create 2 enemies of level 2
            {
                DaRooms[randomI, randomJ].AddEntity(new EnemyL2());
                    if (keyN < maxKey)
                    {
                        DaRooms[randomI, randomJ].AddItem(new Key("Key", "Open a lock door", Environment.CurrentDirectory + "/Items/Key 1.png")); // add the key to open the lock room
                        keyN++;
                    }
                        numEnemy++;
            }
            else if (numEnemy < maxL3) // Create 1 enemies of level 1
            {
                DaRooms[randomI, randomJ].AddEntity(new EnemyL3());
                numEnemy++;
            }
            else if (numEnemy < boss) // Create 1 boss
            {
                DaRooms[randomI, randomJ].AddEntity(new Boss());
                numEnemy++;
                break; // we excite the loop because we are done adding enemies
            }
    }
}*/


        // Generate item in the room in a random order
        // 
        public static void GenerateItem()
        {
            
            List<Items> stuff = new List<Items> ();

            string desc = "Forged from enchanted metal with intricate patterns, this chest plate blends elegance with formidable strength. Its polished surface emits an ethereal glow, hinting at magical properties within. Reinforced with mythical alloys, it offers superior protection against both physical and mystical threats. Adorned with protective runes and featuring articulated joints for flexibility, this chestplate is a symbol of prestige and practical defense for adventurous souls.";
            stuff.Add(new ChestPlate("ChestPlate", desc, 5, Environment.CurrentDirectory + "/Items/ChestPlate 1.png"));

            desc = "Introducing the \"Aegis Crown,\" a powerful RPG helmet crafted for fearless adventurers. With enchanted alloys for unbeatable protection, an elegant visor for clear vision, and magical gemstone accents, this helmet blends style and defense. The adjustable fit ensures comfort, and a discreet channel allows for magical enhancements. Conquer the unknown with the Aegis Crown – where style meets resilience in every quest.";
            stuff.Add(new Helmet("Helmet", desc, 2, Environment.CurrentDirectory + "/Items/Helmet 1.png"));

            desc = "Flexible yet resilient, the legging armor provides a perfect balance of protection and mobility with its advanced materials and ergonomic design.";
            stuff.Add(new Leggings("Leggings", desc, 3, Environment.CurrentDirectory + "/Items/Leggings 1.png"));

            desc = "Sleek, metallic boots intricately designed with reinforced plating, providing both style and formidable protection on the battlefield.";
            stuff.Add(new Boots("Boots", desc, 2, Environment.CurrentDirectory + "/Items/Boots 1.png"));

            desc = "A formidable shield armor, forged from resilient materials and imbued with enchantments, provides robust protection against a myriad of threats on the battlefield.";
            stuff.Add(new Shield("Shield",desc,6, Environment.CurrentDirectory + "/Items/Shield 1.png"));

            desc = "Heal 30 point of health";
            stuff.Add(new HealthPotion("Heal Potion", desc, 1, Environment.CurrentDirectory + "/Items/HealthPotion 1.png"));

            desc = "Increase your strength 10 point of strength";
            stuff.Add(new StrengthPotion("Strength Potions",desc,1, Environment.CurrentDirectory + "/Items/StrengthPotion 1.png"));

            int i = 0;
            bool check = true;

           
            foreach (var r in DaRooms)
            {
                // There is an enemy in the room
                if (r.getEnemyAmount() > 0 && r.getItemsAmount() < r.getEnemyAmount())
                {
                    if (i < stuff.Count) // check if there are items left
                    {
                        r.AddItem(stuff[i++]);
                    }
                }
                // The room is empty 
                // We lock it and put Excalibur
                else if (r.getEnemyAmount() == 0 && check)
                {
                    r.Lock_room();
                    r.AddItem(new Sword("Excalibur", "Super Sword", 14, Environment.CurrentDirectory + "/Items/Sword 1.png"));
                    check = false;
                }
                else if (!DaRooms[0, 0].Equals(r)) // check if there are items left
                {
                    if (i < stuff.Count)
                    {
                        r.AddItem(stuff[i++]);
                    }
                }
            }
            

        }

        public static void GenerateEnemies()
        {
            /*
             * Level 1 enemies: 7 rooms
             * Level 2 enemies: 5 rooms
             * Level 3 enemies: 2 rooms
             * Level 4 enemies: 1 room
            */

            bool added = false;

            for (int i = 0; i < 15;  i++) 
            {
                added = false;
                while(!added) 
                {
                    Random rng = new Random();
                    int row = rng.Next(4);
                    int col = rng.Next(4);

                    if (row == 0 && col == 0)
                    {
                        added = false;
                    }
                    else if (i < 7 && DaRooms[row,col].getEnemyAmount()==0) 
                    {
                        DaRooms[row, col].AddEntity(new EnemyL1());
                        added = true;
                    }
                    else if (i < 12 && DaRooms[row, col].getEnemyAmount() == 0)
                    {
                        DaRooms[row, col].AddEntity(new EnemyL2());
                        added = true;
                    }
                    else if (i < 14 && DaRooms[row, col].getEnemyAmount() == 0)
                    {
                        DaRooms[row, col].AddEntity(new EnemyL3());
                        added = true;
                    }
                    else if (i == 14 && DaRooms[row, col].getEnemyAmount() == 0)
                    {
                        DaRooms[row, col].AddEntity(new Boss());
                        added = true;
                    }
                }

            }

        }

    }
}