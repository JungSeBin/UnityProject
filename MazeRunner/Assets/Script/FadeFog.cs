using UnityEngine;
using System.Collections;

public class FadeFog : MonoBehaviour 
{
    void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }
}