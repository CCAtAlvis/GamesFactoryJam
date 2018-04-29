using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPositionOffset;

    void Update()
    {
        transform.position = playerTransform.position + cameraPositionOffset;
    }
}
