using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject Player1;
	// Use this for initialization
	void Start () {
        Player1.transform.localScale = new Vector3(15F, 15F, 15F);
        Vector3 x = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        Instantiate(Player1, x, transform.rotation);
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
