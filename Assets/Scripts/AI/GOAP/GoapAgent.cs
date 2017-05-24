﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Actions;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP {
    public class GoapAgent : MonoBehaviour {

        public GoapStateMachine StateMachine { get; private set; }
        public Dictionary<GoapCondition, bool> AgentState { get; private set; }
        public Queue<GoapAction> ActionQueue { get; private set; }
        public HashSet<GoapAction> Actions { get; private set; }
        public GoapPlanner Planner { get; private set; }

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
            StateMachine.ChangeState(GoapStateMachine.StateType.Idle);
        }

        void Start() {
            LoadActions();

            SetState(GoapCondition.IsTired, true);

            Planner.Plan(new GoapPlan(GoapCondition.InRange, true));
        }

        public void SetState(GoapCondition cond, bool val) {
            AgentState[cond] = val;
        }

        private void LoadActions() {
            var actions = GetComponents<GoapAction>();
            foreach (var action in actions)
                Actions.Add(action);
        }

        void Update() {
            StateMachine.Update();
        }

    }
}