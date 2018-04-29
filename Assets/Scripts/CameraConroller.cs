using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public GameObject gunTrans, swordTrans;
    public Vector3 cameraPositionOffset;
    private Transform playerTransform;

    void Update()
    {
        if (gunTrans.activeSelf)
            playerTransform = gunTrans.GetComponent<Transform>();
        else if (swordTrans.activeSelf)
            playerTransform = swordTrans.GetComponent<Transform>();

        if (playerTransform != null)
        {
            transform.position = playerTransform.position + cameraPositionOffset;
        }
    }
}
