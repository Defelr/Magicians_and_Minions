using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Coordinates
{
    public int ID, status;// 0 == empty, 1 == occupied, there might be more
    public GameObject G, location;

    public Coordinates(int ID1, int S, GameObject G1, GameObject L1)
    {
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

    public static DDOL instance = null;
    public int turn = 0;
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



    //FOR SUMMONING
    public GameObject summon;

    public string option = "";

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
                Coord = new Coordinates(-1, 0, null, locations[i][j]);
                Coords[i].Add(Coord);
            }
        }
        StartingC.transform.localScale = new Vector3(1F, 1F, 1F);
        GameObject new_p = Coords[0][0].location;
        Vector3 vx = new Vector3(new_p.transform.position.x, 5.5F, new_p.transform.position.z);
        Instantiate(StartingC, vx, new_p.transform.rotation);
    }
    public void Awake()
    {
        source = GetComponent<AudioSource>();
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }
    public Coordinates SetObject(int ID, int Status, GameObject CO, GameObject COL)
    {

        currentObject = CO;
        currentObjectL = COL;
        Coord = new Coordinates(ID, Status, CO, COL);
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
                    if (Coords[n_row][n_col].status == 0)
                    {
                        Debug.Log(Coords[n_row][n_col].location);
                        spaces.Add(Coords[n_row][n_col].location);
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
    public void SummonPawn(Transform new_p)
    {
        Vector3 vx;
        if (summon.gameObject.name == "wraith")
        {
            vx = new Vector3(new_p.transform.position.x, 6.48F, new_p.transform.position.z);
        }
        else
        {
            vx = new Vector3(new_p.transform.position.x, new_p.transform.position.y, new_p.transform.position.z);
        }
        summon.transform.localScale = new Vector3(10F, 10F, 10F);

        Instantiate(summon, vx, new_p.transform.rotation);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (Coords[i][j].location == new_p.gameObject)
                {
                    Coords[i][j] = new Coordinates(summon.GetInstanceID(), 1, summon, Coords[i][j].location);
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
                    Coords[i][j] = new Coordinates(-1, 0, null, Coords[i][j].location);
                }
                if (Coords[i][j].location == new_p.gameObject)
                {
                    Coords[i][j] = new Coordinates(currentObject.GetInstanceID(), 1, currentObject, Coords[i][j].location);
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
