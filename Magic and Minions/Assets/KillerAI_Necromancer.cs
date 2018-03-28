using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerAI_Necromancer : MonoBehaviour {
    public GameObject Skeleton;
    public GameObject Wraith;
    public IList<GameObject> minions = new List<GameObject>();
    private GameObject ai;

    // Use this for initialization
    void Start () {
        //StartCoroutine(PlayTurn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   // public void PlayTurnButton()
    //{
    //    StartCoroutine("PlayTurn");
    //}

    public void PlayTurn()
    {
        print("Let Toriel say the f-word");
        DDOL.instance.currentObject = DDOL.instance.IC2;
        ai = DDOL.instance.IC2;
        //If less than 7 minions, summon minion, preference for wraiths
        if (minions.Count < 7)
       {
            Debug.Log("Summon minion");
            if (!SummonWraith())
            {
                SummonSkeleton();
            }
        }
        //wait between summoning and moving
        //yield return new WaitForSeconds(2f);
        //move minion
        GameObject m = MinionToMove();
        MoveMinion(m);
        //wait between moving and attacking
        //if there is anything within attack radius, attack
        //MinionAttack(m);
        //wait between attacking and casting spells
        if (!UnLifeBlast())
        {
            if (!Swarm())
            {
                LifeDrain();
            }
        }
        DDOL.instance.End_Turn();
    }

    public bool SummonSkeleton()
    {
        Debug.Log("Summon skeleton attempt");
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >=
            Skeleton.GetComponent<MouseDetect>().Cost) {
            Debug.Log("Summon skeleton success");
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Skeleton;
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            }
            minions.Add(DDOL.instance.ICS);
            return true;
        } else
        {
            Debug.Log("Summon skeleton failure");
            return false;
        }
    }
    public bool SummonWraith()
    {
        Debug.Log("Summon wraith attempt");
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana>=
            Wraith.GetComponent<MouseDetect>().Cost) {
            Debug.Log("Summon wraith success");
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Wraith;
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            }
            minions.Add(DDOL.instance.ICS);
            return true;
        } else
        {
            Debug.Log("Summon wraith failure");
            return false;
        }
    }

    //TODO: weight so that it's not always same minion being moved
    public GameObject MinionToMove()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("player: " + p.transform.position);
        GameObject minion = minions[0];
        float min = Vector3.Distance(p.transform.position, minions[0].transform.position);
        foreach(GameObject m in minions)
        {
            if (Vector3.Distance(p.transform.position, m.transform.position) < min)
            {
                min = Vector3.Distance(p.transform.position, m.transform.position);
                minion = m;
            }
        }
        return minion;
    }

    //TODO: move minion towards player, not randomly
    public void MoveMinion(GameObject m)
    {
        Debug.Log("before: " + m.transform.position);
        DDOL.instance.option = "move";
        DDOL.instance.currentObject = m;
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (loc.Count != 0)
        {
            print("first test");
            DDOL.instance.MoveCharacter(loc[Random.Range(0, loc.Count - 1)].transform);
            print("test");
        }
        DDOL.instance.currentObject = ai;
        Debug.Log("after: " + m.transform.position);
    }

    //TODO: actually implement
    //TODO: look through all minions to see if any have something to attack
    //      if multiple, attack with one closest to ai's piece
    public void MinionAttack(GameObject m)
    {
        DDOL.instance.currentObject = m;
        DDOL.instance.option = "attack";
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (loc.Count != 0)
        {
            //DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
        }
        DDOL.instance.currentObject = ai;
    }

    public bool UnLifeBlast()
    {
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 2)
        {
            DDOL.instance.option = "attack";
            DDOL.instance.spell = "Unlife";
            DDOL.instance.currentCost = 2;
            List<GameObject> loc = DDOL.instance.SpaceLocation(3, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                //DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            }
            return true;
        } else
        {
            return false;
        }
    }

    public bool Swarm()
    {
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 2)
        {
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Skeleton;
            DDOL.instance.spell = "Swarm";
            DDOL.instance.currentCost = 2;
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                //DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool LifeDrain()
    {
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 4)
        {
            DDOL.instance.option = "all";
            DDOL.instance.summon = Skeleton;
            DDOL.instance.spell = "LifeDrain";
            DDOL.instance.currentCost = 4;
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                //DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
