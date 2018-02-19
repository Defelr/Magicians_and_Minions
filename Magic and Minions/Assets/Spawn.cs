using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject Player1;
	// Use this for initialization
	void Start () {
        Player1.transform.localScale = new Vector3(15F, 15F, 15F);
        Instantiate(Player1, transform.position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
