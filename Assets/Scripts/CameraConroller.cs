using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPositionOffset;

    private Vector3 cameraRotation = new Vector3(90.0f, 0f);
    // private bool playerRunning = false;
    // private Vector3 pivotPoint;

    // Update is called once per frame
    void Update ()
    {
        // to move camera with player, so that camera is always above player
        transform.position = playerTransform.position + cameraPositionOffset;

        // TODO: to rotate camera in direction which player is seeing, so that player always move forward 
        // cameraRotation.y = playerTransform.rotation.eulerAngles.y;
        // Debug.Log(transform.rotation.eulerAngles);
        // transform.Rotate(cameraRotation);

        // TODO: give camera bounce effect, when player stops running
        // make camera rotate in a circle around the player, player as pivot
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerRunning = true;
            cameraTransform = transform.position;
            pivotPoint = playerTransform.position;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerRunning = false;
            transform.RotateAround(pivotPoint, playerTransform.right, 100 * Time.deltaTime);
        }*/
    }
}
