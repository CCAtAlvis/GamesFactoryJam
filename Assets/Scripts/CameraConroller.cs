using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPositionOffset;

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + cameraPositionOffset;
        }
    }
}
