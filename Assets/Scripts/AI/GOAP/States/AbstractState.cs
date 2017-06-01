using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.GOAP.States {

    // Created by:
    // Eelco Eikelboom
    // S1080542
    public abstract class AbstractState {

        protected readonly GoapAgent Agent;
        protected AbstractState(GoapAgent agent) {
            Agent = agent;
        }

        public abstract void Enter();
        public abstract void Execute();
    }
}
