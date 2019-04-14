using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class InitializedTrigger : Trigger
    {
        public static InitializedTrigger Instance { get; private set; } = new InitializedTrigger();

        public InitializedTrigger() : base("Initialized Trigger")
        {
        }
    }
}
