using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector2 lookSpeed;
    public Vector3 moveSpeed;

    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRotation;
 
    void Start () {
        originalRotation = transform.localRotation;
    }

    void Update() {
        // Rotation
        rotationX += Input.GetAxis("Mouse X") * lookSpeed.x;
        rotationY += Input.GetAxis("Mouse Y") * lookSpeed.y;
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        // Translation
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed.x;
        float vertical = Input.GetAxis("Vertical") * moveSpeed.y;
        float upwards = Input.GetAxis("Upwards") * moveSpeed.z; 
        Vector3 movementInput = transform.forward * vertical + transform.right * horizontal + transform.up * upwards;

        transform.position += (movementInput * Time.deltaTime);
    }
}
