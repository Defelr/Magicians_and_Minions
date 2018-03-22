using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Data
{
    public int HP, DMG, MANA;
    public Data(int hp, int dmg, int mana)
    {
        HP = hp;
        DMG = dmg;
        MANA = mana;
    }
}
public struct Coordinates
{
    public int ID, status, Team;// 0 == empty, 1 == occupied, there might be more
    public GameObject G, location;
    public Data D;

    public Coordinates(int ID1, int S, int TEAM, GameObject G1, Data D1, GameObject L1)
    {
        Team = TEAM;
        ID = ID1;
        status = S;
        G = G1;
        location = L1;
        D = D1;
    }
}

public class DDOL : MonoBehaviour
{

    public AudioClip moveSound;
    private AudioSource source;

    public Camera currentCamera;
    public Camera First;
    public Camera Second;


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

    private Data player1_d = new Data(0,0,0);
    private Data player2_d;


    private int temp_x = -1, temp_y = -1;
    //FOR SUMMONING
    public GameObject summon;

    public string option = "";

    public GameObject SystemEvent;
    public void End_Turn()
    {
        if (turn % 2 == 0)
        {
            Second.enabled = true;
            First.enabled = false;
            currentCamera = Second;
        }
        else
        {
            Second.enabled = false;
            First.enabled = true;
            currentCamera = First;
        }
        DDOL.instance.turn++;
        ClearUI();

    }
    public void ClearUI()
    {
        SystemEvent.GetComponent<Switch_Canvas>().Clear();
    }

