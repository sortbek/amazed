﻿using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class DoAttackAction : GoapAction {
        public override void Execute() {
            Debug.Log("Attacking!");
            //attack
        }

        public override bool Completed() {
            //check if player is damaged
            return true;
        }

        public override GameObject GetTarget() {
            return null;
        }

        public override void Init() {
            RegisterPrecondition(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.IsTired, false);
            RegisterPrecondition(GoapCondition.IsDamaged, false);
        }
    }
}