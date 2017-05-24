using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.Actions {
    public class GoSleepAction  : GoapAction{

        public GoSleepAction() {
            RegisterEffect(GoapConditionKey.IsTired, false);    
        }

        public override void Perform() {
            Debug.Log("Sleeping");
        }

        public override bool Complete() {
            throw new NotImplementedException();
        }

        public override bool CanPerform() {
            return true;
        }
    }
}
