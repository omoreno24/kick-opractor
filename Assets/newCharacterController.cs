using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCharacterController : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed;
    public float Gravity = 7.9f;
    public float jumpSpeed = 5;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal") * moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection.y = 0;

            //moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;

        }


        moveDirection.y -= Gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
