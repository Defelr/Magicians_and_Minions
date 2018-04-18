using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionKillerAI_Paladin : MonoBehaviour
{
    public GameObject GreatSpirit;
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
        justSummoned.Clear();
        //If less than 5 minions, summon minion, preference for wraiths
        if (minions.Count < 5)
        {
            Debug.Log("Summon minion");
            SummonSpirit();
        }
        //If at least 12 mana, summon 2nd minion, regardless
        //add a short wait here, too
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 12)
        {
            SummonSpirit();
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
        //wait between attacking and moving self
        MoveSelf();
        //wait between moving self and casting spells
        if (!UnLifeBlast())
        {
            LifeDrain();
        }
        //short wait here
        DDOL.instance.End_Turn();
    }

    //Summons great spirit to random location
    public bool SummonSpirit()
    {
        Debug.Log("Summon spirit attempt");
        //If we have enough mana...
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >=
            GreatSpirit.GetComponent<MouseDetect>().Cost)
        {
            Debug.Log("Summon spirit success");
            //Set DDOL flags to summon skeleton
            DDOL.instance.option = "summon";
            DDOL.instance.summon = GreatSpirit;
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
            DDOL.instance.currentObject.gameObject.GetComponent<MouseDetect>().DiminishMana(GreatSpirit.GetComponent<MouseDetect>().Cost);
            return true;
        }
        else
        {
            //Return false if not enough mana
            Debug.Log("Summon spirit failure");
            return false;
        }
    }

    //Moves minions closer to enemy
    public void MoveMinion(GameObject m)
    {
        //Set DDOL flags to move minion
        Debug.Log("before: " + m.transform.position);
        DDOL.instance.option = "attack";
        //Get locations minion in question can move to
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, m.GetInstanceID());
        DDOL.instance.option = "move";
        if (loc.Count != 0)
        {
            //get smallest distance between it and possible locations
            GameObject moveTo = loc[0];
            float min = Vector3.Distance(m.transform.position, loc[0].transform.position);
            foreach (GameObject l in loc)
            {
                if (Vector3.Distance(m.transform.position, l.transform.position) < min)
                {
                    min = Vector3.Distance(m.transform.position, l.transform.position);
                    moveTo = l;
                }
            }
            DDOL.instance.MoveCharacter(moveTo.transform);
        }
        Debug.Log("after: " + m.transform.position);
    }

    public void MoveSelf()
    {
        //Set DDOL flags to move
        DDOL.instance.option = "attack";
        //Get locations can move to
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        DDOL.instance.option = "attack";
        //get smallest distance between it and possible locations
        GameObject moveTo = loc[0];
        float min = Vector3.Distance(DDOL.instance.currentObject.transform.position, loc[0].transform.position);
        foreach (GameObject l in loc)
        {
            if (Vector3.Distance(DDOL.instance.currentObject.transform.position, l.transform.position) < min)
            {
                min = Vector3.Distance(DDOL.instance.currentObject.transform.position, l.transform.position);
                moveTo = l;
            }
        }
        DDOL.instance.MoveCharacter(moveTo.transform);
    }

    //Minions attack enemies in range with prefernce for lowest health enemy minions
    public void MinionAttack(GameObject m)
    {
        //Set DDOL flags to attack
        DDOL.instance.option = "attack";
        //Get list of things minion can attack
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, m.GetInstanceID());
        if (loc.Count != 0)
        {
            //attack whatever has the lowest health
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

    //Cast UnLifeBlast on enemy with most health
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
                //attack whatever has the most health
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

    //Cast LifeDrain, preference to attack minions and restore self
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
                    if (DDOL.instance.FindCurrentObject(l).tag != "Player")
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
