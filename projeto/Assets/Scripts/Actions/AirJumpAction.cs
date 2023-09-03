using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpAction : MonoBehaviour
{
    
    [SerializeField] private new Rigidbody2D rigidbody2D = null;
    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private float jumpHeight;
    private float _jumpSpeed;
    
    [SerializeField, Range(0, 10)] private int airJumps;
    private int _airJumpCount = 0;

    
    
    public void Do()
    {
        if (_airJumpCount < airJumps)
        {
            _airJumpCount++;
            _jumpSpeed = rigidbody2D.mass * Mathf.Sqrt(-2f * (Physics2D.gravity.y * characterController.defaultGravityScale) * jumpHeight);
            _jumpSpeed = Mathf.Max((_jumpSpeed - rigidbody2D.velocity.y), 0f);
            
            //adiciona a velocidade necessaria para atingir a altura desejada. Sem negativar.
            rigidbody2D.AddForce(_jumpSpeed * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public void ResetAirJumpCount()
    {
        _airJumpCount = 0;
    }
}
