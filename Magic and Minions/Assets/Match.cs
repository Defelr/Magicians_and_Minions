using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour {
    public GameObject EndTurn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Player2B()
    {
        this.GetComponent<Animator>().SetTrigger("CamB2");
    }
    public void Player1B()
    {
        this.GetComponent<Animator>().SetTrigger("CamB1");
    }
    public void Deactivate()
    {
        EndTurn.GetComponent<Button>().interactable = false;
    }
    public void ReActivate()
    {
        EndTurn.GetComponent<Button>().interactable = true;
    }
}
