using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLogic : Photon.MonoBehaviour
{
    public GameObject tile;
    public GameObject[,] grid;

    //public GameObject enemy1;
    //public GameObject enemy2;
    //public GameObject enemy3;
    //public GameObject enemy4;
    //public GameObject ally1;
    //public GameObject ally2;


    private int col = 6;
    private int row = 14;

    [SerializeField]
    private float size = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[col, row];

        for(int i = 0; i < col; i++)
        {
            for(int j = 0; j < row; j++)
            {
                grid[i,j] = Instantiate(tile, new Vector3(i, 0, j), tile.transform.rotation);
            }
        }

        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
        Debug.Log("Joined lobby");
    }

    void OnJoinedRoom()
    {
        //if (PhotonNetwork.room.PlayerCount == 1)
        if (PhotonNetwork.isMasterClient)
        {
            //enemy1.SetActive(false);
            //enemy2.SetActive(false);
            //enemy3.SetActive(false);
            //enemy4.SetActive(false);
            //ally1.SetActive(false);
            //ally2.SetActive(false);
            PhotonNetwork.Instantiate("enemy", new Vector3(1, 1.1f, 1), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("enemy", new Vector3(2, 1.1f, 1), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("enemy", new Vector3(3, 1.1f, 1), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("enemy", new Vector3(4, 1.1f, 1), Quaternion.identity, 0);
            
        }
        else 
        {
            PhotonNetwork.Instantiate("ally", new Vector3(1, 1.1f, 12), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("ally2", new Vector3(2, 1.1f, 12), Quaternion.identity, 0);
        }
        Debug.Log("Joined room: " + PhotonNetwork.room.Name);
    }

    //snapping to grid logic from here: https://www.youtube.com/watch?v=VBZFYGWvm4A
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3((float)xCount * size, (float)yCount * size, (float)zCount * size);

        result += transform.position;

        return result;
    }

    //TODO: get it to check the character's movement
    //adjacent detection from here: https://www.youtube.com/watch?v=6QTB2wP3JXE
    public void HighlightMovement(GameObject[,] grid, int x, int z, int move, Color colour)
    {

        int maxX = x + (move - 1);
        int minX = x - (move - 1);
        int maxZ = z + (move - 1);
        int minZ = z - (move - 1);

        if (maxX > 5)
        {
            maxX = 5;
        }
        if (minX < 0)
        {
            minX = 0;
        }
        if (maxZ > 13)
        {
            maxZ = 13;
        }
        if (minZ < 0)
        {
            minZ = 0;
        }

        //adjacent highlights
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minZ; j <= maxZ; j++)
            {
                grid[i, j].GetComponent<Renderer>().material.color = colour;
                //TODO: FIX THIS
                //below highlight currently only works for move <= 2
                if (x + move <= 5)
                {
                    grid[x + move, z].GetComponent<Renderer>().material.color = colour;
                }
                if (z + move <= 13)
                {
                    grid[x, z + move].GetComponent<Renderer>().material.color = colour;
                }
                if (x - move >= 0)
                {
                    grid[x - move, z].GetComponent<Renderer>().material.color = colour;
                }
                if (z - move >= 0)
                {
                    grid[x, z - move].GetComponent<Renderer>().material.color = colour;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //grid[5, 5].GetComponent<TileLogic>().status = "poison";
    }
}
