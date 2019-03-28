using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : ScriptableObject
{
    private static ItemManager _instance;

	private string ItemBaseName = "Item";
	private GameObject ItemBaseObj = null;

    private List<GameObject> ItemList = new List<GameObject>();

    private string itemPrefix = "Prefab/Item/Sedan";

	private Vector3 Origin = new Vector3(0, 5, 0);
	private Quaternion OriginRotation = Quaternion.identity;

    public static ItemManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<ItemManager>();
            }
            return _instance;
        }
    }

    public GameObject Get(int index)
    {
        return ItemList[index];
    }

    public bool isEmpty()
    {
        return ItemList.Count == 0;
    }

    public void Init()
    {
        ItemBaseObj = new GameObject(ItemBaseName);

        GameObject itemObj = ResourceLoader.LoadPrefab(itemPrefix, Origin, OriginRotation, ItemBaseObj, true);
        ItemList.Add(itemObj);
    }
}