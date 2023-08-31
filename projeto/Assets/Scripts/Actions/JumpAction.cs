using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour
{

    [SerializeField] private GroundCheck _groundCheck = null;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    
    [SerializeField] private float jumpHeight;
    private float _jumpForce;
    [SerializeField, Range(0, 10)] private int airJumps;
    private int _airJumpCount=0;
    private bool _onGround;
    
    
    
    public void Do(bool wantJump)
    {
        _onGround = _groundCheck.GetOnGround();


        //Esse check depende do Do() ser rodado dentro do FixedUpdate() sempre;
        if (_onGround)
        {
            _airJumpCount = 0;
        }

        if (wantJump)
        {
            if (_onGround)
            {
                _jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * Mathf.Pow(_rigidbody2D.mass,2) * jumpHeight);
                _rigidbody2D.AddForce(_jumpForce*Vector2.up, ForceMode2D.Impulse);
            }else if (_airJumpCount<airJumps)
            {
                _airJumpCount++;
                _jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * Mathf.Pow(_rigidbody2D.mass,2) * jumpHeight);
                _rigidbody2D.AddForce(_jumpForce*Vector2.up, ForceMode2D.Impulse);
            }
        }
    }
}
