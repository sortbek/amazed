﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    public class GoapIdleState : AbstractState {

        public GoapIdleState(GoapAgent agent) : base(agent) { }

        public override void Enter() {
            Debug.Log("AI Idling state");
        }

        public override void Execute() {
            var plan = Agent.ActionQueue;
            //Check whether there is a requested plan active
            if (plan.Count > 0) { 
                //Set the state to the moving state, since we found a plan
                Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Moving);
            } else {
                //Idle, there is no requested plan
                Agent.Entity.SetBehaviour(new EntityWanderBehaviour());
            }
        }

    }
}