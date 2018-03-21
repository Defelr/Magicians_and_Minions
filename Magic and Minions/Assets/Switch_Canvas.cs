using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Switch_Canvas : MonoBehaviour {

    public GameObject necroInterface;
    public GameObject minionInterface;
    public GameObject wraithImage;
    public GameObject skelImage;

    //public Text necroMana;
    //public Text necroHP;

    public LayerMask player1;
    public LayerMask player2;
    private LayerMask LM;   

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            if (DDOL.instance.turn % 2 == 0)
            {
                LM = player1;
            }
            else
            {
                LM = player2;
            }
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(DDOL.instance.currentCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LM))
            {
                int temp_x = -1, temp_y =-1;
                for(int i = 0; i < DDOL.instance.x; i++)
                {
                    for(int j= 0; j  < DDOL.instance.x; j++)
                    {
                        if(DDOL.instance.Coords[i][j].ID == hitInfo.transform.gameObject.GetInstanceID())
                        {
                            temp_x = i;
                            temp_y = j;
                        }
                    }
                }
                if (hitInfo.transform.gameObject.tag == "Necro")
                {
                    minionInterface.SetActive(false);
                    necroInterface.SetActive (true);
                    wraithImage.SetActive(false);
                    skelImage.SetActive(false);
                    //necroMana.text = DDOL.instance.Coords[temp_x][temp_y].D.MANA.ToString();
                    //necroHP.text = DDOL.instance.Coords[temp_x][temp_y].D.HP.ToString();


                } 
                if (hitInfo.transform.gameObject.tag == "Wraith" && DDOL.instance.currentObject.tag == "Wraith" )
                {
                    necroInterface.SetActive(false);
                    minionInterface.SetActive(true);
                    skelImage.SetActive(false);
                   wraithImage.SetActive(true);
                }
                else if (hitInfo.transform.gameObject.tag == "Skeleton" && DDOL.instance.currentObject.tag == "Skeleton")
                {
                    necroInterface.SetActive(false);
                    minionInterface.SetActive(true);
                    wraithImage.SetActive(false);
                    skelImage.SetActive(true);
                }
            }
       }
    }
}
