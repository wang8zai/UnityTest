using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : ScriptableObject
{
    private GameManager gameManager = null;

	private string ItemBaseName = "Item";
	private GameObject ItemBaseObj = null;

    private List<GameObject> ItemList = new List<GameObject>();

    private string itemPrefix = "Prefab/Item/Sedan";

	private Vector3 Origin = new Vector3(0, 5, 0);
	private Quaternion OriginRotation = Quaternion.identity;

    public void Awake() {
    }

    public void Init()
    {
    }

    public GameObject Get(int index)
    {
        return ItemList[index];
    }

    public bool isEmpty()
    {
        return ItemList.Count == 0;
    }

    public void Init(GameManager gm)
    {
        gameManager = gm;
        ItemBaseObj = new GameObject(ItemBaseName);

        GameObject itemObj = ResourceLoader.LoadPrefab(itemPrefix, Origin, OriginRotation, ItemBaseObj, true);
        itemObj.GetComponent<Sedan>().SetGameManager(gm);
        ItemList.Add(itemObj);
    }
}