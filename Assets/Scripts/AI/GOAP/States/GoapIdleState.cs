using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.GOAP.States {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapIdleState : AbstractState {
        private readonly EntityWanderBehaviour _wander;

        public GoapIdleState(GoapAgent agent) : base(agent) {
            _wander = new EntityWanderBehaviour(agent.Entity);
        }

        public override void Enter() {
            _wander.Reset();
            Debug.Log("AI Idling state");
        }

        public override void Execute() {
            var plan = Agent.ActionQueue;
            Agent.Entity.PlayAnimation(Animation.walk);
            //Check whether there is a requested plan active
            if (plan.Count > 0) Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Moving);
            else Agent.Entity.SetBehaviour(_wander);
        }
    }
}