using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour
{

    float sensitivity = 100.0f;
    float rotationY;

    public Camera playerEye;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (Input.mousePosition.x <= 0 || Input.mousePosition.x >= screenWidth
            || Input.mousePosition.y <= 0 || Input.mousePosition.y >= screenHeight)
            return;

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        float mouseMoveValueX = Input.GetAxis("Mouse X");
        rotationY += mouseMoveValueX * sensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);

        ray = playerEye.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.tag == "Box") //if front object is box
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ItemManager.Instance().GetBox(hit.transform.gameObject);
                }
            }
        }
    }

    void OnMouseDown()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}