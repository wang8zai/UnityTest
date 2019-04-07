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

    private int npcCnt = 1;

    public GameObject Get(int index)
    {
        return PlayerList[index];
    }

    public List<GameObject> GetNpcList() {
        return NpcList;
    }

    public void Init()
    {
        CharacterBaseObj = new GameObject(CharacterBaseName);

        GameObject P1Obj = ResourceLoader.LoadPrefab(characterPrefix, Origin - new Vector3(2,0,0), OriginRotation, CharacterBaseObj, true);
        PlayerList.Add(P1Obj);
        P1Obj.GetComponent<BaseCharacter>().SetCharacterAsPlayer(0);

        GameObject P2Obj = ResourceLoader.LoadPrefab(characterPrefix, Origin - new Vector3(-2,0,0), OriginRotation, CharacterBaseObj, true);
        PlayerList.Add(P2Obj);
        P2Obj.GetComponent<BaseCharacter>().SetCharacterAsPlayer(0);

        for (int i = 0; i < npcCnt; i++) {
            GameObject characterObj = ResourceLoader.LoadPrefab(characterPrefix, Origin - new Vector3(0,0,3), OriginRotation, CharacterBaseObj, true);
            Destroy(characterObj.GetComponent<BaseCharacter>());
            characterObj.AddComponent<Npc>();
            NpcList.Add(characterObj);
            characterObj.GetComponent<Npc>().SetCharacterAsNpc();            
        }
    }
}