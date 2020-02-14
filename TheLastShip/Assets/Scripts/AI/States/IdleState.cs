using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.States
{
    public class IdleState : FSM
    {
        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("ENTERED IDLE STATE");

            return true;
        }

        public override void UpdateState()
        {
            throw new NotImplementedException();
        }

        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("ENTERED IDLE STATE");
            return true;
        }
    }
}
