using UnityEngine;
using System.Collections;

public class CameraCollision : MonoBehaviour
{
    bool isCollision = false;
    RaycastHit hit;

    float posX, posY, posZ;

    void Awake()
    {
        posX = transform.localPosition.x;
        posY = transform.localPosition.y;
        posZ = transform.localPosition.z;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.tag == "Wall") //앞에 가리키는게 벽이면
            {
                posZ += 0.25f;
            }
            else if (!Physics.Raycast(transform.position, -transform.forward, out hit, 20))
            {
                if(!isCollision)
                {
                    posZ -= 0.25f;                }
            }
        }
        posZ = Mathf.Clamp(posZ, -3.5f, -0.5f);
        transform.localPosition = new Vector3(posX, posY, posZ);
    }

    void OnTriggerEnter(Collider collider)
    {
        isCollision = true;
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.tag == "Player")
        {
            posZ -= 0.25f;
        }
        else
        {
            posZ += 0.25f;
        }
        posZ = Mathf.Clamp(posZ, -3.5f, -0.5f);
        transform.localPosition = new Vector3(posX, posY, posZ);
    }

    void OnTriggerExit(Collider collider)
    {
        isCollision = false;
    }
}
