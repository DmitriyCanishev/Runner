using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] float moveSpeed;
    [SerializeField] private GameObject losePanel;

    private int lineToMove = 1;
    public float lineDistance = 4;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        transform.position = targetPosition;

       
    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    void FixedUpdate()
    {
        dir.z = speed;
        speed = moveSpeed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    public void SetSpeed(float speed)
    {
       moveSpeed = speed;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

}