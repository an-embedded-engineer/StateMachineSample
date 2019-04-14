using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public abstract class Effect
    {
        string Name { get; }

        protected abstract void ExecuteAction(StateMachine context);

        public Effect(string name)
        {
            this.Name = name;
        }

        public void Execute(StateMachine context)
        {
            Messenger.Send($"Execute : {this.Name}");

            this.ExecuteAction(context);
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
