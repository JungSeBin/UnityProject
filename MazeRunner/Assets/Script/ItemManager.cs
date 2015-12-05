using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemManager : MonoBehaviour {

    public Camera skyCamera;
    GameObject maze;
    Vector3 localPosition;

    public GameObject[] itemBox;
    List<ItemList> item = new List<ItemList>();
    List<ItemList> itemList = new List<ItemList>();
    List<int> overlapNum = new List<int>();

    public Texture TNTtexture;
    public GameObject keyIcon;

    enum ItemList
    {
        ITEM_NONE,
        ITEM_MAP,
        ITEM_KEY,
        ITEM_BOMB,
        ITEM_PENDANT,
        ITEM_FAIL,
    }
    static ItemManager _instance;
    public static ItemManager Instance()
    {
        return _instance;
    }

    void Start()
    {
        if (_instance == null)
            _instance = this;

        localPosition = skyCamera.transform.position;

        itemList.Add(ItemList.ITEM_MAP);
        itemList.Add(ItemList.ITEM_KEY);
        itemList.Add(ItemList.ITEM_BOMB);
        itemList.Add(ItemList.ITEM_BOMB);
        itemList.Add(ItemList.ITEM_PENDANT);
        itemList.Add(ItemList.ITEM_FAIL);
        itemList.Add(ItemList.ITEM_FAIL);
        itemList.Add(ItemList.ITEM_FAIL);

        int randNum;

        for (int i = 0; i < 8; ++i)
        {
            do
            {
                randNum = UnityEngine.Random.Range(0, 8);
            } while (isOverlap(randNum));

            item.Add(itemList[randNum]);
            overlapNum.Add(randNum);
        }

        for(int i=0;i<8;++i)
        {
            if(item[i] == ItemList.ITEM_BOMB)
            {
                itemBox[i].GetComponent<Renderer>().material.mainTexture = TNTtexture;
            }
        }
    }

    bool isOverlap(int num)
    {
        foreach(int overlap in overlapNum)
        {
            if (overlap == num)
                return true;
        }
        return false;
    }

    public void GetBox(GameObject box)
    {
        int i = 0;
        for (; i < 8; ++i) 
        {
            if (box == itemBox[i])
                break;
        }
        
        switch(item[i])
        {
            case ItemList.ITEM_MAP:
                OpenMap();
                break;

            case ItemList.ITEM_KEY:
                GetKey();
                break;

            case ItemList.ITEM_BOMB:
                StartCoroutine(DestroyWall());
                break;

            case ItemList.ITEM_PENDANT:
                GetPendant();
                break;

            default:
                break;

        }
        itemBox[i] = null;
        Destroy(box);
    }

    void OpenMap()
    {
        GameObject[] fogs = GameObject.FindGameObjectsWithTag("Fog");

        foreach(GameObject fog in fogs)
        {
            Destroy(fog);
        }
        NGUIManager.Instance().ChangeLabel("You got a all map!! Press 'M' ");
    }

    void GetKey()
    {
        PlayerState.Instance().GetKey();
        NGUIManager.Instance().ChangeLabel("You got a Key!! You can enter the Cube");
    }

    IEnumerator DestroyWall()
    {
        NGUIManager.Instance().ChangeLabel("You got a dynamite!! Blast the Maze!!");

        int randNum;
        GameObject wall;
        for (int i = 0; i < 400; ++i) 
        {
            randNum = UnityEngine.Random.Range(0,1200);
            wall = transform.parent.GetChild(0).GetChild(randNum).gameObject;
            if (wall != null)
                iTween.MoveBy(wall, iTween.Hash("y", -250.0f,
                    "time", UnityEngine.Random.Range(4.0f, 8.0f),
                    "easeType", iTween.EaseType.linear));

            skyCamera.depth = 10;
            StartCoroutine(CameraShake(3.0f, 5.0f));
        }
        yield return new WaitForSeconds(8.0f);
        skyCamera.depth = -10;
    }

    void GetPendant()
    {
        PlayerState.Instance().GetPendant();

        int i = 0;
        for (; i < 8; ++i)
        {
            if (item[i] == ItemList.ITEM_KEY)
                break;
        }

        GameObject keyFlag = Instantiate(keyIcon) as GameObject;
        keyFlag.transform.position = new Vector3(itemBox[i].transform.position.x, 1000.0f, itemBox[i].transform.position.z);

        NGUIManager.Instance().ChangeLabel("You got a Pandent!! Press 'M', you can find the key ");        
    }

    IEnumerator CameraShake(float shakeTime, float shakeSense)
    {
        yield return null;
        float deltaTime = 0.0f;

        while (deltaTime < shakeTime)
        {
            deltaTime += Time.deltaTime;

            skyCamera.transform.localPosition = localPosition;
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-shakeSense, shakeSense);
            pos.y = Random.Range(-shakeSense, shakeSense);
            pos.z = Random.Range(-shakeSense, shakeSense);
            skyCamera.transform.localPosition += pos * (shakeTime - deltaTime);

            yield return new WaitForEndOfFrame();
        }
        skyCamera.transform.localPosition = localPosition;
    }
}
