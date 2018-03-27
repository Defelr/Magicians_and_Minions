﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChoice : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {

    }
    private void OnCollisionEnter()
    {
        Debug.Log("SPAWNED");
    }
    private void OnMouseOver()
    {
        if (DDOL.instance.option == "attack")
        {
            this.gameObject.GetComponent<Renderer>().material = DDOL.instance.G_Color;
        }
    }
    private void OnMouseExit()
    {
        this.gameObject.GetComponent<Renderer>().material = DDOL.instance.Gr_Color;
    }
    private void OnMouseDown()
    {
        Renderer Re = null;
        GameObject temp = null;
        Renderer R = this.gameObject.GetComponent<Renderer>();
        Collider C = GetComponent<Collider>();
        List<GameObject> tempList = new List<GameObject>();
        if (R.enabled)
        {
            if (DDOL.instance.option == "summon")
            {
                DDOL.instance.SummonPawn(transform);
                if (DDOL.instance.spell == "Swarm" && GetComponentInParent<Board_count>().times < 4)
                {
                    if(GetComponentInParent<Board_count>().times == 0)
                    {
                        DDOL.instance.GetCurrentPlayer().GetComponent<MouseDetect>().DiminishMana(DDOL.instance.currentCost);
                    }
                    foreach (GameObject c in DDOL.instance.spaces)
                    {
                        if (c.gameObject != this.gameObject)
                        {
                            tempList.Add(c);
                        }
                    }
                    DDOL.instance.spaces = tempList;
                    tempList = null;
                    R.enabled = false;
                    R.gameObject.GetComponentInParent<Board_count>().times++;
                    if (GetComponentInParent<Board_count>().times == 3)
                    {
                        DDOL.instance.spell = "";
                    }
                    DDOL.instance.ShowSpaces();
                }
                else
                {
                    DDOL.instance.summon = null;
                    this.GetComponentInParent<Board_count>().times = 0;
                    DDOL.instance.ClearSpaces();
                }
            }

            if (DDOL.instance.option == "move")
            {
                Debug.Log("You can move Here!");
                DDOL.instance.currentObject.GetComponent<MouseDetect>().Moves++;
                DDOL.instance.MoveCharacter(transform);
            }

            if(DDOL.instance.option == "attack")
            {
                for (int i = 0; i < DDOL.instance.x; i++)
                {
                    for (int j = 0; j < DDOL.instance.x; j++)
                    {
                        if (DDOL.instance.Coords[i][j].location == this.gameObject)
                        {
                            Debug.Log(DDOL.instance.Coords[i][j].G);
                            if (DDOL.instance.spell == "Unlife")
                            {
                                if(DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().HP - 2 <= 0)
                                {
                                    DDOL.instance.summon = DDOL.instance.IC.GetComponent<Magician_N>().Skeleton;

                                    DDOL.instance.SummonPawn(DDOL.instance.Coords[i][j].G.transform);

                                    Debug.Log("IT WORKED");
                                }
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().DamageHP(2);
                                DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(DDOL.instance.currentCost);
                                DDOL.instance.currentCost = 0;

                            }
                            else
                            {
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                                DDOL.instance.currentObject.GetComponent<MouseDetect>().Attacks++;
                            }
                            DDOL.instance.option = "";
                            DDOL.instance.spell = "";
                            DDOL.instance.ClearSpaces();
                            
                            return;
                        }
                    }
                }
            }

            if(DDOL.instance.spell != "Swarm")
            {
                DDOL.instance.option = "";
            }

        }
    }
}
