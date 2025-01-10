using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera targetCamera; // Reference to the camera

    void Start()
    {
        // If no camera is assigned, use the main camera by default
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        // Make the canvas face the camera
        if (targetCamera != null)
        {
            transform.LookAt(transform.position + targetCamera.transform.forward);
        }
    }
}
