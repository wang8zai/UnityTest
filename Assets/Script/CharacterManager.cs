using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : ScriptableObject
{
    #region Singleton
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<CharacterManager>();
            }
            return _instance;
        }
    }
    #endregion

    public GameObject MainHeroGameObject;
    private List<GameObject> PlayerList = new List<GameObject>();
    private List<GameObject> NpcList = new List<GameObject>();
    
    private string characterPrefix = "Prefab/MainHeroSprite";

	private string CharacterBaseName = "Character";
	private GameObject CharacterBaseObj = null;

	private Vector3 Origin = new Vector3(0, 5, 0);
	private Quaternion OriginRotation = Quaternion.identity;

    protected int gravityScale = 5;

    protected int[] bodypid = {50, 3, 0};
    protected int[] legpid = {35, 3, 0};

    private int playerCnt = 2;
    private int npcCnt = 1;

    public GameObject Get(int index)
    {
        return PlayerList[index];
    }

    public void Init()
    {
        CharacterBaseObj = new GameObject(CharacterBaseName);
        for (int i = 0; i < playerCnt; i++)
        {
            GameObject characterObj = ResourceLoader.LoadPrefab(characterPrefix, Origin, OriginRotation, CharacterBaseObj, true);
            PlayerList.Add(characterObj);
            characterObj.GetComponent<BaseCharacter>().SetCharacterAsPlayer(0);
        }

        for (int i = 0; i < npcCnt; i++) {
            GameObject characterObj = ResourceLoader.LoadPrefab(characterPrefix, Origin, OriginRotation, CharacterBaseObj, true);
            Destroy(characterObj.GetComponent<BaseCharacter>());
            characterObj.AddComponent<Npc>();
            PlayerList.Add(characterObj);
            characterObj.GetComponent<Npc>().SetCharacterAsNpc();            
        }
    }
}