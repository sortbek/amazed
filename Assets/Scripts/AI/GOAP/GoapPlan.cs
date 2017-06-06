using System.Collections.Generic;

namespace Assets.Scripts.AI.GOAP {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapPlan {
        public GoapPlan() {
            Plan = new Dictionary<GoapCondition, bool>();
        }

        public GoapPlan(GoapCondition cond, bool result) : this() {
            Plan[cond] = result;
        }

        public GoapPlan(GoapAction action) {
            Plan = action.Effects;
        }

        public Dictionary<GoapCondition, bool> Plan { get; private set; }

        public GoapPlan Add(GoapCondition cond, bool result) {
            Plan[cond] = result;
            return this;
        }
    }
}