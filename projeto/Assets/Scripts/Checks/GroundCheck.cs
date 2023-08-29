using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private bool onGround;
    private float friction;
    [SerializeField] private float collisionAngleSensitivity = 0.9f;
    [SerializeField] private Collider2D _collider2D;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.otherCollider == _collider2D)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }
        */
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        /*
        if (collision.otherCollider == _collider2D)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }
        */
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        /*
        if (collision.otherCollider == _collider2D)
        {
            onGround = false;
            friction = 0f;
            Debug.Log("Exit1");
        }
        Debug.Log("Exit2");
        */
        onGround = false;
        friction = 0f;
        Debug.Log("Exit");
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
