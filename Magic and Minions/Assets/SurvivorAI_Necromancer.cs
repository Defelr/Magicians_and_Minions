using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerAI_Necromancer : MonoBehaviour
{
    public GameObject Skeleton;
    public GameObject Wraith;
    public IList<GameObject> minions = new List<GameObject>();
    public IList<GameObject> justSummoned = new List<GameObject>();
    private GameObject ai;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(PlayTurn());
    }

    // Update is called once per frame
    void Update()
    {

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
        DDOL.instance.currentObject = DDOL.instance.IC2;
        ai = DDOL.instance.IC2;
        foreach (GameObject m in justSummoned)
        {
            minions.Add(m);
        }
        justSummoned = new List<GameObject>();
        //If less than 5 minions, summon minion, preference for wraiths
        if (minions.Count < 5)
        {
            Debug.Log("Summon minion");
            if (Random.Range(0, 1) < 0.7)
            {
                SummonWraith();
            }
            else
            {
                SummonSkeleton();
            }
        }
        //If at least 10 mana, summon 2nd minion, regardless
        //add a short wait here, too
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 10)
        {
            Debug.Log("Summon 2nd minion");
            if (Random.Range(0, 1) < 0.7)
            {
                SummonWraith();
            }
            else
            {
                SummonSkeleton();
            }
        }
        //wait between summoning and moving
        //yield return new WaitForSeconds(2f);
        //move minion
        foreach (GameObject m in minions)
        {
            DDOL.instance.currentObject = m;
            MoveMinion(m);
            DDOL.instance.currentObject = ai;
        }
        //wait between moving and attacking
        //if there is anything within attack radius, attack
        foreach (GameObject m in minions)
        {
            DDOL.instance.currentObject = m;
            MinionAttack(m);
            DDOL.instance.currentObject = ai;
        }
        //wait between attacking and casting spells
        if (!UnLifeBlast())
        {
            if (!Swarm())
            {
                LifeDrain();
            }
        }
        //wait between spells and moving self
        MoveSelf();
        //short wait here
        DDOL.instance.End_Turn();
    }

    //Summons skeleton to random location
    public bool SummonSkeleton()
    {
        Debug.Log("Summon skeleton attempt");
        //If we have enough mana...
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >=
            Skeleton.GetComponent<MouseDetect>().Cost)
        {
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
            }
            else
            {
                //Return false if no place to summon minion to
                return false;
            }
            //Add instance to minions list for later referencing
            justSummoned.Add(DDOL.instance.ICS);
            DDOL.instance.currentObject.gameObject.GetComponent<MouseDetect>().DiminishMana(Skeleton.GetComponent<MouseDetect>().Cost);
            return true;
        }
        else
        {
            //Return false if not enough mana
            Debug.Log("Summon skeleton failure");
            return false;
        }
    }

    //Summon wraith to random location
    public bool SummonWraith()
    {
        Debug.Log("Summon wraith attempt");
        //If we have enough mana...
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >=
            Wraith.GetComponent<MouseDetect>().Cost)
        {
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
            }
            else
            {
                //Return flase if no place to summon minion to
                return false;
            }
            //Add instance to minions list for later referencing
            justSummoned.Add(DDOL.instance.ICS);
            DDOL.instance.currentObject.gameObject.GetComponent<MouseDetect>().DiminishMana(Wraith.GetComponent<MouseDetect>().Cost);
            return true;
        }
        else
        {
            //Return flase if not enough mana
            Debug.Log("Summon wraith failure");
            return false;
        }
    }

    //Moves minions closer to enemy player
    public void MoveMinion(GameObject m)
    {
        //Set DDOL flags to move minion
        Debug.Log("before: " + m.transform.position);
        DDOL.instance.option = "move";
        //Get locations minion in question can move to
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, m.GetInstanceID());
        if (loc.Count != 0)
        {
            //get location of other player
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            //get smallest distance between it and possible locations
            GameObject moveTo = loc[0];
            float min = Vector3.Distance(p.transform.position, loc[0].transform.position);
            foreach (GameObject l in loc)
            {
                if (Vector3.Distance(p.transform.position, l.transform.position) < min)
                {
                    min = Vector3.Distance(p.transform.position, l.transform.position);
                    moveTo = l;
                }
            }
            //compare against current location
            if (min < Vector3.Distance(p.transform.position, m.transform.position))
            {
                //if closer, move and wait
                DDOL.instance.MoveCharacter(moveTo.transform);
            }
        }
        Debug.Log("after: " + m.transform.position);
    }

    public void MoveSelf()
    {
        //Set DDOL flags to move
        DDOL.instance.option = "move";
        //Get locations can move to
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        //get location of other player
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        //get smallest distance between it and possible locations
        GameObject moveTo = loc[0];
        float min = Vector3.Distance(p.transform.position, loc[0].transform.position);
        foreach (GameObject l in loc)
        {
            if (Vector3.Distance(p.transform.position, l.transform.position) < min)
            {
                min = Vector3.Distance(p.transform.position, l.transform.position);
                moveTo = l;
            }
        }
        //compare against current location
        if (min < Vector3.Distance(p.transform.position, DDOL.instance.currentObject.transform.position))
        {
            //if closer, move and wait
            DDOL.instance.MoveCharacter(moveTo.transform);
        }
    }

    //Minions attack enemies in range with prefernce for the player,
    //then lowest health enemy minions
    public void MinionAttack(GameObject m)
    {
        //Set DDOL flags to attack
        DDOL.instance.option = "attack";
        //Get list of things minion can attack
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, m.GetInstanceID());
        if (loc.Count != 0)
        {
            //check for player
            foreach (GameObject l in loc)
            {
                if (DDOL.instance.FindCurrentObject(l).tag == "Player")
                {
                    DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                    return;
                }
            }
            //otherwise, attack whatever has the lowest health
            GameObject v = DDOL.instance.FindCurrentObject(loc[0]);
            foreach (GameObject l in loc)
            {
                if (DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().HP <
                    v.GetComponent<MouseDetect>().HP)
                {
                    v = DDOL.instance.FindCurrentObject(l);
                }
            }
            v.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
            return;
        }
    }

    //Cast UnLifeBlast on player if possible, otherwise on minion with most health
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
                //check for player
                foreach (GameObject l in loc)
                {
                    if (DDOL.instance.FindCurrentObject(l).tag == "Player")
                    {
                        //VALUES HARDCODED- MAY NEED TO BE CHECKED
                        DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().DamageHP(2);
                        DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(2);
                        return true;
                    }
                }
                //otherwise, attack whatever has the most health
                GameObject v = DDOL.instance.FindCurrentObject(loc[0]);
                foreach (GameObject l in loc)
                {
                    if (DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().HP >
                        v.GetComponent<MouseDetect>().HP)
                    {
                        v = DDOL.instance.FindCurrentObject(l);
                    }
                }
                v.GetComponent<MouseDetect>().DamageHP(2);
                DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(2);
                return true;
            }
            return false;
        }
        return false;
    }

    //Cast Swarm if 3 enough spots to summon minions into
    public bool Swarm()
    {
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 2)
        {
            DDOL.instance.option = "summon";
            DDOL.instance.summon = Skeleton;
            DDOL.instance.spell = "Swarm";
            DDOL.instance.currentCost = 2;
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count >= 3)
            {
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
                justSummoned.Add(DDOL.instance.ICS);
                loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
                justSummoned.Add(DDOL.instance.ICS);
                loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
                DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
                justSummoned.Add(DDOL.instance.ICS);
            }
            //MANA VALUE HARDCODED
            DDOL.instance.currentObject.gameObject.GetComponent<MouseDetect>().DiminishMana(2);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Cast LifeDrain, preference to attack player and restore self
    //If health is low, attack anything to restore self
    public bool LifeDrain()
    {
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 4)
        {
            DDOL.instance.option = "all";
            DDOL.instance.spell = "LifeDrain";
            DDOL.instance.currentCost = 4;
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (loc.Count != 0)
            {
                //check for player
                foreach (GameObject l in loc)
                {
                    if (DDOL.instance.FindCurrentObject(l).tag == "Player")
                    {
                        //VALUES HARDCODED- MAY NEED TO BE CHECKED
                        DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().DamageHP(4);
                        DDOL.instance.currentObject.GetComponent<MouseDetect>().HealHP(4);
                        return true;
                    }
                }
                if (DDOL.instance.currentObject.GetComponent<MouseDetect>().HP < 10)
                {
                    DDOL.instance.FindCurrentObject(loc[Random.Range(0, loc.Count - 1)]).GetComponent<MouseDetect>().DamageHP(4);
                    DDOL.instance.currentObject.GetComponent<MouseDetect>().HealHP(4);
                }
            }
            DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(4);
            return true;
        }
        else
        {
            return false;
        }
    }
}
