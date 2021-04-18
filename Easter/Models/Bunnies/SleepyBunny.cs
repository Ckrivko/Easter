using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
{
    public class SleepyBunny : Bunny
    {
       // private int energy;  
        private const int defaultEnergy = 50;
        public SleepyBunny(string name) 
            : base(name, defaultEnergy)
        {
        
        
        }
        

        public override void Work()  // problem!
        {

            base.Work();
            this.Energy -= 5;
            
        }


    }
}
