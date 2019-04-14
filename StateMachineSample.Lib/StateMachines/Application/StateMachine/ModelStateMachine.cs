using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public class ModelStateMachine : StateMachine
    {
        public AirConditioner Model { get; }

        public ModelStateMachine(AirConditioner model)
        {
            this.Model = model;

            this.ChangeToInitialState();
        }

        protected override State GetInitialState()
        {
            return InitialState.Instance;
        }
    }
}
