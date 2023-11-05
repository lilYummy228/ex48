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

            World world = new World();

            Console.Write($"Война\n\n{CommandCreateSquad} - создать отряды\n{CommandStartBattle} - начать битву\n\nВаш ввод: ");

            switch (Console.ReadLine())
            {
                case CommandCreateSquad:
                    world.CreateArmy();
                    break;

                case CommandStartBattle:
                    break;
            }
        }
    }

    class World
    {
        private List<Soldier> _soldiers;

        public World()
        {
            _soldiers = new List<Soldier>
            {
                new Sniper("Снайпер", 100, 150),
                new Pyro("Поджигатель", 400, 100),
                new Medic("Медик", 150, 50),
                new MachineGunner("Пулеметчик", 600, 50),
                new Bomber("Подрыватель", 250, 150),
                new Engineer("Инжинер", 200, 100)
            };
        }

        public void CreateArmy()
        {
            List<Soldier> army = new List<Soldier>();
            bool isEmpty = true;

            while (isEmpty)
            {
                Console.Clear();

                for (int i = 0; i < _soldiers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_soldiers[i].Name}");
                }

                Console.Write("Кого вы хотите добавить во взвод? ");

                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    army.Add(_soldiers[index - 1]);
                }

                if (army.Count > 5)
                {
                    isEmpty = false;
                }
            }

            Console.WriteLine("Взвод номер один");

            foreach (Soldier soldier in army)
            {
                Console.WriteLine($"{soldier.Name}");
            }
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
        private int _critDamage;
        private int _initialDamage;
        private int _critMultiplier = 2;
        private int _critDamageValue = 0;
        private int _critDamageChance = 4;
        private Random _random = new Random();

        public Sniper(string name, int health, int damage) : base(name, health, damage)
        {
            Damage = _initialDamage;
            _critDamage = Damage * _critMultiplier;
        }

        public override void UseAbility()
        {
            HeadShot();
        }

        public void HeadShot()
        {
            int chance = _random.Next(_critDamageChance);
            Damage = _initialDamage;

            if (chance == _critDamageValue)
                Damage = _critDamage;
        }
    }

    class Pyro : Soldier
    {
        private int _fireDamage = 10;
        public Pyro(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {
            Burn();
        }

        public void Burn()
        {
            Damage += _fireDamage;
        }
    }

    class Medic : Soldier
    {
        public Medic(string name, int health, int damage) : base(name, health, damage) { }

        public override void UseAbility()
        {

        }

        public void Heal()
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
