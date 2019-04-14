using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public class RunningStateMachine : StateMachine
    {
        public ModelStateMachine Parent { get; }

        public AirConditioner Model { get; }

        public RunningStateMachine(ModelStateMachine parent)
        {
            this.Parent = parent;

            this.Model = parent.Model;

            this.ChangeToInitialState();
        }

        protected override State GetInitialState()
        {
            return CoolState.Instance;
        }
    }
}
