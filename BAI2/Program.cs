using System;
using System.Collections.Generic;

namespace BAI
{
    public partial class BAI_Afteken2
    {
        public static bool Vooruit(uint b)
        {
            return (b & (1 << 7)) != 0;
        }

        public static uint Vermogen(uint b)
        {
            uint vermogenBits = (b >> 5) & 3;
            switch (vermogenBits)
            {
                case 0:
                    return 0;
                case 1:
                    return 33;
                case 2:
                    return 67;
                case 3:
                    return 100;
                default:
                    return 0;
            }
        }

        public static bool Wagon(uint b)
        {
            return (b & (1 << 4)) != 0;
        }

        public static bool Licht(uint b)
        {
            return (b & (1 << 3)) != 0;
        }

        public static uint ID(uint b)
        {
            return b & 7;
        }

        public static HashSet<uint> Alle(List<uint> inputStroom)
        {
            return new HashSet<uint>(inputStroom);
        }

        public static HashSet<uint> ZonderLicht(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>();
            foreach (var b in inputStroom)
            {
                if (!Licht(b))
                {
                    set.Add(b);
                }
            }
            return set;
        }

        public static HashSet<uint> MetWagon(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>();
            foreach (var b in inputStroom)
            {
                if (Wagon(b))
                {
                    set.Add(b);
                }
            }
            return set;
        }

        public static HashSet<uint> SelecteerID(List<uint> inputStroom, uint lower, uint upper)
        {
            HashSet<uint> set = new HashSet<uint>();
            foreach (var b in inputStroom)
            {
                uint id = ID(b);
                if (id >= lower && id <= upper)
                {
                    set.Add(b);
                }
            }
            return set;
        }

        public static HashSet<uint> Opdr3a(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>();
            foreach (var b in inputStroom)
            {
                if (ID(b) < 3 && !Licht(b))
                {
                    set.Add(b);
                }
            }
            return set;
        }

        public static HashSet<uint> Opdr3b(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>(Alle(inputStroom));
            set.ExceptWith(Opdr3a(inputStroom));
            foreach (var b in Opdr3a(inputStroom))
            {
                if (ID(b) > 2 || Licht(b))
                {
                    set.Add(b);
                }
            }
            return set;
        }

        public static void ToonInfo(uint b)
        {
            Console.WriteLine($"ID {ID(b)}, Licht {Licht(b)}, Wagon {Wagon(b)}, Vermogen {Vermogen(b)}%, Vooruit {Vooruit(b)}");
        }

        public static List<uint> GetInputStroom()
        {
            List<uint> inputStream = new List<uint>();
            for (uint i = 0; i < 256; i++)
            {
                inputStream.Add(i);
            }
            return inputStream;
        }

        public static void PrintSet(HashSet<uint> x)
        {
            Console.Write("{");
            foreach (uint i in x)
                Console.Write($" {i}");
            Console.WriteLine($" }} ({x.Count} elementen)");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Opgave 1 ===");
            ToonInfo(210);
            Console.WriteLine();

            List<uint> inputStroom = GetInputStroom();

            Console.WriteLine("=== Opgave 2 ===");
            HashSet<uint> alle = Alle(inputStroom);
            PrintSet(alle);
            HashSet<uint> zonderLicht = ZonderLicht(inputStroom);
            PrintSet(zonderLicht);
            HashSet<uint> metWagon = MetWagon(inputStroom);
            PrintSet(metWagon);
            HashSet<uint> groter6 = SelecteerID(inputStroom, 6, 7);
            PrintSet(groter6);
            Console.WriteLine();

            Console.WriteLine("=== Opgave 3a ===");
            HashSet<uint> opg3a = Opdr3a(inputStroom);
            PrintSet(opg3a);
            foreach (uint b in opg3a)
            {
                ToonInfo(b);
            }
            Console.WriteLine();

            Console.WriteLine("=== Opgave 3b ===");
            HashSet<uint> opg3b = Opdr3b(inputStroom);
            PrintSet(opg3b);
            foreach (uint b in opg3b)
            {
                ToonInfo(b);
            }
            Console.WriteLine();
        }
    }
}