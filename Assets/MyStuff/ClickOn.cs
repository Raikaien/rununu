using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//click logic from here: 
//https://www.youtube.com/watch?v=OCGoTiV4kbM
//https://www.youtube.com/watch?v=iJs9j-zMSQo
//https://www.youtube.com/watch?v=23R3l8KQnts
public class ClickOn : MonoBehaviour
{
    [SerializeField]
    private Material red, blue, select;
    private Renderer myRend;
    public bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<Renderer>();
        ClickMe();
    }

    public void ClickMe()
    {
        if (isSelected == false)
        {
            if (this.tag == "Ally")
            {
                myRend.material = blue;
            }
            else
            {
                myRend.material = red;
            }
        }
        else
        {
            myRend.material = select;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
