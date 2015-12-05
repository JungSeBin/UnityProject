using UnityEngine;
using System.Collections;

public class GateMove : MonoBehaviour 
{
    void OnTriggerEnter(Collider collider)
    {
        Transform warp = transform.Find("Warp");

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(80.0f, 3.0f, -10.0f);
    }
}
