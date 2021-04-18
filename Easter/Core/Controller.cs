using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Repositories;
using Easter.Repositories.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Dyes.Contracts;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;
        private Workshop workshop;
        public Controller()
        {
            eggs = new EggRepository();
            bunnies = new BunnyRepository();
            workshop = new Workshop();
        }


        public string AddBunny(string bunnyType, string bunnyName)   //ok?
        {
            // Valid types are: "HappyBunny" and "SleepyBunny".

            if (bunnyType != "HappyBunny" && bunnyType != "SleepyBunny")
            {

                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidBunnyType));
            }
            IBunny bunny = null;

            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            bunnies.Add(bunny);
            return $"Successfully added {bunnyType} named {bunnyName}.";


        }

        public string AddDyeToBunny(string bunnyName, int power) //ok?
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            if (bunny == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentBunny));
            }
            Dye dye = new Dye(power);
            bunny.AddDye(dye);
            return $"Successfully added dye with power {power} to bunny {bunnyName}!";

        }

        public string AddEgg(string eggName, int energyRequired)
        {
            Egg egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return $"Successfully added egg: {eggName}!";
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);

            List<IBunny> orderedBunnies = bunnies.Models.Where(x => x.Energy >= 50).ToList();

            if (orderedBunnies.Count == 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.BunniesNotReady));

            }

            foreach (var bunny in orderedBunnies)
            {

                workshop.Color(egg, bunny);
                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                    if (orderedBunnies.Count == 0)
                    {
                        break;
                    }
                }
                if (egg.IsDone() == true)
                {
                    break;
                }
            }


            if (egg.IsDone() == false)
            {
                return $"Egg {eggName} is not done.";

            }
            return $"Egg {eggName} is done.";


        }

        public string Report()
        {

            List<IEgg> coloredEggs = eggs.Models.Where(x => x.IsDone() == true).ToList();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{coloredEggs.Count} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");

                List<IDye> orderedDyes = bunny.Dyes.Where(x => x.Power > 0).ToList();
                sb.AppendLine($"Dyes {orderedDyes.Count} not finished left");

            }

            return sb.ToString().TrimEnd();

        }
    }
}
