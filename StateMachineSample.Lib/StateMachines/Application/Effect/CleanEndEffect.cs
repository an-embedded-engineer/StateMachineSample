using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public sealed class CleanEndEffect : Effect
    {
        public static CleanEndEffect Instance { get; private set; } = new CleanEndEffect();

        public CleanEndEffect() : base("Clean End Effect")
        {
        }

        protected override void ExecuteAction(StateMachine context)
        {
            var stm = context.GetAs<ModelStateMachine>();

            var model = stm.Model;

            model.CleanEnd();
        }
    }
}
