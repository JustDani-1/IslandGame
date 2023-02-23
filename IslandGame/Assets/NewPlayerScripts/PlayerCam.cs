using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float senX;
    public float senY;

    public Transform orientation;

    float xRotation;
    float yRotation;

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	private void Update()
	{
		//get mouse input
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * senX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * senY;

		yRotation += mouseX;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		//rotate cam and oprientation
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation, 0);
	}
}
