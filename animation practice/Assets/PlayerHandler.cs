using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
        //Reference to the Animator
        //this will allow us to manipulate this characters 
    public Animator anim;
    public Vector3 moveDir;
    private CharacterController _charC;
    public float speed, jumpspeed = 8, gravity = 20, crouch = 2.5f, walk = 5, run = 10;
    void Start()
    {
        //animations
        anim = GetComponent<Animator>();
        anim.SetFloat("isCrouching", 1);
        _charC = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (_charC.isGrounded)
        {
            anim.SetFloat("isCrouching", 1);
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveDir == Vector3.zero)
            {
                speed = 0;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = run;
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    speed = crouch;
                    anim.SetFloat("isCrouching", 0);
                }
                else
                {
                    speed = walk;
                }
                anim.SetFloat("movespeed", 0);
                anim.SetFloat("horizontal", moveDir.x);
                anim.SetFloat("vertical", moveDir.z);
                moveDir = transform.TransformDirection(moveDir);
                moveDir *= speed;
            }
            moveDir.y -= gravity * Time.deltaTime;
            _charC.Move(moveDir * Time.deltaTime);
        }
    }
}
