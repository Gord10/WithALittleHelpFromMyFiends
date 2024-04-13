using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    // Update is called once per frame
    void Update()
    {
        SetMovementDirectionWithInput();
    }

    void SetMovementDirectionWithInput()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        movementDirection = new(movementX, movementY);
        movementDirection = Vector2.ClampMagnitude(movementDirection, 1);
    }

    private void FixedUpdate()
    {
        MoveRigidbody();
    }
}
