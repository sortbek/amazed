using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.GOAP {
    public class GoapPlan {

        public Dictionary<GoapCondition, bool> Plan { get; private set; }

        public GoapPlan() {
            Plan = new Dictionary<GoapCondition, bool>();
        }

        public GoapPlan(GoapCondition cond, bool result) : this() {
            Plan[cond] = result;
        }

        public GoapPlan(GoapAction action) {
            Plan = action.Effects;
        }

        public GoapPlan Add(GoapCondition cond, bool result) {
            Plan[cond] = result;
            return this;
        }
    }
}
