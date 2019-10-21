using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {
    private CharacterController controller;

    public float speed = 10;
    public float jumpSpeed = 10;
    public Vector3 gravity = Vector3.down * 9.8f;
    public Vector3 acceleration = Vector3.zero;
    public CapsuleCollider collider;
    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 move = Vector3.zero;
        if (Math.Abs(hAxis) > 0.1 || Math.Abs(vAxis) > 0.1) {
            move = (hAxis * speed * transform.right + vAxis * speed * transform.forward);
        }

        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Jump");
            acceleration += Vector3.up * jumpSpeed;
        }

        acceleration += gravity * Time.deltaTime;
        //Debug.DrawLine(transform.position, transform.position + Vector3.up * (acceleration.y + Mathf.Sign(acceleration.y) * controller.height/2));
        if (controller.isGrounded && acceleration.y < 0) {
            acceleration.y = 0;
        }

        controller.Move(move + acceleration);
    }
}
