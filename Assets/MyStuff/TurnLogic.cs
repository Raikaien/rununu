using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    public List<GameObject> timeline = new List<GameObject>();
    public int current = 0;
    public static TurnLogic instance;
    public GameObject winUI;
    public GameObject gridBuilder;
    public int playerTurn = 1;
    public int enemyTurn = 1;
    public bool newTurn = true;
    public GameObject currentCharacter;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateTimeline()
    {
        //clean out list for repopulation and resize
        timeline.Clear();
        timeline = new List<GameObject>();

        //grab all entities
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (gridBuilder.GetComponent<GridLogic>().grid[i, j].GetComponent<TileLogic>().isOccupied)
                {
                    //fixed the check
                    if (gridBuilder.GetComponent<GridLogic>().grid[i, j].GetComponent<TileLogic>().character != null)
                    {
                        timeline.Add(gridBuilder.GetComponent<GridLogic>().grid[i, j].GetComponent<TileLogic>().character);
                    }
                }
            }
        }

        //sort based on speed
        if (timeline.Count > 0)
        {
            timeline = timeline.OrderBy(c => c.GetComponent<CharacterLogic>().speed).ToList();
            
        }

        //if no more enemies, you win; display win panel
        //doesnt work rn
        //int enemyCounter = 0;
        //for (int i = 0; i < timeline.Count; i++)
        //{
        //    if (timeline[i].tag == "Enemy")
        //    {
        //        enemyCounter++;
        //    }
        //}
        //if(enemyCounter == 0)
        //{
        //    winUI.SetActive(true);
        //}
    }

    //TODO: add events/delegates system for turn start and end efefcts 


    public void StartRound()
    {
        //Debug.Log(timeline.Count());
        if (newTurn)
        {
            timeline[current].GetComponent<CharacterLogic>().isTurn = true;
            currentCharacter = timeline[current];
            Debug.Log(timeline[current].name + "'s Turn Begin!");
            newTurn = false;
        }
    }
    
    public void RoundEnd()
    {
        if (current == timeline.Count)
        {
            current = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentCharacter.GetComponent<CharacterLogic>().hasAttacked = true;
        }
        UpdateTimeline();

        StartRound();

        RoundEnd();
    }

    public void endTurn()
    {
        timeline[current].GetComponent<CharacterLogic>().isTurn = false;
        currentCharacter.GetComponent<CharacterLogic>().hasMoved = false;
        currentCharacter.GetComponent<CharacterLogic>().hasAttacked = false;
        current++;
        newTurn = true;
        RoundEnd();
    }
}
