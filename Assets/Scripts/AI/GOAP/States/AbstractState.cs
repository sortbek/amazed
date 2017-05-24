using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.GOAP.States {
    public abstract class AbstractState {

        protected readonly GoapAgent _agent;
        protected AbstractState(GoapAgent agent) {
            _agent = agent;
        }

        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
    }
}
