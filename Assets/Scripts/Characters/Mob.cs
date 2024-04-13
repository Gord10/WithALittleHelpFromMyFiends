using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : CharacterBase
{
    private Transform targetTransform;

    public void SetTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public void ManualFixedUpdate()
    {
        SetDirectionTowardsTarget(targetTransform.position);
        MoveRigidbody();
    }

}
