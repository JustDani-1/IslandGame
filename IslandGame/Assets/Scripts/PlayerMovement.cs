using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("camera")]
    public Camera pcamera;

    public float xSen = 5f;
    public float ySen = 5f;
    
    public float xMin = -360f;
    public float xMax = 360f;

    public float yMin = -90f;
    public float yMax = 90f;

    public float rotationY {get; private set;}

    [Header("movement")]
    public float speed = 0.2f;
    public float dashSpeed;
    public float glideStrength;
    public float jumpCooldown;
    public float jumpStrength;
    private bool readyToJump = true;
    private bool grounded;

    Rigidbody rb;

    [Header("groundCheck")]
    public Transform groundCheckPos;
    public float playerHeight;
    public LayerMask whatIsGround;

    private float speedMultiplier = 1f;
    private bool isDashing = false;
    private bool isGliding = false;

    
    void Start()
    {
        rotationY = 0;
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xSen = DataManager.Instance.xSen;
        ySen = DataManager.Instance.ySen;
    }

    
    void Update()
    {
        grounded = IsGrounded();

        MyInput();

        //camera follow mouse
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * xSen;
        rotationY += Input.GetAxis("Mouse Y") * ySen;
        rotationY = Mathf.Clamp(rotationY, yMin, yMax);

        pcamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);

    }
    private void MyInput()
    {
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private bool IsGrounded() 
    {
        return Physics.CheckSphere(groundCheckPos.position, 0.5f, whatIsGround);
    }

    private void FixedUpdate()
    {
        //movement
        Vector3 dirFWD = transform.TransformDirection(Vector3.forward);
        Vector3 dirLEFT = transform.TransformDirection(Vector3.left);

        Vector3 vel = new Vector3(0, rb.velocity.y, 0);

        if (isDashing)
        {
            vel = dashSpeed * dirFWD;
            rb.velocity = vel;
            return;
        }
        if (isGliding) 
        {
            vel.y += glideStrength;
        }

        if (Input.GetKey(KeyCode.W)) vel += speed * dirFWD * speedMultiplier;
        if (Input.GetKey(KeyCode.S)) vel += speed * -dirFWD * speedMultiplier;
        if (Input.GetKey(KeyCode.A)) vel += speed * dirLEFT * speedMultiplier;
        if (Input.GetKey(KeyCode.D)) vel += speed * -dirLEFT * speedMultiplier;

        if (grounded) //if grounded, modify the vel vector so that its tangent to the surface
        {
            SnapToGround();
            vel = AlignVelocityVector(vel);
        }

        rb.velocity = vel;

    }
    public void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        
    }
    private void SnapToGround() 
    {
        if (rb.velocity.y < 0) 
        {
            Vector3 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
        }

        
    }
    private Vector3 AlignVelocityVector(Vector3 vel) 
    {
        float m = vel.magnitude;
        (Vector3 hit, Vector3 n) = GetGroundInfo();
        if (n.y <= 0) 
        {
            return vel;
        }
        if (!n.normalized.Equals(new Vector3(0, 1, 0)))
        {
            float d = PlaneCalc.DotProduct(hit, n);
            PlaneCalc plane = new PlaneCalc(n, d);
            Vector3 desiredPos = rb.position + vel;
            float y = plane.SolveForY(desiredPos.x, desiredPos.z);
            desiredPos.y = y;
            vel = desiredPos - this.transform.position;
        }

        
        vel = vel.normalized * m;
        
        return vel;
    }
    private (Vector3 hit, Vector3 normal) GetGroundInfo() 
    {
        Vector3 dir = new Vector3(0, -1, 0);
        Vector3 pos = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, Mathf.Infinity, whatIsGround))
        {
            return (hit.point, hit.normal);
        }
        else 
        {
            
            return (Vector3.zero, new Vector3(0, 1, 0));
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void startSpeedBoost(float f)
    {
        speedMultiplier = f;
    }
    public void defaultSpeed()
    {
        speedMultiplier = 1;
    }
    public void dash() 
    {
        isDashing = true;
    }
    public void stopDash() 
    {
        isDashing = false;
    }
    public void glide()
    {
        isGliding = true;
    }
    public void stopGlide()
    {
        isGliding = false;
    }
}
