using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class GA
    {
        Population selectedParents;
        Population parents;
        Population children;
        Random rnd = new Random();

        public GA(Population population)
        {
            parents = population;
            children = new Population();
            selectedParents = new Population();
        }

        public void Selection()
        {
            for (int i = 0; i < 50; i++)
            {
                selectedParents.addIndividuum(parents.population[rnd.Next(0, 50)]);
            }
        }
        public void Mutatation(int pChange)
        {
            Random rnd = new Random();
            List<int> toHits = new List<int>();
            for (int i = 0; i < pChange; i++)
            {
                toHits.Add(rnd.Next(0, 100));
            }
            foreach (Individuum ind in parents.population)
            {
                if (toHits.Contains(rnd.Next(0, 100)))
                {
                    Console.WriteLine("Element hit");
                    int start = rnd.Next(0, ind.gen.Count);
                    int end = rnd.Next(start, ind.gen.Count);
                    ind.gen.Reverse(start, end - start);
                }
            }
        }
        public void Crossover(Individuum mother, Individuum father)
        {
            Random rnd = new Random();
            Individuum child1 = new Individuum();
            Individuum child2 = new Individuum();
            List<Allel> genNewPart = new List<Allel>();
            List<KeyValuePair<Allel, int>> newPositions = new List<KeyValuePair<Allel, int>>();
            int crossoverPoint = rnd.Next(8, parents.population[0].gen.Count);


            for (int i = crossoverPoint; i < mother.gen.Count; i++)
            {
                Allel all = mother.gen[i];
                int position = father.gen.FindIndex(ax => ax.id == all.id);
                newPositions.Add(new KeyValuePair<Allel, int>(all, position));
            }
            newPositions.Sort((ind1, ind2) => ind1.Value.CompareTo(ind2.Value));

            foreach (KeyValuePair<Allel, int> kvp in newPositions)
            {
                genNewPart.Add(kvp.Key);
            }
            child1.gen.AddRange(mother.gen.GetRange(0, crossoverPoint));
            child1.gen.AddRange(genNewPart);


            newPositions = new List<KeyValuePair<Allel, int>>();
            for (int i = crossoverPoint; i < father.gen.Count; i++)
            {
                Allel all = father.gen[i];
                int position = mother.gen.FindIndex(ax => ax.id == all.id);
                newPositions.Add(new KeyValuePair<Allel, int>(all, position));
            }
            newPositions.Sort((ind1, ind2) => ind1.Value.CompareTo(ind2.Value));

            foreach (KeyValuePair<Allel, int> kvp in newPositions)
            {
                genNewPart.Add(kvp.Key);
            }
            child2.gen.AddRange(father.gen.GetRange(0, crossoverPoint));
            child2.gen.AddRange(genNewPart);



            children.addIndividuum(child1);
            children.addIndividuum(child2);
            int gjhh = 0;
        }


        public void createNewChildren()
        {
            for (int i = 0; i < parents.population.Count; i += 2)
            {

            }
        }

    }
}
