using Assets.Scripts.AI.Actions;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States{
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapActionState : AbstractState{
        private GoapAction _current;
        private bool _running;

        public GoapActionState(GoapAgent agent) : base(agent){
            _current = null;
            _running = false;
        }

        public override void Enter(){
            Debug.Log("Action state");
            _running = true;
            _current = new DoAttackAction(){Agent = Agent};
            _current.Execute();
        }

        public override void Execute(){
            //Check if there's a current action running
            if (!_running){
                // Execute the first action in the queue
                _running = true;
                _current = Agent.ActionQueue.Dequeue();
                _current.Execute();
            }
            else{
                //Check whether the current action is completed
                if (!_current.Completed()) return;
                //Change the state based on the amount of actions left
                Agent.StateMachine.ChangeState(Agent.ActionQueue.Count > 0
                    ? GoapStateMachine.StateType.Moving
                    : GoapStateMachine.StateType.Idle);
                _running = false;
            }
        }
    }
}