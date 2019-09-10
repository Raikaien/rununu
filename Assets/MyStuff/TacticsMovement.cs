//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////tile movement and range from here https://www.youtube.com/watch?v=cK2wzBCh9cg
//public class TacticsMovement : MonoBehaviour
//{
//    List<TileLogic> selectableTiles = new List<TileLogic>();
//    Stack<TileLogic> path = new Stack<TileLogic>();

//    public float height = 2;
//    float halfHeight = 0;
//    float move = 2;

//    Vector3 velocity = new Vector3();
//    Vector3 heading = new Vector3();

//    GameObject[] tiles;
//    TileLogic currentTile;


//    // Start is called before the first frame update
//    void Start()
//    {
//        tile = GameObject.FindGameObjectWithTag("Tile");
//        halfHeight = GetComponent<Collider>().bounds.extents.y;
//    }

//    public void GetCurrentTile()
//    {
//        currentTile = GetTargetTile(gameObject);
//        currentTile.isSelected = true;
//    }

//    public TileLogic GetTargetTile(GameObject target)
//    {
//        RaycastHit rayHit;
//        TileLogic tile = null;

//        if (Physics.Raycast(target.transform.position, Vector3.down, out rayHit, 1))
//        {
//            tile = rayHit.collider.GetComponent<TileLogic>();
//        }

//        return tile;
//    }

//    public void ComputeAdjacenyList()
//    {
//        foreach (GameObject tile in tiles)
//        {
//            TileLogic t = tile.GetComponent<TileLogic>();
//            t.FindNeighbours(height);
//        }
//    }

//    //do BFS stuff
//    public void FindSelectableTiles()
//    {
//        ComputeAdjacenyList();
//        GetCurrentTile();

//        Queue<TileLogic> process = new Queue<TileLogic>();

//        process.Enqueue(currentTile);
//        currentTile.visited = true;

//        while (process.Count > 0)
//        {
//            TileLogic t = process.Dequeue();

//            selectableTiles.Add(t);
//            t.selectable = true;

//            if (t.distance < move)
//            {
//                foreach (TileLogic tile in t.adjacenyList)
//                {
//                    if (!tile.visited)
//                    {
//                        tile.parent = t;
//                        tile.visited = true;
//                        tile.distance = 1 + t.distance;
//                        process.Enqueue(tile);
//                    }
//                }
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
