using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour {
    public GameObject EndTurn;
    public GameObject P1;
    public GameObject P2;
    public GameObject D;

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
        DDOL.instance.ANIMATING = true;
    }
    public void ReActivate()
    {
        EndTurn.GetComponent<Button>().interactable = true;
        DDOL.instance.ANIMATING = false;
    }
    IEnumerator StartText()
    {
        yield return new WaitForSeconds(5F);
        D.GetComponent<DialogueManager>().ClearBoth();
    }
    public void StartText2()
    {
        D.GetComponent<DialogueManager>().StartGame();
        Player1B();
        EndTurn.GetComponent<Button>().interactable = true;
       StartCoroutine(StartText());
    }
    
}
