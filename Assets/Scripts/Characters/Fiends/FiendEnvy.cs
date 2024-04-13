using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendEnvy : FiendBase
    {
        // Update is called once per frame
        void Update()
        {
            //SetMovementDirectionWithInput();
        }

        private void FixedUpdate()
        {
            MoveRigidbody();
        }
    }
}

