using System;
using System.Collections.Generic;

namespace ex48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battlefield battlefield = new Battlefield();

            battlefield.Battle();

            battlefield.ShowBattleResult();
        }
    }

    class Battlefield
    {
        private Platoon _firstCountry = new Platoon();
        private Platoon _secondCountry = new Platoon();
        private Soldier _firstSoldier;
        private Soldier _secondSoldier;

        public void Battle()
        {
            while (_firstCountry.GetCount() > 0 && _secondCountry.GetCount() > 0)
            {
                _firstSoldier = _firstCountry.GetSoldierFromPlatoon();
                _secondSoldier = _secondCountry.GetSoldierFromPlatoon();

                ShowPlatoons();
                Fight();
                DetermineWinner();

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void ShowBattleResult()
        {
            if (_firstCountry.GetCount() < 0 && _secondCountry.GetCount() < 0)
            {
                Console.WriteLine("Ничья");
            }
            else if (_firstCountry.GetCount() <= 0)
            {
                Console.WriteLine("Победа за вторым взводом");
            }
            else if (_secondCountry.GetCount() <= 0)
            {
                Console.WriteLine("Победа за первым взводом");
            }
        }

        private void ShowPlatoons()
        {
            Console.WriteLine("Взвод первой страны: ");
            _firstCountry.ShowPlatoon();
            Console.WriteLine("Взвод второй страны: ");
            _secondCountry.ShowPlatoon();
        }

        private void Fight()
        {
            _firstSoldier.UseAnAttack();
            _secondSoldier.UseAnAttack();
            _firstSoldier.TakeDamage(_secondSoldier.Damage);
            _secondSoldier.TakeDamage(_firstSoldier.Damage);
        }

        private void DetermineWinner()
        {
            if (_firstSoldier.Health <= 0)
            {
                _firstCountry.RemoveDiedSoldier(_firstSoldier);
            }
            else if (_secondSoldier.Health <= 0)
            {
                _secondCountry.RemoveDiedSoldier(_secondSoldier);
            }
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();
        private Random _random = new Random();

        public Platoon()
        {
            CreatePlatoon(_soldiers, GetSoldierCount());
        }

        public void RemoveDiedSoldier(Soldier soldier)
        {
            _soldiers.Remove(soldier);
        }

        public void ShowPlatoon()
        {
            foreach (Soldier soldier in _soldiers)
            {
                soldier.ShowInfo();
            }

            Console.WriteLine();
        }

        public int GetCount()
        {
            return _soldiers.Count;
        }

        public Soldier GetSoldierFromPlatoon()
        {
            return _soldiers[_random.Next(0, _soldiers.Count)];
        }

        private void CreatePlatoon(List<Soldier> soldiers, int soldierCount)
        {
            for (int i = 0; i < soldierCount; i++)
            {
                soldiers.Add(GetSoldier());
            }
        }

        private int GetSoldierCount()
        {
            int minSoldierCount = 3;
            int maxSoldierCount = 6;
            int soldierCount = _random.Next(minSoldierCount, maxSoldierCount);
            return soldierCount;
        }

        private Soldier GetSoldier()
        {
            int maxSoldierCount = 4;
            int randomSoldierIndex = _random.Next(maxSoldierCount);

            if (randomSoldierIndex == 0)
            {
                return new Sniper("Снайпер", 500, 100);
            }
            else if (randomSoldierIndex == 1)
            {
                return new Miner("Минер", 800, 100);
            }
            else if (randomSoldierIndex == 2)
            {
                return new MachineGunner("Пулеметчик", 1100, 50);
            }
            else
            {
                return new Spy("Шпион", 600, 50);
            }
        }
    }

    abstract class Soldier
    {
        public Soldier(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
            InitialDamage = Damage;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int InitialDamage { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, Здоровье: {Health}, урон: {Damage}");
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                damage = 0;
            }

            Health -= damage;
            Console.WriteLine($"\n{Name} нанес {Damage} урона");
        }

        public void UseAnAttack()
        {
            Damage = InitialDamage;

            Random random = new Random();
            int chance = 1;
            int chanceValue = 4;
            int randomValue = random.Next(chanceValue);

            if (chance == randomValue)
            {
                Console.WriteLine();
                UseAttack();
            }
        }

        protected abstract void UseAttack();
    }

    class Sniper : Soldier
    {
        public Sniper(string name, int health, int damage) : base(name, health, damage) { }

        protected override void UseAttack()
        {
            int extraDamage = 3;
            Damage *= extraDamage;
            Console.WriteLine("Попадание в голову!");
        }
    }

    class Miner : Soldier
    {
        public Miner(string name, int health, int damage) : base(name, health, damage) { }

        protected override void UseAttack()
        {
            int extraDamage = 50;
            Damage += extraDamage;
            Console.WriteLine("Противник наступил на мину!");
        }
    }

    class MachineGunner : Soldier
    {
        private int _warmup = 1;

        public MachineGunner(string name, int health, int damage) : base(name, health, damage) { }

        protected override void UseAttack()
        {
            int extraDamage = 20;
            Damage += _warmup * extraDamage;
            _warmup++;
            Console.WriteLine("Пулемет разгоняется!");
        }
    }

    class Spy : Soldier
    {
        public Spy(string name, int health, int damage) : base(name, health, damage) { }

        protected override void UseAttack()
        {
            Random random = new Random();
            int minRandomValue = -10;
            int maxRandomValue = 100;
            int randomValue = random.Next(minRandomValue, maxRandomValue);
            Health += randomValue;
            Damage += randomValue;
            Console.WriteLine("Шпион получил преимущество над противником!");
        }
    }
}
