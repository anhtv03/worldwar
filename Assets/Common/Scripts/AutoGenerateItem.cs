using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoGenerateItem : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;
    [SerializeField] float period;

    float time;
    float cameraWidth;

    int random;
    List<GameObject> enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

        enemyPool = new List<GameObject>();

        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject itemObject = Instantiate(GetRandom());
            itemObject.SetActive(false);
            enemyPool.Add(itemObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= period)
        {
            GameObject newItem = GetFreeItem();
            if (newItem != null)
            {
                newItem.SetActive(true);
                float gOWidthX = newItem.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                newItem.transform.position = new Vector3(Random.Range(-cameraWidth + gOWidthX, cameraWidth - gOWidthX),
                                                         itemList[random].transform.position.y, 
                                                         itemList[random].transform.position.z);

                ItemScript enemyScript = newItem.GetComponent<ItemScript>();
                time = 0;
            }
        }
    }

    private GameObject GetFreeItem()
    {
        foreach (var item in enemyPool)
        {
            if (item.activeSelf == false)
                return item;
        }

        return null;
    }
    private GameObject GetRandom()
    {
        random = Random.Range(0, itemList.Count);
        return itemList[random];
    }
}
