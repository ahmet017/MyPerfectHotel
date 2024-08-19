using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour
{
    public bool inCueWalk = true;
    public Customer customer;
    private Transform cueTargetPoint;
    
    private void Start()
    {
        cueTargetPoint = GameObject.Find("CueTargetPoint").transform;
        customer.anim.SetBool("Walk", true);

    }

    private void Update()
    {
        if (inCueWalk && !customer.gotoRoom)
        {
            Vector3 newPosition = Vector3.Lerp(customer.transform.position, cueTargetPoint.position, customer.inCueSpeed * Time.deltaTime);
            customer.transform.position = newPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            inCueWalk = false;
            customer.anim.SetBool("Walk", false);
        }

     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            print(other.gameObject.name + " Walk " + transform.parent.gameObject.name);
            //Time.timeScale = 0;
            inCueWalk = true;
            customer.anim.SetBool("Walk", true);
        }
    }
}
