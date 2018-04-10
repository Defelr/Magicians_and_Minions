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
    private void CheckPrevious()
    {
        DDOL.instance.summon = null;
        DDOL.instance.option = "";
        DDOL.instance.spell = "";
        DDOL.instance.UnShowSpaces();
        DDOL.instance.spaces.Clear();
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
        CheckPrevious();
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
        CheckPrevious();
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
        CheckPrevious();
        if (CheckSummon(GreatSpirit.GetComponent<MouseDetect>().Cost))
        {
            DDOL.instance.option = "summon";
            Debug.Log("Start");
            DDOL.instance.summon = GreatSpirit;
            Range(1);
        }
    }
    //SPELLS SECTION
    public bool UnLifeBlastCheck()
    {
        if (CheckSummon(2))
        {
            return true;
        }
        return false;
    }
    public bool SwarmCheck()
    {
        if (CheckSummon(2))
        {
            return true;
        }
        return false;
    }
    public bool LifeDrainCheck()
    {
        if (CheckSummon(4))
        {
            return true;
        }
        return false;
    }
    public void UnLifeBlast()
    {
        CheckPrevious();
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
        CheckPrevious();
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
    public void LifeDrain()
    {
        CheckPrevious();
        if (CheckSummon(4))
        {
            DDOL.instance.option = "all";
            DDOL.instance.spell = "LifeDrain";
            DDOL.instance.currentCost = 4;
            Debug.Log("Life Drain");
            Range(1); //Changed ranged from 4 to 1
        }
    }
}
