using UnityEngine;
using System.Collections;

public class MazeMove : MonoBehaviour 
{
    TimeManager time = null;
    public GameObject northDoor, eastDoor, southDoor, westDoor;

    enum DoorState
    {
        NORTH_OPEN,
        EAST_OPEN,
        SOUTH_OPEN,
        WEST_OPEN,
    }

    DoorState curState = DoorState.WEST_OPEN;

    void Start()
    {
        time = TimeManager.Instance();
        StartCoroutine(OpenDoor(30.0f));
    }

    void Update()
    {
        if (time == null)
            time = TimeManager.Instance();
    }

    IEnumerator OpenDoor(float waitTime)
    {
        if (time == null)
            yield return null;

        while(true)
        {
            switch(curState)
            {
                case DoorState.WEST_OPEN:
                    iTween.MoveBy(westDoor, iTween.Hash("z", -60.0f, "time", 10.0f, "easeType", iTween.EaseType.linear));
                    iTween.MoveBy(northDoor, iTween.Hash("z", 100.0f, "time", 5.0f, "easeType", iTween.EaseType.linear));
                    curState = DoorState.NORTH_OPEN;
                    break;

                case DoorState.NORTH_OPEN:
                    iTween.MoveBy(northDoor, iTween.Hash("z", -100.0f, "time", 10.0f, "easeType", iTween.EaseType.linear));
                    iTween.MoveBy(eastDoor, iTween.Hash("z", -50.0f, "time", 5.0f, "easeType", iTween.EaseType.linear));
                    curState = DoorState.EAST_OPEN;
                    break;

                case DoorState.EAST_OPEN:
                    iTween.MoveBy(eastDoor, iTween.Hash("z", 50.0f, "time", 10.0f, "easeType", iTween.EaseType.linear));
                    iTween.MoveBy(southDoor, iTween.Hash("z", -50.0f, "time", 5.0f, "easeType", iTween.EaseType.linear));
                    curState = DoorState.SOUTH_OPEN;
                    break;

                case DoorState.SOUTH_OPEN:
                    iTween.MoveBy(southDoor, iTween.Hash("z", 50.0f, "time", 10.0f, "easeType", iTween.EaseType.linear));
                    iTween.MoveBy(westDoor, iTween.Hash("z", 60.0f, "time", 5.0f, "easeType", iTween.EaseType.linear));
                    curState = DoorState.WEST_OPEN;
                    break;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
