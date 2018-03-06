using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician_N : MonoBehaviour {
    public GameObject Wraith;
    public GameObject Skeleton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SummonSkeleton()
    {
        DDOL.instance.option = "summon";
        Debug.Log("Start");
        Collider colp = GetComponent<Collider>();
        GameObject grid_B = GameObject.Find("Grid_Board");
        foreach (Transform child in grid_B.transform)
        {
            Collider col = child.GetComponent<Collider>();
            if (col.isTrigger /*&& !colp.isTrigger*/) //Uncomment so that people can only move once. When turn ends, all triggers should go back to false
            {

                List<GameObject> spaces = DDOL.instance.Movement(child.gameObject, gameObject);
                DDOL.instance.SetSummon(Skeleton);
                foreach (GameObject c in DDOL.instance.spaces)
                {
                    // Debug.Log(c.gameObject.name);
                    Renderer R = c.GetComponent<Renderer>();
                    R.enabled = true;
                }
                Debug.Log(name);
            }
        }
    }
    public void SummonWraith()
    {
        DDOL.instance.option = "summon";
        Debug.Log("Start");
        Collider colp = GetComponent<Collider>();
        GameObject grid_B = GameObject.Find("Grid_Board");
        foreach (Transform child in grid_B.transform)
        {
            Collider col = child.GetComponent<Collider>();
            if (col.isTrigger /*&& !colp.isTrigger*/) //Uncomment so that people can only move once. When turn ends, all triggers should go back to false
            {

                DDOL.instance.Movement(child.gameObject, gameObject);
                DDOL.instance.SetSummon(Wraith);
                foreach (GameObject c in DDOL.instance.spaces)
                {
                    // Debug.Log(c.gameObject.name);
                    Renderer R = c.GetComponent<Renderer>();
                    R.enabled = true;
                }
                Debug.Log(name);
            }
        }
    }
}
