using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;

    public float xSen = 5f;
    public float ySen = 5f;

    public float xMin = -360f;
    public float xMax = 360f;

    public float yMin = -90f;
    public float yMax = 90f;

    float rotationY = 0f;

    public float speed = 0.2f;

    Rigidbody rb;
    public Transform groundCheckPos;
    public LayerMask layerMask;

    private bool shouldJump;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        //camera follow mouse
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * xSen;
        rotationY += Input.GetAxis("Mouse Y") * ySen;
        rotationY = Mathf.Clamp(rotationY, yMin, yMax);

        camera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);

        if (Input.GetKey(KeyCode.Space) && isGrounded()) 
        {
            shouldJump = true;
        }

    }

    private void FixedUpdate()
    {
        //movement
        Vector3 dirFWD = transform.TransformDirection(Vector3.forward);
        Vector3 dirLEFT = transform.TransformDirection(Vector3.left);
        

        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += speed * dirFWD;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += speed * -dirFWD;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += speed * dirLEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += speed * -dirLEFT;
        }
        if(shouldJump)
        {
            rb.AddForce(new Vector3(0, 300f, 0));
            shouldJump = false;
        }
        Debug.Log(isGrounded());
    }
    private bool isGrounded() 
    {
        return Physics.CheckSphere(groundCheckPos.position, 0.5f, layerMask);
    }
}
