using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class Population
    {
        public List<Individuum> poulation;

        public Population()
        {
            poulation = new List<Individuum>();
        }

        public void addIndividuum(Individuum ind)
        {
            poulation.Add(ind);
        }

        public void calculateFittnessForPopulation()
        {
            foreach(Individuum ind in poulation)
            {
                ind.calculateFitness();
            }
        }

        public void sortByFitnessDescending()
        {
            poulation.Sort((ind2, ind1) => ind1.fittnes.CompareTo(ind2.fittnes));
        }

        public override string ToString()
        {
            string text = "";
           foreach(Individuum ind in poulation)
            {
                text += "Fitness " + ind.fittnes+"\n";
            }
            return text;
        }
    }
}
