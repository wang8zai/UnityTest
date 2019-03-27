using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : ScriptableObject
{
    private GameManager gameManager = null;

    public GameObject MainHeroGameObject;
    private List<GameObject> PlayerList = new List<GameObject>();
    private List<GameObject> NpcList = new List<GameObject>();
    
    private string characterPrefix = "Prefab/MainHeroSprite";

    private int playerCnt = 2;

	private string CharacterBaseName = "Character";
	private GameObject CharacterBaseObj = null;

	private Vector3 Origin = new Vector3(0, 5, 0);
	private Quaternion OriginRotation = Quaternion.identity;

    protected int gravityScale = 5;

    protected int[] bodypid = {50, 3, 0};
    protected int[] legpid = {35, 3, 0};

    public void Start()
    {
    }

    public GameObject Get(int index)
    {
        return PlayerList[index];
    }

    public void Init(GameManager gm)
    {
        gameManager = gm;
        CharacterBaseObj = new GameObject(CharacterBaseName);
        for(int i = 0; i < playerCnt; i++)
        {
            GameObject characterObj = ResourceLoader.LoadPrefab(characterPrefix, Origin, OriginRotation, CharacterBaseObj, true);
            PlayerList.Add(characterObj);
            characterObj.GetComponent<BaseCharacter>().SetGameManager(gm);
            characterObj.GetComponent<BaseCharacter>().SetCharacterAsPlayer(0);
        }

    }

    // old version init with PID and mass.
    // public void Init(int cnt)
    // {
    //     for(int i=0; i < cnt; i++)
    //     {
    //         MainHeroGameObject = Resources.Load("Prefab/MainHeroSprite") as GameObject;
    //         PlayerList.Add(Instantiate<GameObject>(MainHeroGameObject));
    //         int[] weightArray = {10,30,10,15};
    //         PlayerList[i].GetComponent<MainHero>().SetBodyPartsMass(weightArray);
    //         PlayerList[i].GetComponent<MainHero>().SetBodyGravityScale(gravityScale);
    //         PlayerList[i].GetComponent<MainHero>().LeftLeg.GetComponent<LeftLeg>().SetGObj(PlayerList[i].GetComponent<MainHero>().LeftLeg);
    //         PlayerList[i].GetComponent<MainHero>().RightLeg.GetComponent<RightLeg>().SetGObj(PlayerList[i].GetComponent<MainHero>().RightLeg);
    //         // PlayerList[i].GetComponent<MainHero>().LeftLeg.GetComponent<LeftLeg>().SetRd(PlayerList[i].GetComponent<MainHero>().LeftLeg.GetComponent<Rigidbody2D>());
    //         // PlayerList[i].GetComponent<MainHero>().RightLeg.GetComponent<RightLeg>().SetRd(PlayerList[i].GetComponent<MainHero>().RightLeg.GetComponent<Rigidbody2D>());
    //         PlayerList[i].GetComponent<Body>().SetPID(bodypid);
    //         PlayerList[i].GetComponent<MainHero>().LeftLeg.GetComponent<LeftLeg>().SetPID(legpid);
    //         PlayerList[i].GetComponent<MainHero>().RightLeg.GetComponent<RightLeg>().SetPID(legpid);
                        
    //         PlayerList[i].SetActive(true);
    //     }
    // }
}