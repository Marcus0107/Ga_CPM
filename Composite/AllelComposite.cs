using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetischer_Algorithmus
{
    public class AllelComposite : Composite
    {
        List<Composite> children;
        List<Allel> possibleCitys;
        List<List<Allel>> candidates;
        Allel ownAllel;

        public AllelComposite(List<Allel> allels)
        {
            createNewLeaves();
        }


        public void createNewLeaves()
        {
            ownAllel = possibleCitys[0];
            possibleCitys.RemoveAt(0);
            if (possibleCitys.Count > 1)
            {
                children.Add(new AllelComposite(possibleCitys));
            }
            else
            {
                children.Add(new AllelLeave(possibleCitys[0]));
            }
        }

        public List<Allel> createNewGen()
        {
            foreach (Composite leave in children)
            {
                List<Allel> newCandiate = new List<Allel>();
                newCandiate.AddRange(leave.createNewGen());
                newCandiate.Add(ownAllel);
                candidates.Add(newCandiate);
            }
            return null;
        }
    }
}
