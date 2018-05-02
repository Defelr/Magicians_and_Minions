using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChoice : MonoBehaviour
{
    private bool Summon;
    private Transform SummonPos;
    // Use this for initialization
    void Start()
    {
       gameObject.GetComponent<ParticleSystem>().Stop();
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
        if (DDOL.instance.option == "attack" || DDOL.instance.option == "all")
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
                if(DDOL.instance.spell == "")
                {
                    DDOL.instance.summon = null;
                    this.GetComponentInParent<Board_count>().times = 0;
                    DDOL.instance.option = "";
                    DDOL.instance.ClearSpaces();
                }
            }

            else if (DDOL.instance.option == "move")
            {
                Debug.Log("You can move Here!");
                DDOL.instance.currentObject.GetComponent<MouseDetect>().Moves++;
                DDOL.instance.MoveCharacter(transform);
            }

            else if(DDOL.instance.option == "attack")
            {
                for (int i = 0; i < DDOL.instance.x; i++)
                {
                    for (int j = 0; j < DDOL.instance.x; j++)
                    {
                        if (DDOL.instance.Coords[i][j].location == this.gameObject)
                        {
                            if (DDOL.instance.spell == "Unlife")
                            {
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().OnMouseDown();
                            }
                            else if (DDOL.instance.spell == "") 
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
            else if (DDOL.instance.option == "all" || DDOL.instance.option == "allE" || DDOL.instance.option == "AllE")
            {
                for (int i = 0; i < DDOL.instance.x; i++)
                {
                    for (int j = 0; j < DDOL.instance.x; j++)
                    {
                        if (DDOL.instance.Coords[i][j].location == this.gameObject)
                        {
                            if (DDOL.instance.spell == "LifeDrain")
                            {
                                if (DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().HP - 4 <= 0)
                                {
                                    DDOL.instance.TempHP = DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().HP;
                                }
                                else
                                {
                                    DDOL.instance.TempHP = 4;
                                }
                                Debug.Log(DDOL.instance.spell + " " + DDOL.instance.Coords[i][j].G + " Block Choice");
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().DamageHP(4);
                                DDOL.instance.spell = "LifeDrain2";
                                DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(DDOL.instance.currentCost);
                                DDOL.instance.currentCost = 0;
                            }
                            else if (DDOL.instance.spell == "LifeDrain2")
                            {
                                Debug.Log(DDOL.instance.spell + " " + DDOL.instance.Coords[i][j].G + " Block Choice");
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().HealHP(DDOL.instance.TempHP);
                                DDOL.instance.spell = "";
                                DDOL.instance.TempHP = 0;
                            }else if (DDOL.instance.spell == "HolyFire")
                            {
                                DDOL.instance.ClearSpaces();
                                DDOL.instance.option = "all";
                                DDOL.instance.SpaceLocation(1, this.gameObject);
                                //ADD THE DAMAGE IT WILL DEAL TO SPACES HERE
                                DDOL.instance.AOE();
                            }
                            if(DDOL.instance.spell == "")
                            {
                                DDOL.instance.option = "";
                                DDOL.instance.ClearSpaces();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            DDOL.instance.option = "";
            DDOL.instance.spell = "";
            DDOL.instance.summon = null;
            DDOL.instance.ClearSpaces();
        }
    }
}
