using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Coordinates
{
    public int ID, status, Team;// 0 == empty, 1 == occupied, there might be more
    public GameObject G, location;

    public Coordinates(int ID1, int S, int TEAM, GameObject G1, GameObject L1)
    {
        Team = TEAM;
        ID = ID1;
        status = S;
        G = G1;
        location = L1;
    }
}

public class DDOL : MonoBehaviour
{

    public AudioClip moveSound;
    private AudioSource source;

    public GameObject First;


    public static DDOL instance = null;
    public int turn = 1;
    public int player = 0;
    //Grid size
    public int x = 8; //n == x board is n X n length
    public List<List<Coordinates>> Coords;
    public List<List<GameObject>> locations;
    public List<GameObject> spaces;
    public GameObject currentObject; //The player whose action is being taken
    private GameObject currentObjectL;//The location of that player
    private GameObject currentTarget;
    Coordinates Coord;

    public GameObject StartingC;
    public GameObject StartingC2;
    public Transform SC;
    public Transform SC2;
    public GameObject IC;
    public GameObject IC2;


    private int temp_x = -1, temp_y = -1;
    //FOR SUMMONING
    public GameObject summon;

    public string option = "";
    //Spells
    public string spell;
    public int currentCost;

    public GameObject SystemEvent;

    //FOR BlockChoice
    public Material G_Color;
    public Material Gr_Color;

    public int TempHP;

