using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Spielfigur : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator Player;
    float extraRotationSpeed = 2f;
    float distanceToTarget;
    Vector3 Ziel;
    float clicktime = 0;
    //float clickdelay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 1f;
        //agent.destination = new Vector3(0, 0, 0);
    }

    void extraRotation()
    {
        Vector3 lookrotation = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetBool("InBewegung"))
        {
            extraRotation();
        }
        if (Input.GetMouseButtonDown(0))
        {
            clicktime = Time.time;
            SetDestinationToMousePosition();
        }
        if (Time.time - clicktime > 5)
        {
            Player.SetBool("InBewegung", false);
        }
        CheckDestinationReached();


    }

    void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Ziel = hit.point;
            Player.SetBool("InBewegung", true);
            agent.SetDestination(hit.point);

        }
    }
    void CheckDestinationReached()
    {
        distanceToTarget = Vector3.Distance(transform.position, Ziel);
        if (distanceToTarget < 0.5f)
        {
            Player.SetBool("InBewegung", false);
            
        }
    }
}
