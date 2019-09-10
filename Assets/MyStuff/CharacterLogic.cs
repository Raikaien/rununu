using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
    public bool turnEnd = false;

    public string charName;
    public int id;
    public int level;
    public int expPoints;
    public int attack;
    public int health;
    public int speed;
    public int stability;
    public int move;
    public string status;
    public string charClass;
    public GameObject primaryGear;
    public GameObject secondaryGear;
    public GameObject attire;
    public GameObject keystone;
    public GameObject target;
    public GameObject tile;
    public bool hasMoved = false;
    public bool hasAttacked = false;
    public bool isTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(primaryGear, primaryGear.transform.position, primaryGear.transform.rotation);

        charName = "Marcus the Righteous Fist";
        id = 1;
        level = 1;
        expPoints = 0;
        attack = 5;
        health = 30;
        speed = 1;
        stability = 5;
        move = 2;
        status = "normal";
        charClass = "melee";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            tile = collision.gameObject;
        }
    }

    public bool WithinRange(GameObject target)
    {
        if (charClass.Equals("melee"))
        {
            //adjacent enemies can be hit by melee attacks
            if (Vector3.Distance(this.transform.position, target.transform.position) <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else if (charClass.Equals("ranged"))
        {
            //enemies within 2 spaces can be hit by ranged attacks
            if ((Vector3.Distance(this.transform.position, target.transform.position)) <= 2 && ((Vector3.Distance(this.transform.position, target.transform.position) > 1.1)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public int Attack(GameObject target)
    {
        int damage = 0;
        if (WithinRange(target))
        {
            damage = attack + primaryGear.GetComponent<PrimaryLogic>().attack;
            target.GetComponent<CharacterLogic>().health -= damage;
            
            if(target.GetComponent<CharacterLogic>().health <= 0)
            {
                expPoints += 30;
                if(expPoints >= 100)
                {
                    LevelUp();
                }
            }
        }
        turnEnd = true;
        return damage;
    }
    
    public void UsePrimary(GameObject target)
    {

    }

    public void UseSecondary(GameObject target)
    {

    }

    public void Wait()
    {
        turnEnd = true;
    }

    public void LevelUp()
    {
        expPoints -= 100;
        level++;
        attack += 2;
        health += 5;
        speed += 1;
        stability += 2;
    }

    public void CharacterUpdate()
    {
        if(health <= 0)
        {
            Death();
        }

        if(tile.GetComponent<TileLogic>().status == "fire")
        {
            if (stability > 0)
            {
                stability--;
            }
            else
            {
                status = "burn";
            }   
        }

        if (tile.GetComponent<TileLogic>().status == "poison")
        {
            if (stability > 0)
            {
                stability--;
            }
            else
            {
                status = "poison";
            }
        }

        if (status == "burn" || status == "poison")
        {
            health--;
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CharacterUpdate();
    }
}
