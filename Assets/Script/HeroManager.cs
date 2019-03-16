using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public GameObject MainHeroGameObject;
    private List<GameObject> BodyList = new List<GameObject>();

    private static HeroManager instance = null;

    public static HeroManager Instance
    {
        get
        {
            return instance;
        }
    }

    protected int gravityScale = 5;

    protected int[] bodypid = {50, 3, 0};
    protected int[] legpid = {35, 3, 0};

    public void Start()
    {
    }

    public GameObject Get(int index)
    {
        return BodyList[index];
    }

    public void InitHero(int cnt)
    {
        for(int i=0; i < cnt; i++)
        {
            MainHeroGameObject = Resources.Load("Prefab/MainHero") as GameObject;
            BodyList.Add(Instantiate<GameObject>(MainHeroGameObject));
            int[] weightArray = {10,30,10,15};
            BodyList[i].GetComponent<MainHero>().SetBodyPartsMass(weightArray);
            BodyList[i].GetComponent<MainHero>().SetBodyGravityScale(gravityScale);
            BodyList[i].GetComponent<MainHero>().LeftLeg.GetComponent<LeftLeg>().SetGObj(BodyList[i].GetComponent<MainHero>().LeftLeg);
            BodyList[i].GetComponent<MainHero>().RightLeg.GetComponent<RightLeg>().SetGObj(BodyList[i].GetComponent<MainHero>().RightLeg);
            // BodyList[i].GetComponent<MainHero>().LeftLeg.GetComponent<LeftLeg>().SetRd(BodyList[i].GetComponent<MainHero>().LeftLeg.GetComponent<Rigidbody2D>());
            // BodyList[i].GetComponent<MainHero>().RightLeg.GetComponent<RightLeg>().SetRd(BodyList[i].GetComponent<MainHero>().RightLeg.GetComponent<Rigidbody2D>());
            BodyList[i].GetComponent<Body>().SetPID(bodypid);
            BodyList[i].GetComponent<MainHero>().LeftLeg.GetComponent<LeftLeg>().SetPID(legpid);
            BodyList[i].GetComponent<MainHero>().RightLeg.GetComponent<RightLeg>().SetPID(legpid);
                        
            BodyList[i].SetActive(true);
        }
    }
}