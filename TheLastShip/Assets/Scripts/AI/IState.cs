using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Fly(AI_Enemy ai);
    public void Suicide(AI_Enemy ai);
    public void Shoot(AI_Enemy ai);
}
