using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour {
    bool onClick = false;
    public GameObject grid_B;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        grid_B = GameObject.Find("Grid_Board");
      /*  foreach(GameObject child in grid_B)
        {
            if()
        }*/
    }

}
