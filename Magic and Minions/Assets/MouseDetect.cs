using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDetect : MonoBehaviour
{

    public int HP;
    public int MAX_HP;
    public int DMG;
    public int Mana;
    public int Cost;

    public int Movement_c; //AMOUNT OF TIMES THEY CAN MOVE
    public int Attack_c;  //AMOUNT OF TIMES THEY CAN ATTACK

    public int Moves;//Amount of times moved/attacked
    public int Attacks;

    public Slider healthSlider = null;
    public Text manaSlider = null;
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
        if (gameObject.GetComponent<ParticleSystem>())
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
        foreach (Transform TPanel in DDOL.instance.SystemEvent.GetComponent<Switch_Canvas>().MenuCanvasPanel.transform)
        {
            if (TPanel.tag == this.tag)
            {
                foreach (Transform BPanel in TPanel)
                {
                    if (BPanel.tag == this.tag)
                    {
                        healthSlider = BPanel.Find("Health_Sldr").gameObject.GetComponent<Slider>();
                        healthSlider.maxValue = GetComponent<MouseDetect>().MAX_HP;
                        healthSlider.value = GetComponent<MouseDetect>().HP;
                        manaSlider = BPanel.Find("Mana").gameObject.GetComponent<Text>();
                        manaSlider.text = GetComponent<MouseDetect>().Mana.ToString();
                    }
                }
            }
            else if (TPanel.tag == this.tag || this.tag == "Wraith" || this.tag == "Skeleton" || this.tag == "GreatSpirit")
            {
                foreach (Transform BPanel in TPanel)
                {
                    if (BPanel.tag == this.tag)
                    {
                        healthSlider = BPanel.Find("Health_Sldr").gameObject.GetComponent<Slider>();
                        healthSlider.maxValue = GetComponent<MouseDetect>().MAX_HP;
                        healthSlider.value = GetComponent<MouseDetect>().HP;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            for (int i = 0; i < DDOL.instance.x; i++)
            {
                for (int j = 0; j < DDOL.instance.x; j++)
                {
                    if (DDOL.instance.Coords[i][j].G == this.gameObject)
                    {
                        if (this.gameObject == DDOL.instance.IC.gameObject)
                        {

                            HotseatWin.winVar = 2;
                            Debug.Log("PLAYER 2 WON");
                        }
                        else if (this.gameObject == DDOL.instance.IC2.gameObject)
                        {
                            HotseatWin.winVar = 1;
                            Debug.Log("PLAYER 1 WON");
                        }
                        Destroy(this.gameObject);
                        DDOL.instance.Coords[i][j] = new Coordinates(-1, 0, -1, null, DDOL.instance.locations[i][j]);
                        return;
                    }
                }
            }
            //Work on getting the Particle System to stop when you select another character // probably also when the turn ends
            if(DDOL.instance.currentObject.gameObject != gameObject)
            {
                gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }
        if (HP > MAX_HP)
        {
            HP = MAX_HP; //Makes sure HP doesn't exceed MAX_HP when healing
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
        this.manaSlider.text = Mana.ToString();
    }
    public void IncrementMana(int ManaCost)
    {
        Mana += ManaCost;
        this.manaSlider.text = Mana.ToString();
    }
    public void DamageHP(int DecHP)
    {
        HP -= DecHP;
        this.healthSlider.value = HP;
                    //new Coordinates(-1, 0, -1, null, DDOL.instance.locations[i][j]);
        if (HP <= 0)
        {
            for (int i = 0; i < DDOL.instance.x; i++)
            {
                for (int j = 0; j < DDOL.instance.x; j++)
                {
                    if (DDOL.instance.Coords[i][j].G == this.gameObject)
                    {
                        DDOL.instance.Coords[i][j] = new Coordinates(-1, 0, -1, null, DDOL.instance.locations[i][j]);
                        Destroy(this.gameObject);
                        return;
                    }
                }
            }
        }
    }
    public void HealHP(int IncHP)
    {
        HP += IncHP;
        this.healthSlider.value = HP;
    }
    public void OnMouseOver()
    {
        if (DDOL.instance.option == "attack" || DDOL.instance.option == "all")
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
                    if(HP - 2 <= 0)
                    {
                        DDOL.instance.summon = DDOL.instance.IC.GetComponent<Magician_N>().Skeleton;
                        DDOL.instance.SummonPawn(this.transform);
                    }
                    DamageHP(2);

                }
                else
                {
                    DamageHP(DDOL.instance.currentObject.GetComponent<MouseDetect>().DMG);
                    DDOL.instance.currentObject.GetComponent<MouseDetect>().Attacks++;
                }
                DDOL.instance.option = "";
                DDOL.instance.spell = "";
                DDOL.instance.ClearSpaces();
                return;
            }
        }else if (DDOL.instance.option == "all")
        {
            if (DDOL.instance.spell == "LifeDrain")
            {
                if (HP - 4 <= 0)
                {
                    DDOL.instance.TempHP = HP;

                }
                else
                {
                    DDOL.instance.TempHP = 4;
                }
                Debug.Log(DDOL.instance.spell + " " + this.gameObject + " Mouse Detect");
                DDOL.instance.spell = "LifeDrain2";
                DamageHP(4);
            }
            else if (DDOL.instance.spell == "LifeDrain2")
            {
                Debug.Log(DDOL.instance.spell + " " + this.gameObject + " Mouse Detect");
                HealHP(DDOL.instance.TempHP);
                DDOL.instance.spell = "";
                DDOL.instance.option = "";
                DDOL.instance.TempHP = 0;
                DDOL.instance.ClearSpaces();
            }
        }
    }
    public void Move()
    {
        DDOL.instance.summon = null;
        DDOL.instance.option = "";
        DDOL.instance.spell = "";
        DDOL.instance.UnShowSpaces();
        DDOL.instance.spaces.Clear();
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
            DDOL.instance.ShowSpaces();
            }
    }
    public void Attack()
    {
        DDOL.instance.summon = null;
        DDOL.instance.option = "";
        DDOL.instance.spell = "";
        DDOL.instance.UnShowSpaces();
        DDOL.instance.spaces.Clear();
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
            DDOL.instance.ShowSpaces();
        }
    }
}
