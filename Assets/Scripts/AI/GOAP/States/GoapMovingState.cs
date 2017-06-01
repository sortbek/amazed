using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.GOAP.States {

    // Created by:
    // Hugo Kamps
    // S1084074
    public class GoapMovingState : AbstractState {

        public Vector3[] Path;
        public int CurrentIndex;
        public GoapMovingState(GoapAgent agent) : base(agent) { }

        public override void Enter() {
            Debug.Log("AI Moving state");
            Path = null;
            CurrentIndex = 0;
            Agent.Entity.Target = GameManager.Instance.GetEndpoint();
            PathRequestManager.RequestPath(Agent.transform.position, Agent.Entity.Target, OnPathFound);
        }

        public override void Execute() {
            if (Path != null && Path.Length > 0) {
                Agent.Entity.PlayAnimation(Animation.run);
                Path[CurrentIndex].y = 0.0f;

                Agent.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Agent.transform.forward, Path[CurrentIndex] - Agent.transform.position,
                    Time.deltaTime * Agent.Entity.Speed, 0.0f));
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, Path[CurrentIndex], Time.deltaTime * Agent.Entity.Speed);

                if (CurrentIndex == Path.Length - 1) Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
                if (Vector3.Distance(Agent.transform.position, Path[CurrentIndex]) < 0.1f) CurrentIndex += 1;
            }
        }

        public void OnPathFound(Vector3[] newPath, bool pathFound) {
            if (pathFound) {
                Path = newPath;
            }
        }
    }
}
