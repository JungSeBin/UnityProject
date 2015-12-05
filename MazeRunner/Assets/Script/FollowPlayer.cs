using UnityEngine;
using System.Collections;

public class FollowPlayer: MonoBehaviour 
{
    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
	void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
	}
}
