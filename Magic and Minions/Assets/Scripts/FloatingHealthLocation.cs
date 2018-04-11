using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthLocation : MonoBehaviour {

    float yPos = 15;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + yPos);
	}
}
