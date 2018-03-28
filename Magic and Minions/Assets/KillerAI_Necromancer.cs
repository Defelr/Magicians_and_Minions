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

    //TODO: look more into how spells work
    //      may want to consider moving AI around to cast spells
    //      potentially; for each location it can move into, save as temp
    //      get the occupied tiles around it
    //      if rather full, maybe step in to cast a spell
    //      if not, perhaps don't move

    public void PlayTurn()
    {
        print("Let Toriel say the f-word");
        DDOL.instance.currentObject = DDOL.instance.IC2;
        ai = DDOL.instance.IC2;
        //If less than 7 minions, summon minion, preference for wraiths
        //TODO: probably put in some consideration about summoning skeletons for mana
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

    //Summons skeleton
    public bool SummonSkeleton()
    {
        Debug.Log("Summon skeleton attempt");
        //If we have enough mana...
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >=
            Skeleton.GetComponent<MouseDetect>().Cost) {
            Debug.Log("Summon skeleton success");
            //Set DDOL flags to summon skeleton
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Skeleton;
            //Generate list of possible spaces to summon skeleton in
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                //Summon randomly to one of those locations
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            } else
            {
                //Return false if no place to summon minion to
                return false;
            }
            //Add instance to minions list for later referencing
            minions.Add(DDOL.instance.ICS);
            return true;
        } else
        {
            //Return false if not enough mana
            Debug.Log("Summon skeleton failure");
            return false;
        }
    }

    //Summon wraith
    public bool SummonWraith()
    {
        Debug.Log("Summon wraith attempt");
        //If we have enough mana...
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana>=
            Wraith.GetComponent<MouseDetect>().Cost) {
            Debug.Log("Summon wraith success");
            //Set DDOL flags to summon wraith
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Wraith;
            //Generate list of possible spaces to summon wraith in
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                //Summon randomly to one of these locations
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
            } else
            {
                //Return flase if no place to summon minion to
                return false;
            }
            //Add instance to minions list for later referencing
            minions.Add(DDOL.instance.ICS);
            return true;
        } else
        {
            //Return flase if not enough mana
            Debug.Log("Summon wraith failure");
            return false;
        }
    }

    //TODO: weight so that it's not always same minion being moved
    public GameObject MinionToMove()
    {
        //Get player
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("player: " + p.transform.position);
        //Find closest minion by comparing each minion distance
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
        //Return closest minion
        return minion;
    }

    //TODO: move minion towards player, not randomly
    public void MoveMinion(GameObject m)
    {
        //Set DDOL flags to move minion
        Debug.Log("before: " + m.transform.position);
        DDOL.instance.option = "move";
        //Get locations minion in question can move to
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, m.GetInstanceID());
        if (loc.Count != 0)
        {
            //For now, moves randomly WILL BE CHANGED
            print("first test");
            DDOL.instance.MoveCharacter(loc[Random.Range(0, loc.Count - 1)].transform);
            print("test");
        }
        Debug.Log("after: " + m.transform.position);
    }

    //TODO: look through all minions to see if any have something to attack
    //      if multiple, attack with one closest to ai's piece
    public void MinionAttack(GameObject m)
    {
        //Set DDOL flags to attack
        DDOL.instance.option = "attack";
        //Get list of things minion can attack
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, m.GetInstanceID());
        if (loc.Count != 0)
        {
            //Pick one of those at random
            GameObject l = loc[Random.Range(0, loc.Count - 1)];
            //Get the actual object at chosen location
            GameObject victim = DDOL.instance.FindCurrentObject(l);
            //attack
            victim.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
        }
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
