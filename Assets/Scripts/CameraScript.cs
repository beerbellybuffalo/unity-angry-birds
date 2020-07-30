using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{

    public GameObject launcher;

    //Lock cursor automatically at the beginning. In this case we can use either Awake or Start
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        //change the position and rotation of the camera based on mouse movement
        transform.RotateAround(launcher.transform.position, Vector3.up, Input.GetAxis("Mouse X"));
        transform.RotateAround(launcher.transform.position, transform.right, Input.GetAxis("Mouse Y"));

        //rotate the launcher about the Y-axis when the mouse moves
        launcher.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        //Right-Click Lock the mouse cursor while in game so we don't see it, Esc to Unlock
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(1)) //what's the associated number for right click?
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}