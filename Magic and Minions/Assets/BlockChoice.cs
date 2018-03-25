using System.Collections;
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
        Renderer R = GetComponent<Renderer>();
        Collider C = GetComponent<Collider>();
        if (R.enabled)
        {
            if (DDOL.instance.option == "summon")
            {
                DDOL.instance.SummonPawn(transform);
                foreach (GameObject c in DDOL.instance.spaces)
                {
                    Re = c.GetComponent<Renderer>();
                    Re.enabled = false;
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
                                    DDOL.instance.SummonPawn(this.transform);

                                    Debug.Log("IT WORKED");
                                }
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().DamageHP(2);

                            }
                            else
                            {
                                DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                            }
                            DDOL.instance.option = "";
                            DDOL.instance.spell = "";
                            DDOL.instance.ClearSpaces();
                            
                            return;
                        }
                    }
                }
            }
            C.isTrigger = true;
            DDOL.instance.option = "";
        }
    }
}
