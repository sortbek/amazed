using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Actions;
using Assets.Scripts.AI.Entity;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP {

    [RequireComponent(typeof(LivingEntity))]
    public class GoapAgent : MonoBehaviour {

        public LivingEntity Entity { get; private set; }
        public GoapStateMachine StateMachine { get; private set; }
        public Dictionary<GoapCondition, bool> AgentState { get; private set; }
        public Queue<GoapAction> ActionQueue { get; private set; }
        public HashSet<GoapAction> Actions { get; private set; }
        public GoapPlanner Planner { get; private set; }

        // Registers all possible agent states
        private void LoadDefaultState() {
            foreach (var condition in Enum.GetValues(typeof(GoapCondition)).Cast<GoapCondition>())
                AgentState[condition] = false;
        }

        void Awake() {
            StateMachine = new GoapStateMachine(this);
            ActionQueue = new Queue<GoapAction>();
            Actions = new HashSet<GoapAction>();
            Planner = new GoapPlanner(this);
            AgentState = new Dictionary<GoapCondition, bool>();
            LoadDefaultState();
            Entity = GetComponent<LivingEntity>();
            StateMachine.ChangeState(GoapStateMachine.StateType.Idle);
        }

        void Start() {
            LoadActions();

            SetState(GoapCondition.IsDamaged, true);
            SetState(GoapCondition.IsTired, true);

            Planner.Plan(new GoapPlan(GoapCondition.InAttackRange, true).Add(GoapCondition.IsDamaged, false));
        }

        // Updates the agent state by altering the given condition
        public void SetState(GoapCondition cond, bool val) {
            AgentState[cond] = val;
        }

        //Loads all action components based on the assigned GoapActions in the unity inspector
        private void LoadActions() {
            var actions = GetComponents<GoapAction>();
            foreach (var action in actions)
                Actions.Add(action);
        }

        void Update() {
            if(!Entity.Dead) StateMachine.Update();
        }

    }
}
