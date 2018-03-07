using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician_N : MonoBehaviour
{
    public GameObject Wraith;
    public GameObject Skeleton;
    public GameObject GreatSpirit;
    private List<GameObject> spaces;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Summon()
    {
        DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        DDOL.instance.ShowSpaces();
    }
    public void SummonSkeleton()
    {
        DDOL.instance.option = "summon";
        Debug.Log("Start");
        DDOL.instance.summon = Skeleton;
        Summon();

    }
    public void SummonWraith()
    {
        DDOL.instance.option = "summon";
        Debug.Log("Start");
        DDOL.instance.summon = Wraith;
        Summon();
    }
    public void SummonGreatSpirit()
    {
        DDOL.instance.option = "summon";
        Debug.Log("Start");
        DDOL.instance.summon = GreatSpirit;
        Summon();
    }
}
