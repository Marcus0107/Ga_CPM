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
          //  int rounds = int.Parse(args[0]);
            
            //Erstes Gen in reihenfolge aus der Liste
            //@ vor dem Pfad ist wichtig
            List<Allel> individumGen = readCitiesFromFile(@"C:\Users\Marcus\Dropbox\Master Semester 2\Genetische Algorithmen\76_städte.csv");

            Population pop = new Population();
            var rnd = new Random();

            //Erzeuge zufällige Gene für die initale population
            for (int i = 0; i < 50; i++)
            {
                pop.addIndividuum(new Individuum(new List<Allel>(individumGen)));
                individumGen.Shuffle(rnd);
            }

            //Berechne die Fittness für jedes einzelene Indiviuum der ersten Population
            pop.calculateFittnessForPopulation();

            GA ga = new GA(pop);

            for (int i = 0; i < 50000; i++)
            {
                //Führe Zyklus des Genetischen Algrothmus durch
                ga.Selection();
                ga.createNewChildren();
                ga.Mutatation(5);

                //Sortiere die Eltern und Kindern nach der Fittness
                ga.parents.sortByFitnessDescending();
                ga.children.calculateFittnessForPopulation();
                ga.children.sortByFitnessDescending();

                //Erzeuge neue Generation aus den besten von Eltern und Kindern
                List<Individuum> newGen = new List<Individuum>();
                newGen.AddRange(ga.parents.population.GetRange(0, 25));
                newGen.AddRange(ga.children.population.GetRange(0, 25));

                //Sortiere die neue GEneration nach der Fittness
                ga.setNewParents(new Population(new List<Individuum>(newGen)));
                ga.parents.calculateFittnessForPopulation();
                ga.parents.sortByFitnessDescending();

                //Schreibe den besten aus der Population auf die Konsole
                Debug.WriteLine(ga.parents.population[0].fittnes);
                if(i%500 == 0)
                {
                    Console.WriteLine(i);
                    GC.Collect();
                }
                ga.children = new Population();
            }
        }

        public static List<Allel> readCitiesFromFile(string pathToFile)
        {
            List<Allel> individumGen = new List<Allel>();
            using (var reader = new StreamReader(pathToFile))
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
