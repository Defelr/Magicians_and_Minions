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

    public GameObject SystemEvent;

    //FOR BlockChoice
    public Material G_Color;
    public Material Gr_Color;

    public void End_Turn()
    {
        if (turn % 2 == 0)
        {
            Second.enabled = true;
            First.enabled = false;
            currentCamera = Second;
            if (IC)
            {
                IC.GetComponent<Magician_N>().ManaMechanic();
                ResetCharacters(SC);
            }
            else
            {
                Debug.Log("GG");
            }
        }
        else
        {
            Second.enabled = false;
            First.enabled = true;
            currentCamera = First;
            if (IC2)
            {
                IC2.GetComponent<Magician_N>().ManaMechanic();
                ResetCharacters(SC2);
            }
            else
            {
                Debug.Log("GG");
            }
        }
        DDOL.instance.turn++;
        ClearUI();

    }
    public void ResetCharacters(Transform P) { 
        foreach(Transform T in P)
        {
            T.gameObject.GetComponent<MouseDetect>().ResetV();
        }
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
                Coord = new Coordinates(-1, 0, -1, null, locations[i][j]);
                Coords[i].Add(Coord);
            }
        }
        StartingC.transform.localScale = new Vector3(1F, 1F, 1F);
        GameObject new_p = Coords[0][0].location;
        Vector3 vx = new Vector3(new_p.transform.position.x, 5.5F, new_p.transform.position.z);
        IC = (GameObject)Instantiate(StartingC, vx, new_p.transform.rotation);
        StartingC.gameObject.layer = LayerMask.NameToLayer("Player1");
        Coords[0][0] = new Coordinates(IC.GetInstanceID(), 1, 0, IC, Coords[0][0].location);
        IC.transform.parent = SC.gameObject.transform;

        StartingC2.transform.localScale = new Vector3(1F, 1F, 1F);
        new_p = Coords[x-1][x-1].location;
        vx = new Vector3(new_p.transform.position.x, 6.047379F, new_p.transform.position.z);
        IC2 = (GameObject)Instantiate(StartingC2, vx, new_p.transform.rotation);
        StartingC2.gameObject.layer = LayerMask.NameToLayer("Player2");
        Coords[x-1][x-1] = new Coordinates(IC2.GetInstanceID(), 1, 1, IC2, Coords[x-1][x-1].location);
        IC2.transform.parent = SC2.gameObject.transform;
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
    public void SetObject(int ID, int Status, GameObject CO, GameObject COL)
    {

        currentObject = CO;
        currentObjectL = COL;
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
        spaces.Clear();
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
        Vector3 vx = new Vector3(new_p.transform.position.x, new_p.transform.position.y, new_p.transform.position.z);

        if (summon.gameObject.tag == "Wraith")
        {
            vx = new Vector3(new_p.transform.position.x, 6.48F, new_p.transform.position.z);
            //summon.transform.localScale = new Vector3(10F, 10F, 10F);
        }
        else if (summon.gameObject.tag == "Skeleton")
        {
            vx = new Vector3(new_p.transform.position.x, new_p.transform.position.y-.45F, new_p.transform.position.z);
            //summon.transform.localScale = new Vector3(10F, 10F, 10F)
        }
        else if (summon.gameObject.tag == "GreatSpirit")
        {
            vx = new Vector3(new_p.transform.position.x, 6.019F, new_p.transform.position.z);
        }
       Debug.Log(temp_y);
       Debug.Log(temp_x);

        GameObject ICS = (GameObject)Instantiate(summon, vx, new_p.transform.rotation);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == new_p.gameObject || Coords[i][j].G == new_p.gameObject)
                {
                    if (player == 0)
                    {
                        ICS.gameObject.layer = LayerMask.NameToLayer("Player1");
                        ICS.transform.parent = SC.transform;
                    }
                    else
                    {
                        ICS.gameObject.layer = LayerMask.NameToLayer("Player2");
                        ICS.transform.parent = SC2.transform;
                    }
                    Coords[i][j] = new Coordinates(ICS.GetInstanceID(), 1, player, ICS, Coords[i][j].location);
                }
            }
        }
        currentObject.gameObject.GetComponent<MouseDetect>().DiminishMana(ICS.gameObject.GetComponent<MouseDetect>().Cost);
        ClearSpaces();
        summon = null;
        return;
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
        Collider col = currentObjectL.GetComponent<Collider>();
        Collider colp = currentObject.GetComponent<Collider>();
        colp.isTrigger = true;
        col.isTrigger = false;
    }
}
