using UnityEngine;
using System.Collections;

public class FogManager : MonoBehaviour
{
    public GameObject fog;

    int fogNum = 0;

    void Start()
    {
        for(int row = 0; row < 250; ++row)
        {
            for (int col = 0; col < 250; ++col)
            {
                GameObject enemyObj = Instantiate(fog) as GameObject;
                enemyObj.transform.position = new Vector3(40.0f * col - 5000.0f, 300.0f, 40.0f * row - 5000.0f);
            }
        }
    }
}
