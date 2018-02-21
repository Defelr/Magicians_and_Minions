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
        Renderer R = GetComponent<Renderer>();
        Collider C = GetComponent<Collider>();
        if (R.enabled)
        {
            if(DDOL.instance.option == "summon")
            {
                DDOL.instance.SummonPawn(transform);
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
