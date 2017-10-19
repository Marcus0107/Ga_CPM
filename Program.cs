using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Genetischer_Algorithmus
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Allel> individumGen = readCitiesFromFile();

            Population pop = new Population();
            var rnd = new Random();

            for (int i = 0; i < 50; i++)
            {
                individumGen.Shuffle(rnd);
                pop.addIndividuum(new Individuum(new List<Allel>(individumGen)));
            }

            pop.calculateFittnessForPopulation();

            Console.WriteLine("Before Sorting");
            Console.WriteLine(pop.ToString());

            pop.sortByFitnessDescending();

            Console.WriteLine("After Sorting");
            Console.WriteLine(pop.ToString());

            GA ga = new GA(pop);

            ga.Crossover(pop.population[0], pop.population[1]);

            //Console.ReadKey(true);

        }

        public static List<Allel>  readCitiesFromFile()
        {
            List<Allel> individumGen = new List<Allel>();
            using (var reader = new StreamReader(@"C:\Users\Marcus\Dropbox\Master Semester 2\Genetische Algorithmen\20_städte.csv"))
            {
                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var vals = line.Split(';');
                    individumGen.Add(new Allel(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2])));
                }
            }
            return individumGen;
        }
    }


    public static class IEnumerableExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count; i++)
                list.Swap(i, rnd.Next(i, list.Count));
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
