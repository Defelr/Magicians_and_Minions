using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerAI_Necromancer : MonoBehaviour {
    public GameObject Skeleton;
    public GameObject Wraith;
    public IList<GameObject> minions = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayTurn()
    {
        //If less than 7 minions, summon minion
        if (minions.Count < 7)
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                SummonWraith();
            }
            else
            {
                SummonSkeleton();
            }
        }
        //TODO: CHECK MANA
        //Select minion closest to opponent and move it
        GameObject m = minions[Random.Range(0, minions.Count - 1)];
        MoveMinion(m);
        DDOL.instance.currentObject = this.gameObject;
        //Attack opponent or opponent minion if in range
        //Try to cast spell

    }

    public void SummonSkeleton()
    {
        DDOL.instance.option = "summon";
        DDOL.instance.summon = Skeleton;
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (loc.Count != 0)
        {
            DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
        }
        minions.Add(DDOL.instance.summon);
    }
    public void SummonWraith()
    {
        DDOL.instance.option = "summon";
        DDOL.instance.summon = Wraith;
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (loc.Count != 0)
        {
            DDOL.instance.SummonPawn(loc[Random.Range(0, loc.Count - 1)].transform);
        }
        minions.Add(DDOL.instance.summon);

    }

    //TODO: FIX
    public GameObject MinionToMove()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        GameObject minion = minions[0];
        float min = Vector3.Distance(p.transform.position, minions[0].transform.position);
        foreach(GameObject m in minions)
        {
            Debug.Log("minion: " + m.transform.position);
            if (Vector3.Distance(p.transform.position, m.transform.position) < min)
            {
                min = Vector3.Distance(p.transform.position, m.transform.position);
                minion = m;
            }
        }
        return minion;
    }

    public void MoveMinion(GameObject m)
    {
        Debug.Log("before: " + m.transform.position);
        DDOL.instance.option = "move";
        DDOL.instance.currentObject = m;
        List<GameObject> loc = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (loc.Count != 0)
        {
            DDOL.instance.MoveCharacter(loc[Random.Range(0, loc.Count - 1)].transform);
        }
        Debug.Log("after: " + m.transform.position);
    }
}
