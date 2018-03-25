using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetect : MonoBehaviour
{
    public int HP;
    public int DMG;
    public int Mana;
    public int Cost;

    public int Movement_c; //AMOUNT OF TIMES THEY CAN MOVE
    public int Attack_c;  //AMOUNT OF TIMES THEY CAN ATTACK

    public int Moves;//Amount of times moved/attacked
    public int Attacks;



    // Use this for initialization
    void Start()
    {
        //PLAYER 1 and PLAYER 2 can move, but anything the initially summon cannot until the next turn
        if (DDOL.instance.IC.gameObject == this.gameObject || DDOL.instance.IC2.gameObject == this.gameObject)
        {
            Moves = 0;
            Attacks = 0;
        }
        else
        {
            Moves = Movement_c;
            Attacks = Attack_c;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            for (int i = 0; i < DDOL.instance.x; i++)
            {
                for (int j = 0; j < DDOL.instance.x; j++)
                {
                    if(DDOL.instance.Coords[i][j].G == this.gameObject)
                    {
                        DDOL.instance.Coords[i][j] = new Coordinates(-1, 0, -1, null, DDOL.instance.locations[i][j]);
                        if(this.gameObject == DDOL.instance.IC.gameObject)
                        {
                            Debug.Log("PLAYER 2 WON");
                        }else if(this.gameObject == DDOL.instance.IC2.gameObject)
                        {
                            Debug.Log("PLAYER 1 WON");
                        }
                        Destroy(this.gameObject);
                        return;
                    }
                }
            }
        }
    }
    public void ResetV()
    {
        Moves = 0;
        Attacks = 0;
    }
    public void DiminishMana(int ManaCost)
    {
        Mana -= ManaCost;
    }
    public void IncrementMana(int ManaCost)
    {
        Mana += ManaCost;
    }
    public void DamageHP(int DecHP)
    {
        HP -= DecHP; 
    }
    public void OnMouseOver()
    {
        if (DDOL.instance.option == "attack")
        {
            for (int i = 0; i < DDOL.instance.x; i++)
            {
                for(int j = 0; j < DDOL.instance.x; j++)
                {
                    if(DDOL.instance.Coords[i][j].ID == this.gameObject.GetInstanceID())
                    {
                        DDOL.instance.Coords[i][j].location.gameObject.GetComponent<Renderer>().material = DDOL.instance.G_Color;
                    }
                }
            }
        }
    }
    public void OnMouseExit()
    {
        for (int i = 0; i < DDOL.instance.x; i++)
        {
            for (int j = 0; j < DDOL.instance.x; j++)
            {
                if (DDOL.instance.Coords[i][j].G == this.gameObject)
                {
                    DDOL.instance.Coords[i][j].location.gameObject.GetComponent<Renderer>().material = DDOL.instance.Gr_Color;
                }
            }
        }
    }
    public void OnMouseDown()
    {
        if (DDOL.instance.option == "attack")
        {
            if (DDOL.instance.TeamFinder(DDOL.instance.currentObject) != DDOL.instance.TeamFinder(this.gameObject))
            {
                if (DDOL.instance.spell == "Unlife")
                {
                    if(this.GetComponent<MouseDetect>().HP - 2 <= 0)
                    {
                        DDOL.instance.summon = DDOL.instance.IC.GetComponent<Magician_N>().Skeleton;
                        DDOL.instance.SummonPawn(this.transform);
                        Debug.Log("IT WORKED");
                    }
                    this.GetComponent<MouseDetect>().DamageHP(2);

                }
                else
                {
                    this.GetComponent<MouseDetect>().DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                    DDOL.instance.currentObject.GetComponent<MouseDetect>().Attacks++;
                }
                DDOL.instance.option = "";
                DDOL.instance.spell = "";
                DDOL.instance.ClearSpaces();
                return;
            }
        }
    }
    public void Move()
    {
        Debug.Log("MOVE: " + Moves);
            Debug.Log("Start");
            List<GameObject> spaces = new List<GameObject>();
            DDOL.instance.option = "move";
            spaces = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
            if (spaces.Count <= 0 || DDOL.instance.currentObject.GetComponent<MouseDetect>().Moves >= DDOL.instance.currentObject.GetComponent<MouseDetect>().Movement_c)
            {
                Debug.Log("Can't Move");
                spaces.Clear();
            }
            else
            {
            foreach (GameObject c in spaces)
                {
                    Renderer R = c.GetComponent<Renderer>();
                    R.enabled = true; 
                }
            }
    }
    public void Attack()
    {
        Debug.Log("ATTACK STARTS");
        List<GameObject> spaces = new List<GameObject>();
        DDOL.instance.option = "attack";
        spaces = DDOL.instance.SpaceLocation(1, DDOL.instance.currentObject.GetInstanceID());
        if (spaces.Count <= 0 || DDOL.instance.currentObject.GetComponent<MouseDetect>().Attacks >= DDOL.instance.currentObject.GetComponent<MouseDetect>().Attack_c)
        {
            Debug.Log("Can't Attack Anyone");
            DDOL.instance.option = "";
            spaces.Clear();
        }
        else
        {
            foreach (GameObject c in spaces)
            {
                Renderer R = c.GetComponent<Renderer>();
                R.enabled = true;
            }
        }
    }
}
