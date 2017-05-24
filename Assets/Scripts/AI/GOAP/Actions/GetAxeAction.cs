using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.Actions {
    class GetAxeAction : GoapAction {

        public GetAxeAction() {
            RegisterEffect(GoapConditionKey.HasAxe, true);
            RegisterPrecondition(GoapConditionKey.IsTired, false);    
        }

        public override bool Complete() {
            throw new NotImplementedException();
        }

        public override void Perform() {
            Debug.Log("Getting axe");
        }

        public override bool CanPerform() {
            return true;
        }
    }
}
