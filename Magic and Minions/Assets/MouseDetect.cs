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
    public void Clicked()
    {
        Debug.Log("Start");
        Collider colp = GetComponent<Collider>();
        GameObject grid_B = GameObject.Find("Grid_Board");
        //if (!onClick)
        //{
            //onClick = true;
            foreach (Transform child in grid_B.transform)
            {
                Collider col = child.GetComponent<Collider>();
                if (col.isTrigger /*&& !colp.isTrigger*/) //Uncomment so that people can only move once. When turn ends, all triggers should go back to false
                {

                    DDOL.instance.Movement(child.gameObject, gameObject);
                    foreach (GameObject c in DDOL.instance.spaces)
                    {
                        // Debug.Log(c.gameObject.name);
                        Renderer R = c.GetComponent<Renderer>();
                        R.enabled = true;
                    }
                    DDOL.instance.currentObject = gameObject;
                    Debug.Log(name);
                }
                DDOL.instance.option = "move";
            }
        //}
        //else
        //{
            //if(DDOL.instance.currentObject == null)
            //{ 
                //onClick = false;
            //}
        //}
    }
}
