using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//[CreateAssetMenu(fileName = "New Jump Action", menuName = "Action/Jump")]
public class JumpAction : Action
{

    [SerializeField] private new Rigidbody2D rigidbody2D = null;
    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private float jumpHeight;
    private float _jumpSpeed;
    
    
    
    
    public void Do()
    {
        _jumpSpeed = rigidbody2D.mass * Mathf.Sqrt(-2f * (Physics2D.gravity.y * characterController.defaultGravityScale) * jumpHeight);
        _jumpSpeed = Mathf.Max((_jumpSpeed - rigidbody2D.velocity.y), 0f);
        
        Debug.Log(Physics2D.gravity.y.ToString() + " * " + rigidbody2D.gravityScale.ToString() + " = " + (Physics2D.gravity.y * rigidbody2D.gravityScale).ToString());
        Debug.Log(_jumpSpeed);
        
        
        //adiciona a velocidade necessaria para atingir a altura desejada. Sem negativar.
        rigidbody2D.AddForce(_jumpSpeed * Vector2.up, ForceMode2D.Impulse);
    }
}

/*
 * For those interested here is what AddForce() will do
 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
 */