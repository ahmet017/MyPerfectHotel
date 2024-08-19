using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject[] customerPrefabs;
    public Transform[] cuePoints;
    public List<Customer> customers;

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 1, 3);     
    }

    public void SpawnCustomer()
    {
        GameObject customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

        GameObject customer = Instantiate(customerPrefab, transform.position, transform.rotation);
        customer.name += spawnCount;

        customers.Add(customer.GetComponent<Customer>());
        ArrangeCustomersInQue();

        spawnCount--;

        if (spawnCount == 0)
            CancelInvoke("SpawnCustomer");
    }

    public void ArrangeCustomersInQue()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            //    if (customersForBilling[i].target == null || customersForBilling[i].target != billingQue[i])
            //    {
            customers[i].agent.SetDestination(cuePoints[i].position);
            //}
        }
    }
}
