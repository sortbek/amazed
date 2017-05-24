using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Entity;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP {
    public class GoapAgent : MonoBehaviour {

        public LivingEntity Entity { get; private set; }
        public GoapAction CurrentAction { get; set; }
        public HashSet<GoapAction> ActionRegister { get; private set; }
        public GoapStateMachine StateMachine { get; private set; }

        private readonly Dictionary<GoapConditionKey, bool> _agentState;
        private readonly Queue<GoapAction> _actionQueue;

        public GoapAgent() {
            Entity = new LivingEntity() {Energy = 10f, Health = 100f};
            StateMachine = new GoapStateMachine(this);
            _actionQueue = new Queue<GoapAction>();
            _agentState = new Dictionary<GoapConditionKey, bool>();
            CurrentAction = null;
            ActionRegister = new HashSet<GoapAction>();
        }

        public void CallAction(GoapAction action) {
            foreach (var effect in action.Effects)
                _agentState[effect.Key] = effect.Value;
        }

        public bool Is(GoapConditionKey key, bool val) {
            return _agentState[key] == val;
        }

        void Start() {
            LoadActions();
            StateMachine.ChangeState(GoapStateMachine.StateType.Idle);
        }

        void Update() {
            StateMachine.Update();
        }

        public Queue<GoapAction> GetQueue() {
            return _actionQueue;
        }

        public IEnumerable<GoapAction> GetUsable() {
            return ActionRegister.Select(x => x).Where(x => x.CanPerform());
        }

        public Dictionary<GoapConditionKey, bool> GetAgentState() {
            return _agentState;
        }

        private void LoadActions() {
            var actions = GetComponents<GoapAction>();
            foreach (var action in actions)
                ActionRegister.Add(action);
        }

    }
}
