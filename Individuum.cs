﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
  public  class Individuum
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
            foreach(Allel all in gen)
            {
                text += (all.id + ";");
            }
            return text;
        }


        public void calculateFitness()
        {
            double fit = 0.0;
            for(int i = 0; i < gen.Count - 1; i++)
            {
                int difx = gen[i].x_nav - gen[i + 1].x_nav;
                int dify = gen[i].y_nav - gen[i + 1].y_nav;
                fit += Math.Sqrt(difx * difx + dify * dify);
            }
            fittnes = fit;
        }
    }
}