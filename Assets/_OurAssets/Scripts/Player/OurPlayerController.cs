using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurPlayerController : MonoBehaviour
{
    public Camera MyCamera;
    [SerializeField] Transform myModel;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float sprintSpeed = 2;

    public Rigidbody _rigidbody;
    private bool isSprinting;
    private KeyCode sprintKey = KeyCode.LeftShift;

    private void Awake()
    {
        if (_rigidbody)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        CameraInput();

        if (Input.GetKeyDown(sprintKey))
            isSprinting = true;
        if (Input.GetKeyUp(sprintKey))
            isSprinting = false;

    }

    private void Movement()
    {
        Vector3 playerVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (!isSprinting)
        {
            playerVelocity = transform.TransformDirection(playerVelocity) * movementSpeed;
            Vector3 velocity = _rigidbody.velocity;
            Vector3 velocityChange = (playerVelocity - velocity);
            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            playerVelocity = transform.TransformDirection(playerVelocity) * sprintSpeed;
            Vector3 velocity = _rigidbody.velocity;
            Vector3 velocityChange = (playerVelocity - velocity);
            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    private void CameraInput()
    {
        RaycastHit hit;
        Ray camRay = MyCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out hit))
        {
            myModel.transform.LookAt(new Vector3(hit.point.x, myModel.transform.position.y, hit.point.z));
            Debug.DrawLine(camRay.origin, hit.point, Color.yellow);
        }
    }
}