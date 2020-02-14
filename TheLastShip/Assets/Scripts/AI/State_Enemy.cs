using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Enemy : MonoBehaviour, IState
{
    public string StateName;

    public State_Enemy()
    {
        this.StateName = "None";
    }

    public virtual void Fly(AI_Enemy ai)
    {
        Debug.Log("Fly");
    }

    public virtual void Shoot(AI_Enemy ai)
    {
        Debug.Log("Shoot");
    }

    public virtual void Suicide(AI_Enemy ai)
    {
        Debug.Log("Suicide");
    }
}
