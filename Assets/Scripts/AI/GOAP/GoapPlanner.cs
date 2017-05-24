using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.GOAP {
    public class GoapPlanner {

        private readonly GoapAgent _agent;
        public GoapPlanner(GoapAgent agent) {
            _agent = agent;
        }

        public void Plan(GoapPlan plan, int attempt= 0) {
            var valid = false;
            foreach (var action in _agent.Actions) {
                if (!Executable(action) || _agent.ActionQueue.Contains(action)) continue;
                Update(action);
                _agent.ActionQueue.Enqueue(action);
                valid = IsValid(plan);
                if (valid) break;
            }
            if (!valid)
                Plan(plan, ++attempt);
        }

        private void Update(GoapAction action) {
            foreach (var pair in action.Effects)
                _agent.AgentState[pair.Key] = pair.Value;
        }

        private bool Executable(GoapAction action) {
            return action.Preconditions.Count == 0 || action.Preconditions.All(pair => _agent.AgentState[pair.Key] == pair.Value);
        }

        private bool IsValid(GoapPlan plan) {
            return plan.Plan.All(pair => _agent.AgentState[pair.Key] == pair.Value);
        }
    }
}
