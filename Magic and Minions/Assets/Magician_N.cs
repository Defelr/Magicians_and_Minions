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
    private bool CheckSummon(int v)
    {
        int C_Mana = DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana;
        if (C_Mana >= 0 && C_Mana >= v)
        {
            return true;
        }
        return false;
    }
    public void Range(int range)
    {
        DDOL.instance.SpaceLocation(range, DDOL.instance.currentObject.GetInstanceID());
        DDOL.instance.ShowSpaces();
    }

    public void ManaMechanic()//CURRENTLY HAS PLACEHOLDER VALUES
    {
        int mana = 0;
        if(tag == "Necro")
        {
            mana = 5;
        }else if(tag == "Paladin")
        {
            mana = 1;
        }
        this.GetComponent<MouseDetect>().IncrementMana(mana);

    }
    //MINIONS SECTION
    public void SummonSkeleton()
    {
        if (CheckSummon(Skeleton.GetComponent<MouseDetect>().Cost))
        {
            DDOL.instance.option = "summon";
            Debug.Log("Start");
            DDOL.instance.summon = Skeleton;
            Range(1);
        }

    }
    public void SummonWraith()
    {
        if (CheckSummon(Wraith.GetComponent<MouseDetect>().Cost))
        {
            DDOL.instance.option = "summon";
            Debug.Log("Start");
            DDOL.instance.summon = Wraith;
            Range(1);
        }
    }
    public void SummonGreatSpirit()
    {
        if (CheckSummon(GreatSpirit.GetComponent<MouseDetect>().Cost))
        {
            DDOL.instance.option = "summon";
            Debug.Log("Start");
            DDOL.instance.summon = GreatSpirit;
            Range(1);
        }
    }
    //SPELLS SECTION
    public void UnLifeBlast()
    {
        if (CheckSummon(2))//Cost of spell
        {
            DDOL.instance.option = "attack";
            DDOL.instance.spell = "Unlife";
            DDOL.instance.currentCost = 2;
            Debug.Log("Unlife Blast");
            Range(3);
        }
    }
    public void Swarm()
    {
        if (CheckSummon(2))
        {
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Skeleton;
            DDOL.instance.spell = "Swarm";
            DDOL.instance.currentCost = 2;
            Debug.Log("Swarm");
            Range(1);
        }
    }
}
