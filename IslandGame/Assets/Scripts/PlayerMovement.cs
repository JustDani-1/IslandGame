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
    public Transform bottomEdge;
    public LayerMask layerMask;

    private bool shouldJump = false;
    private bool jumpedLastFrame = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
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
        
        Vector3 vel = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKey(KeyCode.W)) vel += speed * dirFWD;
        if (Input.GetKey(KeyCode.S)) vel += speed * -dirFWD;
        if (Input.GetKey(KeyCode.A)) vel += speed * dirLEFT;
        if (Input.GetKey(KeyCode.D)) vel += speed * -dirLEFT;

        if (isGrounded()) //if grounded, modify the vel vector so that its tangent to the surface
        {
            SnapToGround();
            vel = AlignVelocityVector(vel);
        }

        rb.velocity = vel;

        if (shouldJump && !jumpedLastFrame)
        {
            rb.AddForce(new Vector3(0, 400f, 0));
            shouldJump = false;
            Debug.Log("jump!");
            jumpedLastFrame = true;
        }
        else 
        {
            jumpedLastFrame = false;
        }

    }
    private void SnapToGround() 
    {
        (Vector3 pos, Vector3 n) = GetGroundInfo();
        float deltaz = transform.position.z - pos.z;
        Vector3 selfPos = transform.position;
        selfPos.z -= deltaz;
        transform.position = selfPos;
    }
    private Vector3 AlignVelocityVector(Vector3 vel) 
    {
        (Vector3 hit, Vector3 n) = GetGroundInfo();
        if (!n.normalized.Equals(new Vector3(0, 1, 0)))
        {
            float d = PlaneCalc.DotProduct(hit, n);
            PlaneCalc plane = new PlaneCalc(n, d);
            Vector3 desiredPos = rb.position + vel;
            float y = plane.SolveForY(desiredPos.x, desiredPos.z);
            desiredPos.y = y;
            vel = desiredPos - this.transform.position;
        }
        return vel;
    }

    private bool isGrounded() 
    {
        return Physics.CheckSphere(groundCheckPos.position, 0.5f, layerMask);
    }
    private (Vector3 hit, Vector3 normal) GetGroundInfo() 
    {
        Vector3 dir = new Vector3(0, -1, 0);
        Vector3 pos = groundCheckPos.position;
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, Mathf.Infinity, layerMask))
        {
            return (hit.point, hit.normal);
        }
        else 
        {
            Debug.LogError("didnt hit ground!");
            return (Vector3.zero, new Vector3(0, 1, 0));
        }
    }
}
