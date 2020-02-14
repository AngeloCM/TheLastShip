using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoFly : State_Enemy
{
    public DoFly()
    {
        this.StateName = "Fly";
    }

    public override void Fly(AI_Enemy ai)
    {
        ai.Fly();
        base.Fly(ai);
    }
}
