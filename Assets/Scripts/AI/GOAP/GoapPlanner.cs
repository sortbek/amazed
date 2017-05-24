using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.GOAP {
    public class GoapPlanner {

        private GoapAgent _agent;
        public GoapPlanner(GoapAgent agent) {
            this._agent = agent;
        }

        public void Plan(Dictionary<GoapConditionKey, bool> goal) {
            var root = new Node(0, null, null, goal);
            var leaves = new HashSet<Node>();

        }

        private Dictionary<GoapConditionKey, bool> ApplyState(Dictionary<GoapConditionKey, bool> old,
            Dictionary<GoapConditionKey, bool> updated) {
            var rtn = new Dictionary<GoapConditionKey, bool>();
            foreach (var pair in old)
                rtn[pair.Key] = old[pair.Key];
            foreach (var pair in updated)
                rtn[pair.Key] = updated[pair.Key];
            return rtn;
        }

        private bool InState(Dictionary<GoapConditionKey, bool> preconditions) {
            var clear = true;
            foreach (var pair in preconditions)
                clear = clear && _agent.GetAgentState()[pair.Key] == preconditions[pair.Key];
            return clear;
        }

        private bool BuildGraph(Node parent, HashSet<Node> leaves, HashSet<GoapAction> actions,
            Dictionary<GoapConditionKey, bool> goal) {
            var rtn = false;
            foreach (GoapAction action in actions) {
                if (InState(action.Preconditions)) {
                    var currentState = ApplyState(parent.State, action.Effects);
                    var node = new Node(parent.Cost + action.Cost, action, parent, currentState);
                    rtn = true;
                    foreach (var pair in goal)
                        if (currentState[pair.Key] != pair.Value) rtn = false;
                    if (rtn)
                        leaves.Add(node);
                    else {
                        
                    }
                }
            }
            return rtn;
        }
    }

    public class Node {
        
        public int Cost { get; set; }
        public GoapAction Action { get; set; }
        public Node Parent { get; set; }
        public Dictionary<GoapConditionKey, bool> State { get; set; }

        public Node(int cost, GoapAction action, Node parent, Dictionary<GoapConditionKey, bool> state) {
            Cost = cost;
            Action = action;
            Parent = parent;
            State = state;
        }
    }

}
