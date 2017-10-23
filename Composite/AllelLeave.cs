using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class AllelLeave : Composite
    {
        Allel singleAllel;

        public AllelLeave(Allel allel)
        {
            singleAllel = allel;
        }

        public List<Allel> createNewGen()
        {
            return new List<Allel>()
            {
                singleAllel
            };
        }
    }
}
