using Easter.Models.Bunnies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories.Contracts
{
    public class BunnyRepository : IRepository<IBunny>
    {

        private List<IBunny> bunnies;

        public BunnyRepository()
        {
            bunnies = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models => this.bunnies.AsReadOnly();

        public void Add(IBunny model)
        {
            bunnies.Add(model);
        }

        public IBunny FindByName(string name)
        {
            IBunny bunny = bunnies.FirstOrDefault(x => x.Name == name);
            return bunny;
        }

        public bool Remove(IBunny model)
        {
            if (bunnies.Any(x => x.Name == model.Name))
            {
                bunnies.Remove(model);
                return true;
            }

            return false;
        }
    }
}
