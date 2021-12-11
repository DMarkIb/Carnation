using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TischTrigger : MonoBehaviour
{
    public Camera cam1;
    public Camera maincam;
    public Animator Player;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 1f;
    private GameObject Spieler;
    bool doppelklick = false;
    bool Wimmelbild = false;

    // Start is called before the first frame update
    void Start()
    {
        Spieler = GameObject.Find("Spielfigur");
        cam1.GetComponent<Camera>().enabled = false;
        maincam.GetComponent<Camera>().enabled = true;

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
            Debug.Log("Double");
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
            if (Vector3.Distance(Spieler.transform.position, transform.position) <= 3f && !Wimmelbild)
            {
                Player.SetBool("Interaction", true);
                GameObject.Find("Spielfigur").GetComponent<Spielfigur>().enabled = false;

                StartCoroutine(waiter());

            }


            doppelklick = false;
        }
    }
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(2);
        GameObject.Find("Spielfigur").GetComponent<Spielfigur>().enabled = true;
        Debug.Log("Interaction");
        cam1.GetComponent<Camera>().enabled = true;
        maincam.GetComponent<Camera>().enabled = false;
        Spieler.SetActive(false);
        Wimmelbild = true;
        Player.SetBool("Interaction", false);

    }
}
