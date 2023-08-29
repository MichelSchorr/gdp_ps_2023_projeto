using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private bool onGround;
    private float friction;
    private float collisionAngleSensitivity = 0.9f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        friction = 0f;
    }


    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            onGround = onGround || (normal.y >= collisionAngleSensitivity);
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

        friction = 0;

        if (material != null)
        {
            friction = material.friction;
        }
    }

    public bool GetOnGround()
    {
        return onGround;
    }

    public float GetFriction()
    {
        return friction;
    }
}
