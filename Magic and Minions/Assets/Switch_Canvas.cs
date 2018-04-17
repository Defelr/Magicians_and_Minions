using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Switch_Canvas : MonoBehaviour {

    public GameObject paladinInterface;
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

    private GameObject CurrentInterafce;

    private Slider healthSlider;
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
        if (DDOL.instance.currentObject)
        {
            GameObject tempCO = DDOL.instance.currentObject;
            if (tempCO.tag == "Necro")
            {
                foreach (Transform C in necroInterface.transform)
                {
                    if (C.gameObject.name == "Move_Btn")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Moves >= tempCO.GetComponent<MouseDetect>().Movement_c)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (2)")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Mana < tempCO.GetComponent<Magician_N>().Wraith.GetComponent<MouseDetect>().Cost)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;

                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (3)")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Mana < tempCO.GetComponent<Magician_N>().Skeleton.GetComponent<MouseDetect>().Cost)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;

                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn")
                    {
                        if (!tempCO.GetComponent<Magician_N>().UnLifeBlastCheck())
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (4)")
                    {
                        if (!tempCO.GetComponent<Magician_N>().SwarmCheck())
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (1)")
                    {
                        if (!tempCO.GetComponent<Magician_N>().LifeDrainCheck())
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                }
            }
            else if (tempCO.tag == "Paladin")
            {
                foreach (Transform C in paladinInterface.transform)
                {
                    if (C.gameObject.name == "Move_Btn")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Moves >= tempCO.GetComponent<MouseDetect>().Movement_c)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (2)")
                    {
                        if (!tempCO.GetComponent<Magician_N>().HolyFireCheck())
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;

                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (3)")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Mana < tempCO.GetComponent<Magician_N>().GreatSpirit.GetComponent<MouseDetect>().Cost)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;

                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn")
                    {
                        if (!tempCO.GetComponent<Magician_N>().ImplosionCheck())
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Ability_Btn (1)")
                    {
                        if (!tempCO.GetComponent<Magician_N>().GroupHealingCheck())
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                }
            }
            else //ELSE SHOULD ALWAYS HANDLE MINIONS
            {
                foreach (Transform C in minionInterface.transform)
                {
                    if (C.gameObject.name == "Move_Btn")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Moves >= tempCO.GetComponent<MouseDetect>().Movement_c)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                    else if (C.gameObject.name == "Attack_Btn")
                    {
                        if (tempCO.GetComponent<MouseDetect>().Attacks >= tempCO.GetComponent<MouseDetect>().Attack_c)
                        {
                            C.gameObject.GetComponent<Button>().interactable = false;
                        }
                        else
                        {
                            C.gameObject.GetComponent<Button>().interactable = true;
                        }
                    }
                }
            }
        }
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

            if (Physics.Raycast(DDOL.instance.currentCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, LM) && (DDOL.instance.option == "move" || DDOL.instance.option == "" || DDOL.instance.option == "summon" ))
            {
                if ((hitInfo.transform.gameObject.GetComponent<MouseDetect>().Movement_c != hitInfo.transform.gameObject.GetComponent<MouseDetect>().Moves) &&
                    hitInfo.transform.gameObject.GetComponent<MouseDetect>().Attack_c != hitInfo.transform.gameObject.GetComponent<MouseDetect>().Attacks)
                {
                    GameObject grid_B = GameObject.Find("Grid_Board");
                    if (DDOL.instance.spell != "Swarm")
                    {
                        DDOL.instance.ClearSpaces();
                    }
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
                                            DDOL.instance.SetObject(hitInfo.transform.gameObject.GetInstanceID(), 1, hitInfo.transform.gameObject, DDOL.instance.Coords[i][j].location);
                                            Debug.Log(hitInfo.transform.gameObject.GetInstanceID());
                                            Debug.Log(DDOL.instance.Coords[i][j].ID);
                                        }
                                    }
                                }
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
                    else if (hitInfo.transform.gameObject.tag == "GreatSpirit" && DDOL.instance.currentObject.tag == "GreatSpirit")
                    {
                        paladinInterface.SetActive(false);
                        minionInterface.SetActive(true);
                        wraithImage.SetActive(false);
                        skelImage.SetActive(false);
                        //greatSpiritImage.SetActive(true); THIS IS STILL NEEDED
                         
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
                }
            }
       }
    }
}
