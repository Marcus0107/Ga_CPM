using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Genetischer_Algorithmus
{
    public class Program
    {
        static void Main(string[] args)
        {
            int cityCount = 6;
            int roundCount = 5000;
            int mutationProbability = 5;
            int populationSize = 50;

            long startTimeInMilliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            run_GA_Algorith(cityCount, roundCount, mutationProbability, populationSize);

            long endTimeInMilliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Debug.WriteLine("Genetic Algorithmn \t City count: " + cityCount + "\tRuntime: " + (endTimeInMilliseconds - startTimeInMilliseconds) + " ms");


            startTimeInMilliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            run_brute_force(cityCount);

            endTimeInMilliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Debug.WriteLine("Brute Force \t City count: " + cityCount + "\tRuntime: " + (endTimeInMilliseconds - startTimeInMilliseconds) + " ms");
        }

        public static void run_brute_force(int cityCount)
        {
            List<Allel> individumGen = readCitiesFromFile(@"C:\Users\Marcus\Dropbox\Master Semester 2\Genetische Algorithmen\280_städte.csv");
            List<Individuum> indis = new List<Individuum>();

            foreach (var permutation in individumGen.GetRange(0, cityCount).GetPermutations())
            {
                indis.Add(new Individuum(permutation.ToList()));
            }
            Population pop = new Population(indis);
            pop.calculateFittnessForPopulation();
            pop.sortByFitnessDescending();
        }

        public static void run_GA_Algorith(int cityCount, int roundCount, int mutationProbabilityInPercent, int populationSize = 50)
        {
            //Erstes Gen in reihenfolge aus der Liste
            //@ vor dem Pfad ist wichtig
            List<Allel> allIndividumGen = readCitiesFromFile(@"C:\Users\Marcus\Dropbox\Master Semester 2\Genetische Algorithmen\280_städte.csv");
            List<Allel> individumGen = allIndividumGen.GetRange(0, cityCount);

            Population pop = new Population();
            var rnd = new Random();

            //Erzeuge zufällige Gene für die initale population
            for (int i = 0; i < populationSize; i++)
            {
                pop.addIndividuum(new Individuum(new List<Allel>(individumGen)));
                individumGen.Shuffle(rnd);
            }

            //Berechne die Fittness für jedes einzelene Indiviuum der ersten Population
            pop.calculateFittnessForPopulation();

            GA ga = new GA(pop);

            for (int i = 0; i < roundCount; i++)
            {
                //Führe Zyklus des Genetischen Algrothmus durch
                ga.Selection();
                ga.createNewChildren();
                ga.Mutatation(mutationProbabilityInPercent);

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

                if (i % 500 == 0)
                {
                    //  Debug.WriteLine(i + "\t\t Fitness of the best from the population: " + ga.parents.population[0].fittnes);
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

}
