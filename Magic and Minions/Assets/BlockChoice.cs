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
		foreach(GameObject c in DDOL.instance.loc)
        {
            Renderer R = c.GetComponent<Renderer>();
            if (R.enabled)
            {

            }
        }
	}
}
