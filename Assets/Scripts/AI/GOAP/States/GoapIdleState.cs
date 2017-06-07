using Assets.Scripts.AI.Entity.Behaviours;
using Assets.Scripts.World;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.GOAP.States {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapIdleState : AbstractState {
        private readonly EntityWanderBehaviour _wander;
        private Character.Character _character;

        public GoapIdleState(GoapAgent agent) : base(agent) {
            _wander = new EntityWanderBehaviour(agent.Entity);
        }

        public override void Enter() {
            if (_character == null) _character = GameManager.Instance.Character;
            _wander.Reset();
            Debug.Log("AI Idling state");
        }

        public override void Execute() {
            var plan = Agent.ActionQueue;
            Agent.Entity.PlayAnimation(Animation.Walk);
            //Check whether there is a requested plan active
            if (plan.Count > 0)
                Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Moving);
            //Check if the character is visible, if so, activate the plan
            else if(Agent.Entity.Perspective.Visible(_character.gameObject))
                Agent.Planner.Plan(new GoapPlan(GoapCondition.InAttackRange, true));
            //No plan, just wander 
            else Agent.Entity.SetBehaviour(_wander);
        }
    }
}