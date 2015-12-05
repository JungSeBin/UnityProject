using UnityEngine;
using System.Collections;

public class ViewWorldMap : MonoBehaviour
{

    bool isView = false;

    Vector3 prevMousePos;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<Camera>().depth = -1;
            isView = false;
        }

        if (Input.GetKeyDown(KeyCode.M) && isView)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<Camera>().depth = -1;
            isView = false;
        }
        else if (Input.GetKeyDown(KeyCode.M) && !isView)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<Camera>().depth = 3;
            isView = true;
        }

        if (isView)
        {
            if (Input.GetMouseButtonDown(0))
            {
                prevMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x - (Input.mousePosition.x - prevMousePos.x), -2500.0f, 2500.0f),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z - (Input.mousePosition.y - prevMousePos.y), -2500.0f, 2500.0f));
        }
    }
}
