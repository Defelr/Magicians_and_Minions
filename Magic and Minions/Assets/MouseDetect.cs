using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour {
    bool onClick = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
    private void OnMouseDown()
    {
        GameObject grid_B = GameObject.Find("Grid_Board");
        if (!onClick)
        {
            onClick = true;
            foreach (Transform child in grid_B.transform)
            {
                Collider col = child.GetComponent<Collider>();
                if (col.isTrigger)
                {

                    DDOL.instance.Movement(GameObject.Find("Grid_Board"), child.gameObject);
                    foreach (GameObject c in DDOL.instance.spaces)
                    {
                        // Debug.Log(c.gameObject.name);
                        Renderer R = c.GetComponent<Renderer>();
                        R.enabled = true;
                    }
                    DDOL.instance.currentObject = gameObject;
                }
            }
        }
        else
        {
            if(DDOL.instance.currentObject == null)
            { 
                onClick = false;
            }
        }
    }
}
