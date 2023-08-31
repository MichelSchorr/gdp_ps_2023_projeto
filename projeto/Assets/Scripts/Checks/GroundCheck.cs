using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private bool _onGround;
    private float _friction;
    private float _timeLastOnGround;
    
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private float collisionAngleSensitivity = 0.9f;

    [SerializeField] private float collisionIgnoreSeconds = 0.01f;
    


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
        _onGround = false;
        _friction = 0f;
    }


    private void EvaluateCollision(Collision2D collision)
    {
        _onGround = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            _onGround = _onGround || (normal.y >= collisionAngleSensitivity);
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

        _friction = 0;

        if (material != null)
        {
            _friction = material.friction;
        }
    }

    public bool GetOnGround()
    {
        return _onGround;
    }

    public float GetFriction()
    {
        return _friction;
    }
}
