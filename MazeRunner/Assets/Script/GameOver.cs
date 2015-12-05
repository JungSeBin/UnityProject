using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Destroy(collider.transform.gameObject);
        }
    }
}
