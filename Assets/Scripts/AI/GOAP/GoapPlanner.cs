using System.Linq;

namespace Assets.Scripts.AI.GOAP {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapPlanner {
        private readonly GoapAgent _agent;

        public GoapPlanner(GoapAgent agent) {
            _agent = agent;
        }


        // Attempts to plan a set of actions to satisfy the requested GoapPlan instance.
        public void Plan(GoapPlan plan, int attempt = 0) {
            var valid = false;
            if (attempt >= _agent.Actions.Count) return;
            // Loop through each possible action
            foreach (var action in _agent.Actions) {
                // Check if the current action is both executable and currently not in the queue.
                if (!Executable(action) || _agent.ActionQueue.Contains(action)) continue;
                Update(action);
                //Add the action to the queue
                _agent.ActionQueue.Enqueue(action);
                valid = IsValid(plan);
                if (valid) break;
            }
            // If the current goal isn't satisfied yet try to plan it again using a simple recursive call.
            // The state of the agent is changed after each iteration, therefore earlier actions could be useful now,
            // hence the recursive call.
            if (!valid)
                Plan(plan, ++attempt);
        }

        //Update the state of the agent using the effects of the given action 
        private void Update(GoapAction action) {
            foreach (var pair in action.Effects)
                _agent.SetState(pair.Key, pair.Value);
        }

        //Determines whether the given action is executable or not based on the preconditions 
        //of the action and the state of the agent
        private bool Executable(GoapAction action) {
            return action.Preconditions.Count == 0 ||
                   action.Preconditions.All(pair => _agent.AgentState[pair.Key] == pair.Value);
        }

        //Determines whether the given plan is reached based on the state of the agent
        private bool IsValid(GoapPlan plan) {
            return plan.Plan.All(pair => _agent.AgentState[pair.Key] == pair.Value);
        }
    }
}