using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinionKillerAI_Paladin : MonoBehaviour
{
    public GameObject GreatSpirit;
    public int map;
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

    public void PlayTurnButton()
    {
        StartCoroutine("PlayTurn");
    }

    IEnumerator PlayTurn()
    {
        DDOL.instance.currentObject = DDOL.instance.IC2;
        ai = DDOL.instance.IC2;
        foreach (GameObject m in justSummoned)
        {
            minions.Add(m);
        }
        justSummoned.Clear();
        foreach (GameObject m in minions)
        {
            if (m == null) { minions.RemoveAt(minions.IndexOf(m)); }
        }
        yield return new WaitForSeconds(2.5f);
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
            yield return new WaitForSeconds(0.5f);
            SummonSpirit();
        }
        //wait between summoning and moving
        yield return new WaitForSeconds(0.5f);
        //move minion
        foreach (GameObject m in minions)
        {
            DDOL.instance.currentObject = m;
            MoveMinion(m);
            DDOL.instance.currentObject = ai;
            yield return new WaitForSeconds(0.25f);
        }
        //wait between moving and attacking
        //if there is anything within attack radius, attack
        foreach (GameObject m in minions)
        {
            yield return new WaitForSeconds(0.25f);
            DDOL.instance.currentObject = m;
            MinionAttack(m);
            DDOL.instance.currentObject = ai;
        }
        //wait between attacking and moving self
        yield return new WaitForSeconds(0.25f);
        MoveSelf();
        //wait between moving self and casting spells
        yield return new WaitForSeconds(0.5f);
        if (!HolyFire())
        {
            if (!GroupHealing())
            {
                Implosion();
            }
        }
        //short wait here
        yield return new WaitForSeconds(0.25f);
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

    //Cast implosion
    public bool Implosion()
    {
        //If enough mana
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 2)
        {
            DDOL.instance.option = "attack";
            DDOL.instance.spell = "HolyFire";
            DDOL.instance.currentCost = 2;
            //Get all locations
            List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            foreach (GameObject l in loc)
            {
                //Damage
                DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().DamageHP(1);
            }
            //Diminsh mana
            DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(2);
            return true;
        }
        return false;
    }

    //Cast holy fire
    public bool HolyFire()
    {
        //If enough mana
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 6)
        {
            DDOL.instance.option = "attack";
            DDOL.instance.spell = "HolyFire";
            DDOL.instance.currentCost = 6;
            //Get all locations
            List<GameObject> loc = DDOL.instance.SpaceLocation(3, DDOL.instance.currentObject.GetInstanceID());
            foreach (GameObject l in loc)
            {
                //Damage
                DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().DamageHP(1);
            }
            //Diminsh mana
            DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(6);
            return true;
        }
        return false;
    }

    //cast group healing
    public bool GroupHealing()
    {
        //if enough mana
        if (DDOL.instance.currentObject.GetComponent<MouseDetect>().Mana >= 3)
        {
            DDOL.instance.option = "friendly";
            DDOL.instance.spell = "GroupHealing";
            DDOL.instance.currentCost = 3;
            //Get all locations
            List<GameObject> loc = DDOL.instance.SpaceLocation(3, DDOL.instance.currentObject.GetInstanceID());
            foreach (GameObject l in loc)
            {
                //Damage
                DDOL.instance.FindCurrentObject(l).GetComponent<MouseDetect>().HealHP(5);
            }
            //Diminsh mana
            DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(3);
            return true;
        }
        return false;
    }
}
