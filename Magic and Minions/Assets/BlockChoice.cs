using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChoice : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
    }
    private void OnMouseDown () {
        Renderer Re = null;
        Renderer R = GetComponent<Renderer>();
        Collider C = GetComponent<Collider>();
        if (R.enabled)
        {
            if(DDOL.instance.option == "summon")
            {
                DDOL.instance.option = "NaN";
                DDOL.instance.SummonPawn(transform);
                foreach(GameObject c in DDOL.instance.spaces)
                {
                    Re = c.GetComponent<Renderer>();
                    Re.enabled = false;
                }
                DDOL.instance.spaces.Clear();
                for (int i = 0; i < DDOL.instance.x; i++)
                {
                    for (int j = 0; j < DDOL.instance.x; j++)
                    {
                        if(Re == DDOL.instance.Coords[i][j].location.GetComponent<Renderer>())
                        {
                               // DDOL.instance.Coords[i][j].G = 
                        }
                    }
                    Debug.Log("\n");
                }
            }
            else
            {
                Debug.Log("You can move Here!");
                DDOL.instance.MoveCharacter(transform);
            }
            C.isTrigger = true;
        }
    }
}
