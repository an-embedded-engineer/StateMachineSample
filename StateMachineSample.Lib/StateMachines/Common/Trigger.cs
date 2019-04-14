using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public abstract class Trigger
    {
        public string Name { get; }

        public Effect Effect { get; }

        public Trigger(string name, Effect effect = null)
        {
            this.Name = name;

            this.Effect = effect;
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
