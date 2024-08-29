using System.Collections;
using UnityEngine;

public class Money : MonoBehaviour
{
    private Transform player;
    private GameManager gameManager;
    private bool gotoPlayer, gotoMoneyCollecter;
    public float moveSpeed; // Adjust this speed as needed.
    private float destroyDistanceThreshold = 0.1f; // Distance threshold for destruction.
    private Vector3 targetPos;
    private Vector3 moneyCollecterTargetPos;
    private Vector3 moneyStackPos;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>().transform;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GotoPlayer()
    {
        gotoPlayer = true;
    }

    public void GotoMoneyCollecter(Vector3 vec, Vector3 _moneyStackPos)
    {
        gotoMoneyCollecter = true;
        moneyCollecterTargetPos = vec;
        moneyStackPos = _moneyStackPos;
    }

    private void Update()
    {
        if (gotoPlayer)
        {
            gotoPlayer = false;
            StartCoroutine(GotoPlayerCoroutine());
        }

        if (gotoMoneyCollecter && !gotoPlayer)
        {
            gotoMoneyCollecter = false;
            StartCoroutine(GotoMoneyCollecterCoroutine());
        }

            
    }

    private IEnumerator GotoMoneyCollecterCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.01f);

            // Calculate the direction to the target.
            Vector3 moveDirection = (moneyCollecterTargetPos - transform.position).normalized;

            // Calculate the desired velocity based on the moveSpeed.
            Vector3 desiredVelocity = moveDirection * moveSpeed;

            // Update the position smoothly based on deltaTime.
            transform.position += desiredVelocity * Time.deltaTime;

            // Check if the object has reached the target position.
            if (Vector3.Distance(transform.position, moneyCollecterTargetPos) < destroyDistanceThreshold)
            {
                transform.position = moneyStackPos;
                gotoMoneyCollecter = false;
                break;
            }
        }


    }

    private IEnumerator GotoPlayerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.01f);

            targetPos = new Vector3(player.position.x, player.position.y + 1, player.position.z);

            // Calculate the direction to the target.
            Vector3 moveDirection = (targetPos - transform.position).normalized;

            // Calculate the desired velocity based on the moveSpeed.
            Vector3 desiredVelocity = moveDirection * moveSpeed;

            // Update the position smoothly based on deltaTime.
            transform.position += desiredVelocity * Time.deltaTime;

            // Check if the object has reached the target position.
            if (Vector3.Distance(transform.position, targetPos) < destroyDistanceThreshold)
            {
                gameManager.AddMoney(5);
                // Destroy the object when it's close enough to the target.
                Destroy(gameObject);
                break;
            }

            else
            {
                // Calculate the direction to the target.
                moveDirection = (targetPos - transform.position).normalized;

                // Calculate the desired velocity based on the moveSpeed.
                desiredVelocity = moveDirection * moveSpeed;

                // Update the position smoothly based on deltaTime.
                transform.position += desiredVelocity * Time.deltaTime;
            }
        }    
    }
}
