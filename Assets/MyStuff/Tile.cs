//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Tile : MonoBehaviour
//{
//    //BFS stuff
//    public List<TileLogic> adjacenyList = new List<TileLogic>();
//    public bool visited = false;
//    public TileLogic parent = null;
//    public int distance = 0;
//    public bool selectable = true;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    public void Reset()
//    {
//        adjacenyList.Clear();

//        isWalkable = true;
//        isOccupied = false;
//        isSelected = false;
//        selectable = true;
//        visited = false;
//        parent = null;
//        distance = 0;
//    }

//    public void FindNeighbours(float height)
//    {
//        Reset();

//        CheckTile(Vector3.forward, height);
//        CheckTile(Vector3.back, height);
//        CheckTile(Vector3.right, height);
//        CheckTile(Vector3.left, height);
//    }

//    public void CheckTile(Vector3 direction, float height)
//    {
//        Vector3 halfExtents = new Vector3(0.25f, (1 + height) / 2.0f, 0.25f);
//        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

//        foreach (Collider item in colliders)
//        {
//            TileLogic tile = item.GetComponent<TileLogic>();
//            if (tile != null & tile.isWalkable)
//            {
//                RaycastHit rayHit;

//                if (!Physics.Raycast(tile.transform.position, Vector3.up, out rayHit, 1))
//                {
//                    adjacenyList.Add(tile);
//                }
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
