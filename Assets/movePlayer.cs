using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed;
    public float rotSpeed;
    public Animator anim;
    Vector3 moveAxis, turnAxis;

    // Update is called once per frame
    void FixedUpdate()
    {
        moveAxis = new Vector3(0, 0, Input.GetAxis("Vertical"));
        turnAxis = new Vector3(0, Input.GetAxis("Horizontal"), 0);

        controller.SimpleMove(transform.TransformVector(moveAxis) * moveSpeed);
        anim.SetFloat("Speed", controller.velocity.magnitude);
        transform.Rotate(turnAxis * rotSpeed);
    }
}
