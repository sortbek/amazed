using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.GOAP.States {
    public class GoapIdleState : AbstractState {

        private readonly EntityWanderBehaviour _wander;

        public GoapIdleState(GoapAgent agent) : base(agent) {
            _wander = new EntityWanderBehaviour();
        }

        public override void Enter() {
            Debug.Log("AI Idling state");
        }

        public override void Execute() {
            var plan = Agent.ActionQueue;
            Agent.Entity.PlayAnimation(Animation.idle);
            //Check whether there is a requested plan active
            if (plan.Count > 0) { 
                //Set the state to the moving state, since we found a plan
                Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Moving);
            } else {
                //Idle, there is no requested plan
                Agent.Entity.SetBehaviour(_wander);
            }
        }

    }
}
