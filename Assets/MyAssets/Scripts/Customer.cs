using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public Animator anim;
    public float inCueSpeed, bedAndToiletTime;
    [HideInInspector] public bool gotoRoom;
    private bool gotoBed, gotoToilet;
    private Room roomManager;
    public NavMeshAgent agent;
    private float reachThreshold; // Adjust this threshold to your needs
    private Transform exitPoint;
    public bool inCue;
    public BoxCollider cueCollider;

    private void Start()
    {
        //Loading();
        anim.SetBool("Walk", true);
        exitPoint = GameObject.Find("ExitPoint").transform;
    }

    //private void Loading()
    //{
    //    anim.Rebind();
    //    ReachedDestination();    
    //}

    public void GotoRoom(Room room)
    {
        Destroy(cueCollider);

        gotoRoom = true;

        roomManager = room;
        agent.enabled = true;
        agent.SetDestination(room.roomBedPoint.position);
        inCue = false;
        anim.SetBool("Walk", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Desk") && !gotoRoom)
        //{
        //    //GetComponentInChildren<CueManager>().inCueWalk = false;
        //    anim.SetBool("Walk", false);
        //}

        if (other.CompareTag("Toilet") && gotoToilet)
        {
            gotoToilet = false;
            other.GetComponentInChildren<MoneyCollector>().AddMoney(transform.position);
        }

        if (other.CompareTag("Exit"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!agent.enabled)
            return;

        if (inCue)
        {
            StartCoroutine(Check1());
        }

        if (!inCue)
        {
            StartCoroutine(Check());
        }
    }

    private IEnumerator Check1()
    {
        if (ReachedDestination())
        {
            anim.SetBool("Walk", false);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        yield return null;
    }

    private IEnumerator Check()
    {
        if (ReachedDestination())
            OnPathEndReached();

        yield return null;
    }

    private bool ReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnPathEndReached()
    {
        print("On End Reached");

        if (gotoBed)
            OnToiletReached();
        else
            OnBedReached();     
    }

    private void OnBedReached()
    {
        agent.enabled = false;

        gotoBed = true;
        print("On Bed Reached");
        transform.eulerAngles = roomManager.roomBedPoint.eulerAngles;
        anim.SetBool("ToBed", true);
        anim.SetBool("Walk", false);

        roomManager.EnterRoom();

        Invoke(nameof(BedTime), bedAndToiletTime);
    }

    private void BedTime()
    {
        roomManager.ExitRoom();
        agent.enabled = true;

        agent.SetDestination(roomManager.toiletPoint.position);

        gotoToilet = true;

        anim.SetBool("Walk", true);
        anim.SetBool("ToBed", false);
    }

    private void OnToiletReached()
    {
        anim.SetBool("Walk", false);

        Invoke(nameof(ToiletTime), bedAndToiletTime);
    }

    private void ToiletTime()
    {
        agent.SetDestination(exitPoint.position);
        anim.SetBool("Walk", true);
    }
}
