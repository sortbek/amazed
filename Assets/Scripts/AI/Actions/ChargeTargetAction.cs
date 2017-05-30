using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class ChargeTargetAction : GoapAction {

        public override void Init() {
            RegisterEffect(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.IsTired, false);
        }

        public override void Execute() {
            Debug.Log("Finding target");
        }

        public override bool Completed() {
            //TODO iets returnen dat ie m gevonden heeft (afstand tussen char en AI ofzo)
            return true;
        }

    }
}
