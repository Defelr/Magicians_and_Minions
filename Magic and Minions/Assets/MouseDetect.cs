using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour {
    bool onClick = false;
    GameObject grid_B;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        grid_B = GameObject.Find("Grid_Board");
        foreach(Transform child in grid_B.transform)
        {
            Collider col = child.GetComponent<Collider>();
            if (col.isTrigger)
            {
                OnTriggerStay(col);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        List<GameObject> locations = DDOL.instance.Movement(GameObject.Find("Grid_Board"), other.gameObject);
        foreach(GameObject c in locations)
        {
            Debug.Log(c.gameObject.name);
            c.transform.localPosition -= transform.up * 2;
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = true;
        }

    }

}
