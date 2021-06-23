using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkingHumanoid : Humanoid
{
    public abstract void WalkUp();

    public abstract void WalkDown();

    public abstract void WalkRight();

    public abstract void WalkLeft();
}
