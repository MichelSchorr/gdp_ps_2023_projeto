using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour
{

    [SerializeField] private float jumpHeight;
    private float jumpForce;
    
    [SerializeField] private GroundCheck _groundCheck = null;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    
    
    public void Do(bool wantJump)
    {
        if (wantJump && _groundCheck.GetOnGround())
        {
            jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * Mathf.Pow(_rigidbody2D.mass,2) * jumpHeight);
            _rigidbody2D.AddForce(jumpForce*Vector2.up, ForceMode2D.Impulse);
        }
    }
}
