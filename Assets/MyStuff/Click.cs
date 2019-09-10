using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//click logic from here: 
//https://www.youtube.com/watch?v=OCGoTiV4kbM
//https://www.youtube.com/watch?v=iJs9j-zMSQo
//https://www.youtube.com/watch?v=23R3l8KQnts
public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;

    public GridLogic grid;
    public ClickOn clickOnScript;
    public Vector3 initialPosition;
    public Vector3 finalPosition;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridLogic>();
    }

    private void MoveChar(Vector3 clickPoint)
    {
        initialPosition = clickOnScript.transform.position;
        finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        finalPosition.y += 1;

        //so long as moving distance is not more than the distance between the vectors the character can move
        if (Vector3.Distance(initialPosition, finalPosition) <= clickOnScript.GetComponent<CharacterLogic>().move + 0.1)
        {
            clickOnScript.transform.position = finalPosition;
        }
        else
        {
            Debug.Log("Out of range." + Vector3.Distance(initialPosition, finalPosition));
        }
    }

    public void MoveUpdate()
    {
        if (TurnLogic.instance.currentCharacter != null)
        {
            clickOnScript = TurnLogic.instance.currentCharacter.GetComponent<ClickOn>();

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit rayHit;
                CharacterLogic character;

                //if rayhit hits an object
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer))
                {
                    if (rayHit.transform.GetComponent<CharacterLogic>() != null)
                    {
                        string enemy;
                        if (TurnLogic.instance.currentCharacter.transform.tag != "Enemy")
                        {
                            enemy = "Enemy";
                        }
                        else
                        {
                            enemy = "Ally";

                        }

                        if (rayHit.transform.gameObject == TurnLogic.instance.currentCharacter)
                        {
                            character = clickOnScript.gameObject.GetComponent<CharacterLogic>();
                            //toggle on/off select
                            clickOnScript.isSelected = !clickOnScript.isSelected;
                            clickOnScript.ClickMe();

                            //highlight movement options
                            if (clickOnScript.isSelected)
                            {
                                grid.HighlightMovement(grid.grid, Mathf.RoundToInt(clickOnScript.transform.position.x), Mathf.RoundToInt(clickOnScript.transform.position.z), clickOnScript.GetComponent<CharacterLogic>().move, Color.cyan);
                            }
                            else
                            {
                                grid.HighlightMovement(grid.grid, Mathf.RoundToInt(clickOnScript.transform.position.x), Mathf.RoundToInt(clickOnScript.transform.position.z), clickOnScript.GetComponent<CharacterLogic>().move, Color.white);
                            }
                        }

                        if (clickOnScript.isSelected && (rayHit.transform.tag.Equals(enemy)))
                        {
                            if (clickOnScript.GetComponent<CharacterLogic>().WithinRange(rayHit.collider.gameObject) && !TurnLogic.instance.currentCharacter.GetComponent<CharacterLogic>().hasAttacked)
                            {
                                Debug.Log(clickOnScript.GetComponent<CharacterLogic>().Attack(rayHit.collider.gameObject));
                                TurnLogic.instance.currentCharacter.GetComponent<CharacterLogic>().hasAttacked = true;
                                grid.HighlightMovement(grid.grid, Mathf.RoundToInt(clickOnScript.transform.position.x), Mathf.RoundToInt(clickOnScript.transform.position.z), clickOnScript.GetComponent<CharacterLogic>().move, Color.white);
                                clickOnScript.isSelected = false;
                                clickOnScript.ClickMe();
                            }
                            //otherwise deselect
                            else
                            {
                                grid.HighlightMovement(grid.grid, Mathf.RoundToInt(clickOnScript.transform.position.x), Mathf.RoundToInt(clickOnScript.transform.position.z), clickOnScript.GetComponent<CharacterLogic>().move, Color.white);
                                clickOnScript.isSelected = false;
                                clickOnScript.ClickMe();
                            }
                        }
                        else if (rayHit.transform.tag.Equals(enemy))
                        {
                        }
                    }
                }

                //if rayhit doesnt hit an object
                else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit) && !TurnLogic.instance.currentCharacter.GetComponent<CharacterLogic>().hasMoved)
                {
                    //if selected tile not occupied, un-highlight movement options and move character to clicked spot
                    if ((rayHit.transform.tag != "Ally" || rayHit.transform.tag != "Enemy") && clickOnScript.isSelected)
                    {
                        grid.HighlightMovement(grid.grid, Mathf.RoundToInt(clickOnScript.transform.position.x), Mathf.RoundToInt(clickOnScript.transform.position.z), clickOnScript.GetComponent<CharacterLogic>().move, Color.white);
                        MoveChar(rayHit.point);
                        //TurnLogic.instance.currentCharacter.GetComponent<CharacterLogic>().hasMoved = true;
                        clickOnScript.isSelected = false;
                        clickOnScript.ClickMe();
                        clickOnScript.GetComponent<CharacterLogic>().turnEnd = true;
                    }
                }
            }

            if (TurnLogic.instance.currentCharacter.GetComponent<CharacterLogic>().hasAttacked)
            {
                TurnLogic.instance.endTurn();
            }
        }
        
        

        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
    }
}
