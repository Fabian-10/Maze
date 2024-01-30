//// <summary>

// Ignore Spelling: pic

/// This class represent the causalities of the enemies the user can encounter
/// Enemies will variety between levels 1 - 3, the higher the more dangerous
/// </summary>

namespace Final_Project {

    public abstract class Entity
    {
        // Attributes that all enemies have
        private int hp;
        private int level;
        protected Random damage = new Random();
        protected string pic;
        protected int delayT = 500;

        // Argumentative constructor
        // Going to be call the default for each child class
        // It child will send different parameters
        public Entity(int hp, int level)
        {
            this.hp = hp;
            this.level = level;
            SetPic();
        }

        public abstract void Attack(Player a);

        public int GetHP() { return hp; } // Hp getter
        public int GetL() { return level; } // Level getter

        // when this enemy has the opportunity to heal up
        public void IncHP(int h) { hp += h; }


        // when the player attacks
        // This is suppose to be called in the attack method of player
        public void decrHP(int h) { hp -= h; }

        public abstract void SetPic();

        public string GetPic() { return pic; }

        public bool isAlive()
        {
            if (hp == 0)
            {
                return false;
            }

            return true;

        }



    }


    // This represent the a enemy of level 1
    // DO not make a lot of damage
    public class EnemyL1 : Entity
    {

        // Default constructor
        // Creating an enemy of level 1 with 35 hp
        public EnemyL1 () : base (35, 1) {}


        // This represent the enemy attacking the player
        // It has 3 level attack that the enemy choose randomly
        public override void Attack(Player a)
        {
            int rNum = damage.Next(1,10);
            
            // probability of making a 10 of damage (60%)
            if (rNum <= 60)
            {
                a.UpdateStatus(a.getHealthPoints()-10, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 10);
            }
            // Probability of making 15 of damage (30%)
            else if (rNum <= 90)
            {
                a.UpdateStatus(a.getHealthPoints() - 15, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 15);
            }
            // Probability of making 20 of damage (10%) 
            else if (rNum  <= 100)
            {
                a.UpdateStatus(a.getHealthPoints() - 20, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 20);
            }
            Thread.Sleep(delayT);
        }

        public override void SetPic()
        {
            pic = Environment.CurrentDirectory + "/Enemies/1.png";
        }
    }

    // An enemy of level 2 with double the hp compare to enemy level 1
    // Same attack but with more damage
    public class EnemyL2 : Entity
    {
        // hp 70 and level 2 enemy
        public EnemyL2() : base (70, 2) {}

        public override void Attack(Player a)
        {
            int rNum = damage.Next(1, 101);

            // probability of making a 15 of damage (60%)
            if (rNum <= 60)
            {
                a.UpdateStatus(a.getHealthPoints() - 15, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 15);
            }
            // Probability of making 20 of damage (30%)
            else if (rNum <= 90)
            {
                a.UpdateStatus(a.getHealthPoints() - 20, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 20);
            }
            // Probability of making 25 of damage (10%) 
            else if (rNum <= 100)
            {
                a.UpdateStatus(a.getHealthPoints() - 25, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 25);
            }
            Thread.Sleep(delayT);
        }

        public override void SetPic()
        {
            pic = Environment.CurrentDirectory + "/Enemies/2.png";
        }
    }


    // Enemy of level 3
    // 90 hp point and 5+ damage in all attacks, less probable for attack level 3
    // Has a opportunity to health or deal
    public class EnemyL3 : Entity
    {
        public EnemyL3() : base (90,3) { }

        public override void Attack(Player a)
        {
            int rNum = damage.Next(1, 102);

            // probability of making a 25 of damage (60%)
            if (rNum <= 60)
            {
                a.UpdateStatus(a.getHealthPoints() - 25, 'H');
                //Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 25);
            }
            // Probability of making 30 of damage (30%)
            else if (rNum <= 90)
            {
                a.UpdateStatus(a.getHealthPoints() - 30, 'H');
                //Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 30);
            }
            // Probability of making 35 of damage (10%) 
            else if (rNum <= 100)
            {
                a.UpdateStatus(a.getHealthPoints() - 35, 'H');
                //Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 35);
            }
            // 1% probability of making a powerful attack or heal
            else if (rNum == 101)
            {
                // If and only if this enemy has 25% or leas health 
                if (this.GetHP() <= (this.GetHP()/4))
                {
                    a.UpdateStatus(a.getHealthPoints() - 35, 'H');
                    //Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 20);
                }

                // This enemy has a health lower than 25% and it need healing
                this.IncHP(35);
            }
            Thread.Sleep(delayT);
        }

        public override void SetPic()
        {
            pic = Environment.CurrentDirectory + "/Enemies/3.png";
        }
    }


    // There is suppose to be only 1 boss
    // It can heal more frequently compare to enemy level 3
    // Make more damage than any other, but the heavier attack are less frequent
    // It can dodge you attacks
    public class Boss : Entity
    {

        public Boss() : base(150, 4) { } 

        public override void Attack(Player a)
        {
            int rNum = damage.Next(1, 102);

            // probability of making a 35 of damage (60%)
            if (rNum <= 75)
            {
                a.UpdateStatus(a.getHealthPoints() - 35, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 35);
            }
            // Probability of making 30 of damage (30%)
            else if (rNum <= 95)
            {
                a.UpdateStatus(a.getHealthPoints() - 45, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 45);
            }
            // Probability of making 35 of damage (10%) 
            else if (rNum <= 100)
            {
                a.UpdateStatus(a.getHealthPoints() - 55, 'H');
                Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 55);
            }
            // 1% probability of making a powerful attack or heal
            else if (rNum == 101)
            {
                // If and only if this enemy has 25% or leas health 
                if (this.GetHP() <= (this.GetHP() / 2))
                {
                    a.UpdateStatus(a.getHealthPoints() - 69, 'H');
                    Console.WriteLine("{0} your health has decrease {1} point", a.getUsername(), 69);
                }

                // This enemy has a health lower than 25% and it need healing
                this.IncHP(50);
            }
            Thread.Sleep(delayT);
        }


        // This should be call in the attack method of player
        // Player check if the level == 4
        // if this is return true the player deal no damage to the this enemy
        public bool checkDodge()
        {
            
            int rNum = damage.Next(1, 101);

            // It has a 40% probability to dodge the attack 
            if(rNum < 40)
            {
                // Attack was dodge
                return true;
            }

            // Didn't dodge the attack and revived the damage
            return false;
        }


        public override void SetPic()
        {
            pic = Environment.CurrentDirectory + "/Enemies/4.png";
        }
    }


}