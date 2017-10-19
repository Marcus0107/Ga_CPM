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
        }

        public void Selection()
        {
            for (int i = 0; i < 25; i++)
            {
                selectedParents.addIndividuum(parents.poulation[rnd.Next(0, 50)]);
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
            foreach (Individuum ind in parents.poulation)
            {
                if (toHits.Contains(rnd.Next(0, 100)))
                {
                    Debug.WriteLine("Element hit");
                    int start = rnd.Next(0, 76);
                    int end = rnd.Next(start, 76);
                    ind.gen.Reverse(start, end - start);
                }
            }
        }
        public void Crossover()
        {

        }

    }
}
