using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class Population
    {
        public List<Individuum> population;

        public Population()
        {
            population = new List<Individuum>();
        }

        public void addIndividuum(Individuum ind)
        {
            population.Add(ind);
        }

        public void calculateFittnessForPopulation()
        {
            foreach(Individuum ind in population)
            {
                ind.calculateFitness();
            }
        }

        public void sortByFitnessDescending()
        {
            population.Sort((ind2, ind1) => ind1.fittnes.CompareTo(ind2.fittnes));
        }

        public override string ToString()
        {
            string text = "";
           foreach(Individuum ind in population)
            {
                text += "Fitness " + ind.fittnes+"\n";
            }
            return text;
        }
    }
}
