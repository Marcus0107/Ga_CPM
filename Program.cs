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
            List<Individuum> bestOfGeneration = new List<Individuum>();

            GA ga = new GA(pop);

            for (int i = 0; i < 5000; i++)
            {
                ga.Selection();
                ga.createNewChildren();
                ga.Mutatation(1);

                ga.parents.sortByFitnessDescending();
                ga.children.sortByFitnessDescending();

                List<Individuum> newGen = new List<Individuum>();
                newGen.AddRange(ga.parents.population.GetRange(0, 25));
                newGen.AddRange(ga.children.population.GetRange(0, 25));

                ga.setNewParents(new Population(new List<Individuum>(newGen)));
                ga.parents.calculateFittnessForPopulation();
                ga.parents.sortByFitnessDescending();
                bestOfGeneration.Add(ga.parents.population[0]);
            }

            Population bestOf = new Population(bestOfGeneration);
            bestOf.sortByFitnessDescending();
            Debug.WriteLine(bestOf.ToString());




            //    Debug.WriteLine("Parents best:\n" + ga.parents.population[0].fittnes.ToString());
            //    Debug.WriteLine("Parents worst:\n" + ga.parents.population[49].fittnes.ToString());

            //    Debug.WriteLine("Children best:\n" + ga.children.population[0].fittnes.ToString());
            //    Debug.WriteLine("Children worst:\n" + ga.children.population[49].fittnes.ToString());
        }

        public static List<Allel> readCitiesFromFile()
        {
            List<Allel> individumGen = new List<Allel>();
            using (var reader = new StreamReader(@"C:\Users\Marcus\Dropbox\Master Semester 2\Genetische Algorithmen\76_städte.csv"))
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
