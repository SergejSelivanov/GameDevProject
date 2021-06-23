using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkingHumanoid : Humanoid
{
    protected abstract void WalkUp();

    protected abstract void WalkDown();

    protected abstract void WalkRight();

    protected abstract void WalkLeft();
}
