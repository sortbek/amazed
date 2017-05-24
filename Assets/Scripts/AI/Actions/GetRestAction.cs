using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class GetRestAction : GoapAction{

        public override void Init() {
            RegisterPrecondition(GoapCondition.IsTired, true);
            RegisterEffect(GoapCondition.IsTired, false);
        }

        public override void Execute() {
            Debug.Log("Getting rest");
        }

        public override bool Completed() {
            return true;
        }
    }
}