    public GameObject ICS; 
    public void ResetCharacters(Transform ParentPlayer) { 
        foreach(Transform T in ParentPlayer)
        {
            T.gameObject.GetComponent<MouseDetect>().ResetV();
        }
    }
    //Clears the system UI, meaning nothing should be showing
    public void ClearUI()
    {
        SystemEvent.GetComponent<Switch_Canvas>().Clear();
    }
    public void SetPlayer1(GameObject P)
    {
        StartingC = P;
        //PLAYER 1 INFO
        StartingC.transform.localScale = new Vector3(15F, 15F, 15F);
        GameObject new_p = Coords[0][0].location;
        Vector3 vx = new Vector3(new_p.transform.position.x, 5.5F, new_p.transform.position.z);
        StartingC.gameObject.layer = LayerMask.NameToLayer("Player1");
        IC = (GameObject)Instantiate(StartingC, vx, new_p.transform.rotation);
        Coords[0][0] = new Coordinates(IC.GetInstanceID(), 1, 0, IC, Coords[0][0].location);
        IC.transform.parent = SC.gameObject.transform;

    }
    public void SetPlayer2(GameObject P)
    {
        StartingC2 = P;
        //PLAYER 2 INFO
        StartingC2.transform.localScale = new Vector3(15F, 15F, 15F);
        GameObject new_p = Coords[x - 1][x - 1].location;
        Vector3 vx = new Vector3(new_p.transform.position.x, 5.5F, new_p.transform.position.z);
        StartingC2.gameObject.layer = LayerMask.NameToLayer("Player2");
        IC2 = (GameObject)Instantiate(StartingC2, vx, new_p.transform.rotation);
        //IC2.transform.rotation.Set(new_p.transform.rotation.x, new_p.transform.rotation.y + 180, new_p.transform.rotation.z, new_p.transform.rotation.w);
        IC2.transform.Rotate(Vector3.up * 180f);
        Coords[x - 1][x - 1] = new Coordinates(IC2.GetInstanceID(), 1, 1, IC2, Coords[x - 1][x - 1].location);
        IC2.transform.parent = SC2.gameObject.transform;
    }
    //We setup the gameboard, and player 1 and 2
    public void Start()
    {
      //  SystemEvent.GetComponent<Quit>().ResetWC();
        currentObject = null;
        ClearSpaces();
        locations = PossibleSpaces(GameObject.Find("Grid_Board"));
        Coords = new List<List<Coordinates>>();
        for (int i = 0; i < x; i++)
        {
            Coords.Add(new List<Coordinates>());
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Coord = new Coordinates(-1, 0, -1, null, locations[i][j]);
                Coords[i].Add(Coord);
            }
        }
    }

    //Updates so that we know which player to be refering to 0 == player 1 while 1 == player2
    public void Update()
    {
        player = turn % 2;
    }

    public void Awake()
    {
        source = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }
    IEnumerator StartMatch()
    {
        yield return new WaitForSeconds(First.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + First.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);

    }
    /*
 * End Turn will enable the right cameras, clears the board, clears the UI, and abilities
 * Clear all options, and essentially resets it so that the player can make a move
 * 
 */
    public void End_Turn()
    {
        int mana = 0;
        if (player == 0)
        {
            First.GetComponent<Animator>().SetTrigger("Player2");
            if (IC)
            {
                mana = IC2.GetComponent<Magician_N>().ManaMechanic();
                IC2.GetComponent<MouseDetect>().IncrementMana(mana);
                ResetCharacters(SC);
            }
            else
            {
                Debug.Log("GG");
            }
        }
        else
        {
            First.GetComponent<Animator>().SetTrigger("Player1");
            if (IC2)
            {
                mana = IC.GetComponent<Magician_N>().ManaMechanic();
                IC.GetComponent<MouseDetect>().IncrementMana(mana);
                ResetCharacters(SC2);
            }
            else
            {
                Debug.Log("GG");
            }
        }
        UnShowSpaces();
        turn++;
        ClearUI();
        spell = ""; //We can play with this as to add a warning before the player ends their turn ? for now it's set to this because by ending your turn no spell should be active at the start of the nex players turn
        option = "";
        if (currentObject != null)
        {
            currentObject.gameObject.GetComponent<ParticleSystem>().Stop();
        }
        currentObject = null;
        summon = null;
        TempHP = 0;

    }
    //Gives you the currentplayer based on turn
    public GameObject GetCurrentPlayer()
    {
        if(player == 0)
        {
            return IC;
        }
        return IC2;
    }
    public GameObject GetOtherPlayer()
    {
        if (player == 0)
        {
            return IC2;
        }
        return IC;
    }
    //Sets the right player
    public void SetCurrentPlayer()
    {
        if(player == 0)
        {
            currentObject = IC;
        }
        currentObject = IC2;
    }
    public Material XXX;
    //Sets what the currentObject is, as well as it's location
    public void SetObject(int ID, int Status, GameObject CO, GameObject COL)
    {
        if(currentObject != null)
        {
            currentObject.gameObject.GetComponent<ParticleSystem>().Stop();
        }
        currentObject = CO;
        currentObjectL = COL;
       // currentObject.GetComponent<ParticleSystemRenderer>().material = XXX; this is how to change material
        currentObject.gameObject.GetComponent<ParticleSystem>().Play();
    }
    public void MouseDown()
    {

    }
    public void ClearSpaces()
    {
        foreach (GameObject c in spaces)
        {
            //Debug.Log(c.gameObject.name + "is in the list");
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = false;
        }
         foreach (GameObject c in spaces)
        {
            ParticleSystem pr = c.GetComponent<ParticleSystem>();
            var main = pr.main;
                pr.Stop();
        }
        spaces.Clear();
    }
    public void ShowSpaces()
    {
        foreach (GameObject c in spaces)
        {
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = true;
        }
        foreach(GameObject c in spaces)
        {
            ParticleSystem pr = c.GetComponent<ParticleSystem>();
            var main = pr.main;
            main.startColor = new Color(0.0F, 250.0F, 0.0F, 1.0F);
            pr.Play();
        }
    }
    public void UnShowSpaces()
    {
        foreach(GameObject c in spaces)
        {
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = false;
        }
        foreach (GameObject c in spaces)
        {
            ParticleSystem pr = c.GetComponent<ParticleSystem>();
            var main = pr.main;          
            pr.Stop();
        }                          
        spaces.Clear();
    }
    public List<List<GameObject>> PossibleSpaces(GameObject p)
    {
        Transform[] children = p.GetComponentsInChildren<Transform>();
        List<List<GameObject>> l = new List<List<GameObject>>();
        for (int i = 0; i < x; i++)
        {
            l.Add(new List<GameObject>());
        }
        int k = 1;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                l[i].Add(children[k].gameObject);
                k++;
            }
        }
        return l;
    }
    //Returns the team the character belongs too
    public int TeamFinder(GameObject finding)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if(Coords[i][j].ID == finding.GetInstanceID())
                {
                    return Coords[i][j].Team;
                }
            }
        }
        return -1;
    }
    public GameObject FindCurrentObject(GameObject Loc)
    {
        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < x; j++)
            {
                if(Coords[i][j].location.gameObject == Loc.gameObject)
                {
                    return Coords[i][j].G;
                }
            }
        }
        return null;
    }
    public Coordinates FindCurrentLocation(GameObject Loc)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].G.gameObject == Loc.gameObject)
                {
                    return Coords[i][j];
                }
            }
        }
        return Coords[0][0]; //Just a random position
    }
    //MOVEMENT
    public List<GameObject> SpaceLocation(int r, int ID)
    {
        spaces.Clear();
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].ID == ID)
                {
                    return Spaces(i, j, r);
                }
            }
        }
        return null;
    }
    public List<GameObject> SpaceLocation(int r, GameObject L)
    {
        spaces.Clear();
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == L)
                {
                    return Spaces(i, j, r);
                }
            }
        }
        return null;
    }
    public List<GameObject> Spaces(int r, int y, int t)
    {
        spaces.Clear();
        spaces = new List<GameObject>();
        int n_row = r - t;
        int n_col = y - t;
        int o_range = 3 + (2 * (t - 1));
        for (int i = 0; i < o_range; i++)
        {
            for (int j = 0; j < o_range; j++)
            {
                if ((n_row >= 0 && n_row < x) && (n_col >= 0 && n_col < x))
                {
                    if (option == "move" || option == "summon")
                    {
                        if (Coords[n_row][n_col].status == 0)
                        {
                            spaces.Add(Coords[n_row][n_col].location);
                        }
                    } else if (option == "attack")
                    {
                        if (Coords[n_row][n_col].status == 1)
                        {
                            if (player == 0)
                            {
                                if (Coords[n_row][n_col].G.gameObject.layer == LayerMask.NameToLayer("Player2"))
                                {
                                    spaces.Add(Coords[n_row][n_col].location);
                                }
                            }
                            else
                            {
                                if (Coords[n_row][n_col].G.gameObject.layer == LayerMask.NameToLayer("Player1"))
                                {
                                    spaces.Add(Coords[n_row][n_col].location);
                                }
                            }
                        }
                    }
                    else if(option == "friendly")
                    {
                        if (Coords[n_row][n_col].status == 1)
                        {
                            if (player == 0)
                            {
                                if (Coords[n_row][n_col].G.gameObject.layer == LayerMask.NameToLayer("Player1"))
                                {
                                    spaces.Add(Coords[n_row][n_col].location);
                                }
                            }
                            else
                            {
                                if (Coords[n_row][n_col].G.gameObject.layer == LayerMask.NameToLayer("Player2"))
                                {
                                    spaces.Add(Coords[n_row][n_col].location);
                                }
                            }
                        }
                    }else if(option == "AllE")
                    {
                        if ((Coords[n_row][n_col].status == 1 || Coords[n_row][n_col].status == 0 ) && Coords[n_row][n_col].G != currentObject)
                        {
                            spaces.Add(Coords[n_row][n_col].location);
                        }
                    }
                    else if(option == "allE")
                    {
                        if (Coords[n_row][n_col].status == 1 && Coords[n_row][n_col].G != currentObject)
                        {
                            spaces.Add(Coords[n_row][n_col].location);
                        }
                    }
                    else if (option == "all")
                    {
                        if (Coords[n_row][n_col].status == 1)
                        {
                            spaces.Add(Coords[n_row][n_col].location);
                        }
                    }
                }
                n_col++;
            }
            n_row++;
            n_col = y - t;
        }
        return spaces;
    }
    public void AOE()
    {
        foreach (GameObject T in DDOL.instance.spaces)
        {
            for (int i = 0; i < DDOL.instance.x; i++)
            {
                for (int j = 0; j < DDOL.instance.x; j++)
                {
                    if (DDOL.instance.Coords[i][j].location == T)
                    {
                        if(DDOL.instance.spell == "GroupHealing")
                        {
                            DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().HealHP(5);
                        } else if (DDOL.instance.spell == "HolyFire")
                        {
                            DDOL.instance.Coords[i][j].G.GetComponent<MouseDetect>().DamageHP(2);
                        }
                    }
                }
            }
        }
        option = "";
        spell = "";
        DDOL.instance.currentObject.GetComponent<MouseDetect>().DiminishMana(currentCost);
        DDOL.instance.ClearSpaces();
    }
    public void SummonPawn(Transform new_p)
    {
        Vector3 vx = new Vector3(new_p.transform.position.x, new_p.transform.position.y - .40F, new_p.transform.position.z);
        int i_x = 0;
        int j_y = 0;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == new_p.gameObject || Coords[i][j].G == new_p.gameObject)
                {
                    if (player == 0)
                    {
                        summon.gameObject.layer = LayerMask.NameToLayer("Player1");
                    }
                    else
                    {
                        summon.gameObject.layer = LayerMask.NameToLayer("Player2");
                    }
                    i_x = i;
                    j_y = j;
                }
            }
        }
        ICS = (GameObject)Instantiate(summon, vx, new_p.transform.rotation);
        if (player == 0)
        {
            ICS.transform.parent = SC.transform;
        }
        else
        {
            ICS.transform.parent = SC2.transform;
            ICS.transform.Rotate(Vector3.up * 180f);
        }
        Coords[i_x][j_y] = new Coordinates(ICS.GetInstanceID(), 1, player, ICS, Coords[i_x][j_y].location);
        if (spell != "Swarm")//HERE IS A POINT WHERE THE COST IS DIMIINSHED BASED ON SWARM
        {
            currentObject.gameObject.GetComponent<MouseDetect>().DiminishMana(ICS.gameObject.GetComponent<MouseDetect>().Cost);
        }
        else
        {
            Coords[i_x][j_y].location.GetComponent<ParticleSystem>().Stop();
        }
    }
    public void MoveCharacter(Transform new_p)
    {
        //Find the collider of the intial position and set it to false
        float timeLerped = 0.0f;
        Transform startPos = currentObject.transform;
        ClearSpaces();
        while (timeLerped < 1.0)
        {
            timeLerped += Time.deltaTime;
            currentObject.transform.position = Vector3.MoveTowards(startPos.position, new Vector3(new_p.transform.position.x, currentObject.transform.position.y, new_p.transform.position.z), timeLerped);
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].ID == currentObject.GetInstanceID())
                {
                    Coords[i][j] = new Coordinates(-1, 0, -1, null, Coords[i][j].location);
                }
            }
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == new_p.gameObject)
                {
                    Coords[i][j] = new Coordinates(currentObject.GetInstanceID(), 1, player, currentObject, Coords[i][j].location);
                }
            }
        }
        source.PlayOneShot(moveSound, 0.7F);
        option = "";
    }
}
