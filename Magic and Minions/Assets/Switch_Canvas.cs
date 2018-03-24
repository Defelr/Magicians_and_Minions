﻿using System.Collections;
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
    public GameObject MenuCanvasPanel;

    public LayerMask player1;
    public LayerMask player2;
    private LayerMask LM;   

	// Use this for initialization
	void Start () {
		
	}

    public void Clear()
    {
        foreach(Transform TPanel in MenuCanvasPanel.transform)
        {
            TPanel.gameObject.SetActive(false);
        }
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
                int temp_x = -1, temp_y = -1;
                for (int i = 0; i < DDOL.instance.x; i++)
                {
                    for (int j = 0; j < DDOL.instance.x; j++)
                    {
                        if (DDOL.instance.Coords[i][j].ID == hitInfo.transform.gameObject.GetInstanceID())
                        {
                            temp_x = i;
                            temp_y = j;
                        }
                    }
                }
                if (hitInfo.transform.gameObject.tag == "Necro")
                {
                    foreach (Transform TPanel in MenuCanvasPanel.transform)
                    {
                        if (TPanel.tag != "Necro")
                        {
                            TPanel.gameObject.SetActive(false);
                        }
                        else
                        {
                            TPanel.gameObject.SetActive(true);
                        }
                    }
                }
                else if (hitInfo.transform.gameObject.tag == "Wraith" && DDOL.instance.currentObject.tag == "Wraith")
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
                else if (hitInfo.transform.gameObject.tag == "Paladin")
                {
                    foreach (Transform TPanel in MenuCanvasPanel.transform)
                    {
                        if (TPanel.tag != "Paladin")
                        {
                            TPanel.gameObject.SetActive(false);
                        }
                        else
                        {
                            TPanel.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    Clear();
                }
                GameObject grid_B = GameObject.Find("Grid_Board");
                DDOL.instance.ClearSpaces();
                foreach (Transform child in grid_B.transform)
                {
                    Collider col = child.GetComponent<Collider>();
                    Collider thiscol = hitInfo.transform.GetComponent<Collider>();
                    if (col.bounds.Intersects(thiscol.bounds))
                    {
                        for (int i = 0; i < DDOL.instance.x; i++)
                        {
                            for (int j = 0; j < DDOL.instance.x; j++)
                            {
                                Debug.Log("Mouse Down");
                                Debug.Log(DDOL.instance.Coords[i][j].location.gameObject.name);
                                Debug.Log(col.gameObject.name);
                                if (DDOL.instance.Coords[i][j].location.gameObject.name == col.gameObject.name)
                                {
                                    if ((DDOL.instance.player == 0 || DDOL.instance.player == 1) && DDOL.instance.player == DDOL.instance.Coords[i][j].Team)
                                    {
                                        Debug.Log("IN ID ADDER");
                                        Debug.Log(hitInfo.transform.gameObject.layer.ToString());
                                        DDOL.instance.SetObject(hitInfo.transform.gameObject.GetInstanceID(), 1, DDOL.instance.Coords[i][j].D, hitInfo.transform.gameObject, DDOL.instance.Coords[i][j].location);
                                        Debug.Log(hitInfo.transform.gameObject.GetInstanceID());
                                        Debug.Log(DDOL.instance.Coords[i][j].ID);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }
            }
       }
    }
}
