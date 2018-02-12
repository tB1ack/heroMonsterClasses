using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace chellengeHeroMonsterClasses
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Character heroCharacter = new Character();

            heroCharacter.Name = "Beowulf";
            heroCharacter.Health = 10;
            heroCharacter.DamageMaximum = 3;
            heroCharacter.AttackBonus = true;

            Character monsterCharacter = new Character();

            monsterCharacter.Name = "Grendel";
            monsterCharacter.Health = 20;
            monsterCharacter.DamageMaximum = 2;
            monsterCharacter.AttackBonus = false;

            Dice dice = new Dice();

            resultLabel.Text += "Battle Begin";
            displayStats(heroCharacter);
            displayStats(monsterCharacter);

            //Bonus Attack
            if (heroCharacter.AttackBonus)
            {
                int heroAttack = heroCharacter.Attack(dice);
                monsterCharacter.Defend(heroAttack);
                resultLabel.Text += "Hero Gets a Bonus Attack:";
                displayStats(heroCharacter, heroAttack);
            }                          
            if (monsterCharacter.AttackBonus)
            {
                int monsterAttack = monsterCharacter.Attack(dice);
                heroCharacter.Defend(monsterAttack);
                resultLabel.Text += "Monster Gets a Bonus Attack:";
                displayStats(monsterCharacter, monsterAttack);
            }          
            
            while (heroCharacter.Health > 0 && monsterCharacter.Health > 0)
            {
                int heroAttack = heroCharacter.Attack(dice);
                monsterCharacter.Defend(heroAttack);
                int monsterAttack = monsterCharacter.Attack(dice);
                heroCharacter.Defend(monsterAttack);

                displayStats(heroCharacter, heroAttack);
                displayStats(monsterCharacter, monsterAttack);
            }

            displayBattleResults(heroCharacter, monsterCharacter);

        }


        private void displayBattleResults(Character opponent1, Character opponent2)
        {
            if (opponent1.Health <= 0 && opponent2.Health <= 0)
            {
                resultLabel.Text += String.Format("Both {0} and {1} are dead.", opponent1.Name, opponent2.Name);
            }
            else if (opponent1.Health <= 0)
            {
                resultLabel.Text += String.Format("{0} defeated {1}.", opponent2.Name, opponent1.Name);
            }
            else
            {
                resultLabel.Text += String.Format("{0} defeated {1}.", opponent1.Name, opponent2.Name);
            }
        }

        private void displayStats(Character character,int attack = 0)
        {
            resultLabel.Text += String.Format("<p>Name: {0} <br />Health: {1} <br />Attack: {2} </p>",
                character.Name,
                character.Health,
                attack.ToString());
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        public int Attack(Dice dice)
        {
            dice.Sides = this.DamageMaximum;
            return dice.Roll();
        }
        public void Defend(int damage)
        {
            this.Health -= damage;
        }
    }

    class Dice
    {
        public int Sides { get; set; }

        Random random = new Random();
        public int Roll()
        {
            return random.Next(this.Sides);
        }
    }
}