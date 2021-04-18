using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {

        }

        public void Color(IEgg egg, IBunny bunny)   ///problemmm
        {

            while (egg.IsDone() == false && bunny.Energy > 0 && bunny.Dyes.Any(x => x.IsFinished() == false))
            {

                egg.GetColored();

                bunny.Work();
                IDye dye = bunny.Dyes.FirstOrDefault(x => x.IsFinished()==false);

                dye.Use();
            
            }


        }
    }
}
