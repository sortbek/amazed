﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class GetHealthAction : GoapAction{

        public override void Init() {
            RegisterPrecondition(GoapCondition.IsDamaged, true);
            RegisterEffect(GoapCondition.IsDamaged, false);
        }

        public override void Execute() {
            Debug.Log("Getting health");
        }

        public override bool Completed() {
            return true;
        }
    }
}