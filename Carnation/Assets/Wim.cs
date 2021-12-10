using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wim : MonoBehaviour
{
    public Camera cam1;
    public Camera maincam;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 1f;
    private GameObject Spieler;
    bool doppelklick = false;
    bool Fertig = false;

    // Start is called before the first frame update
    void Start()
    {
        Spieler = GameObject.Find("Spielfigur");
        
    }

    public void OnMouseDown()
    {
        clicked++;
        clicktime = Time.time;
        Debug.Log("Click");
    }


    // Update is called once per frame
    void Update()
    {
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Debug.Log("Boob");
            doppelklick = true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
        {
            clicked = 0;
            //Debug.Log("Nope");
            doppelklick = false;
        }

        if (doppelklick)
        {
            //Vorher spielen, wenn alles erledigt ist "Fertig" auf true stellen 

            Fertig = true;

            if (Fertig)
            {
                cam1.GetComponent<Camera>().enabled = false;
                maincam.GetComponent<Camera>().enabled = true;
                Spieler.SetActive(true);
                GameObject.Find("Spielfigur").GetComponent<Spielfigur>().enabled = true;

            }

            doppelklick = false;
        }
    }
}
