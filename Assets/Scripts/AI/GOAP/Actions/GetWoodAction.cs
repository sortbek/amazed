using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.Actions {
    public class GetWoodAction  : GoapAction{

        public GetWoodAction() {
            RegisterEffect(GoapConditionKey.HasWood, true);
            RegisterPrecondition(GoapConditionKey.HasAxe, true);
        }

        public override void Perform() {
            Debug.Log("Getting wood");
        }

        public override bool Complete() {
            throw new NotImplementedException();
        }

        public override bool CanPerform() {
            return true;
        }
    }
}
