using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class Allel
    {
        public int id;
        public int x_nav;
        public int y_nav;

        public Allel(int id, int x, int y)
        {
            this.id = id;
            this.x_nav = x;
            this.y_nav = y;
        }
    }
}
