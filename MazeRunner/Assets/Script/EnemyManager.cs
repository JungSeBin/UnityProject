using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;

	void Start ()
    {
        GameObject enemyObj = Instantiate(enemy) as GameObject;
        float x = Random.Range(-20.0f, 20.0f);
        enemyObj.transform.position = new Vector3(x, 0.1f, 20.0f);
	}
}
