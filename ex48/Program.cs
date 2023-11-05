using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandCreateSquad = "1";
            const string CommandStartBattle = "2";

            Console.Write($"Война\n\n{CommandCreateSquad} - создать отряды\n{CommandStartBattle} - начать битву\n\nВаш ввод: ");

            switch (Console.ReadLine())
            {
                case CommandCreateSquad:
                    break;

                case CommandStartBattle:
                    break;
            }
        }
    }

    class BattleField
    {
        private List<Soldier> _soldiers;

        public BattleField()
        {
            _soldiers = new List<Soldier>
            {
                new Sniper("Снайпер", 100, 250),
                new Pyro("Поджигатель", 400, 100),
                new Medic("Медик", 200, 50),
                new MachineGunner(),
                new Bomber(),
                new Engineer()
            };
        }
    }

    class Soldier
    {
        public Soldier(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}. Здоровье: {Health}\nУрон: {Damage}");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void UseAbility() { }
    }

    class Sniper : Soldier
    {
        public Sniper(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {

        }
    }

    class Pyro : Soldier
    {
        public Pyro(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {

        }
    }

    class Medic : Soldier
    {
        public Medic(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {

        }
    }

    class Bomber : Soldier
    {
        public Bomber(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {
            
        }
    }

    class MachineGunner : Soldier
    {
        private int _attackSpeed = 0;

        public MachineGunner(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {
            Overclocking();
        }

        public void Overclocking()
        {
            Damage += _attackSpeed;
            _attackSpeed += 10;
        }
    }

    class Engineer : Soldier
    {
        public Engineer(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {

        }
    }
}
