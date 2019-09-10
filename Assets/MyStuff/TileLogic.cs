using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public string status = "normal";
    public bool isWalkable = true;
    public bool isOccupied = false;
    public bool isSelected = false;
    public GameObject character;
    public GameObject grid;
    public Color orgColour;

    // Start is called before the first frame update
    void Start()
    {
        TileUpdate();
    }

    void CheckIfOccupied()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(new Ray(transform.position, transform.up), out rayHit, 1))
        {
            isOccupied = true;
            character = rayHit.transform.gameObject;
        }
        else
        {
            isOccupied = false;
            character = null;
        }
    }

    public void TileUpdate()
    {
        if (status.Equals("fire"))
        {
            orgColour = Color.red;
        }
        else if (status.Equals("poison"))
        {
            orgColour = Color.magenta;
        }
        else
        {
            orgColour = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TileUpdate();
        CheckIfOccupied();
    }
}
