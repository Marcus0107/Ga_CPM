using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class Individuum
    {
        public List<Allel> gen;
        public double fittnes;

        public Individuum()
        {
            gen = new List<Allel>();
        }
        public Individuum(List<Allel> list)
        {
            gen = list;
        }
        public override string ToString()
        {
            string text = "";
            foreach (Allel all in gen)
            {
                text += (all.id + ";");
            }
            return text;
        }
        public void calculateFitness()
        {
            int fit = 0;
            for (int i = 0; i < gen.Count-2; i++)
            {
                fit += euclid_2d(gen[i], gen[i + 1]);
            }
            fit += euclid_2d(gen[gen.Count - 1], gen[0]);
            fittnes = fit;
        }
        public int euclid_2d(Allel gen1, Allel gen2)
        {
            int difx;
            int dify;

            difx = gen1.x_nav - gen2.x_nav;
            dify = gen1.y_nav - gen2.y_nav;
            var xPower = difx * difx;
            var yPower = dify * dify;
            var root = Math.Sqrt(xPower + yPower);
            var conv = Convert.ToInt32(root);
            return Convert.ToInt32(Math.Abs(Math.Sqrt(difx * difx + dify * dify)));
        }
    }
}