    public void Start()
    {
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
                Coord = new Coordinates(-1, 0, -1, null, player1_d, locations[i][j]);
                Coords[i].Add(Coord);
            }
        }
        StartingC.transform.localScale = new Vector3(1F, 1F, 1F);
        GameObject new_p = Coords[0][0].location;
        Vector3 vx = new Vector3(new_p.transform.position.x, 5.5F, new_p.transform.position.z);
        Instantiate(StartingC, vx, new_p.transform.rotation);
        StartingC.gameObject.layer = LayerMask.NameToLayer("Player1");
        player1_d = new Data(20, -1, 10); //HARD CODED
        Coords[0][0] = new Coordinates(StartingC.GetInstanceID(), 1, 0, StartingC, player1_d, Coords[0][0].location);

        StartingC2.transform.localScale = new Vector3(1F, 1F, 1F);
        new_p = Coords[x-1][x-1].location;
        vx = new Vector3(new_p.transform.position.x, 6.047379F, new_p.transform.position.z);
        Instantiate(StartingC2, vx, new_p.transform.rotation);
        StartingC2.gameObject.layer = LayerMask.NameToLayer("Player2");
        player2_d = new Data(20, -1, 10); //HARD CODED
        Coords[x-1][x-1] = new Coordinates(StartingC2.GetInstanceID(), 1, 1, StartingC2, player2_d, Coords[x-1][x-1].location);
    }
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
    public Coordinates SetObject(int ID, int Status, Data d, GameObject CO, GameObject COL)
    {

        currentObject = CO;
        currentObjectL = COL;
        Coord = new Coordinates(ID, Status, player, CO, d, COL);
        return Coord;
    }
    public void MouseDown()
    {
        if (currentObject != null)
        {
            ClearSpaces();
        }
    }
    public void ClearSpaces()
    {
        foreach (GameObject c in spaces)
        {
            //Debug.Log(c.gameObject.name + "is in the list");
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = false;
        }
    }
    public void ShowSpaces()
    {
        foreach (GameObject c in spaces)
        {
            Renderer R = c.GetComponent<Renderer>();
            R.enabled = true;
        }
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
        for (int i = 0; i < DDOL.instance.x; i++)
        {
            for (int j = 0; j < DDOL.instance.x; j++)
            {
                if(Coords[i][j].ID == finding.GetInstanceID())
                {
                    return Coords[i][j].Team;
                }
            }
        }
        return -1;
    }
    //MOVEMENT
    public List<GameObject> SpaceLocation(int r, int ID)
    {
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
    private List<GameObject> Spaces(int r, int y, int t)
    {
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
                            Debug.Log(Coords[n_row][n_col].location);
                            spaces.Add(Coords[n_row][n_col].location);
                        }
                    }else if (option == "attack")
                    {
                        if(Coords[n_row][n_col].status == 1)
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
                }
                n_col++;
            }
            n_row++;
            n_col = y - t;
        }
        Debug.Log("HI" + spaces.Count);
        return spaces;
    }
    public void SetLocationTemp(int obj)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].ID == obj)
                {
                    temp_x = i;
                    temp_y = j;
                    return;
                }
            }
        }
    }
    public void SummonPawn(Transform new_p)
    {
        if (player == 0)
        {
            SetLocationTemp(StartingC.gameObject.GetInstanceID());
        }
        else
        {
            SetLocationTemp(StartingC2.gameObject.GetInstanceID());
        }
        Debug.Log(StartingC.gameObject.name);
        Debug.Log(StartingC2.gameObject.name);
        Vector3 vx;
        Data minionD = new Data(0, 0, 0);
        Data MagicianM = new Data(0, 0, 0);
       
            if (summon.gameObject.name == "wraith")
            {
                vx = new Vector3(new_p.transform.position.x, 6.48F, new_p.transform.position.z);
                minionD = new Data(4, 2, -1);//TEMP DATA
               // MagicianM = new Data(Coords[temp_x][temp_y].D.HP, Coords[temp_x][temp_y].D.DMG, Coords[temp_x][temp_y].D.MANA - 3);
                //   Coords[temp_x][temp_y] = new Coordinates(Coords[temp_x][temp_y].ID, 1, Coords[temp_x][temp_y].G, MagicianM, Coords[temp_x][temp_y].location);//3 is the cost of the minion
            }
            else
            {
                vx = new Vector3(new_p.transform.position.x, new_p.transform.position.y, new_p.transform.position.z);
                minionD = new Data(2, 1, -1);//TEMP DATA
                //MagicianM = new Data(Coords[temp_x][temp_y].D.HP, Coords[temp_x][temp_y].D.DMG, Coords[temp_x][temp_y].D.MANA - 1);
                // Coords[temp_x][temp_y] = new Coordinates(Coords[temp_x][temp_y].ID, 1, Coords[temp_x][temp_y].G, MagicianM, Coords[temp_x][temp_y].location);//1 is the cost of the minion
            }
       Debug.Log(temp_y);
       Debug.Log(temp_x);
        summon.transform.localScale = new Vector3(10F, 10F, 10F);

        Instantiate(summon, vx, new_p.transform.rotation);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == new_p.gameObject)
                {
                    if (turn % 2 == 0)
                    {
                        summon.gameObject.layer = LayerMask.NameToLayer("Player1");
                    }
                    else
                    {
                        summon.gameObject.layer = LayerMask.NameToLayer("Player2");
                    }
                    Coords[i][j] = new Coordinates(summon.GetInstanceID(), 1, player, summon, minionD, Coords[i][j].location);
                }
            }
        }
        ClearSpaces();
        return;
    }
    public void MoveCharacter(Transform new_p)
    {
        //Find the collider of the intial position and set it to false
        float timeLerped = 0.0f;
        Transform startPos = currentObject.transform;
        Data temp_d = new Data(0, 0, 0);
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
                    temp_d = Coords[i][j].D;
                    Coords[i][j] = new Coordinates(-1, 0, -1, null, new Data(0,0,0), Coords[i][j].location);
                }
            }
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == new_p.gameObject)
                {
                    Coords[i][j] = new Coordinates(currentObject.GetInstanceID(), 1, player, currentObject, temp_d, Coords[i][j].location);
                }
            }
        }
        source.PlayOneShot(moveSound, 0.7F);
        Collider col = currentObjectL.GetComponent<Collider>();
        Collider colp = currentObject.GetComponent<Collider>();
        colp.isTrigger = true;
        col.isTrigger = false;
        spaces.Clear();
    }
}
