using UnityEngine;
using RDG;
using TMPro;

public class BuyPoint : MonoBehaviour
{
    public int srNo, purchaseAmount;
    private GameManager gameManager;
    private float countAnimSpeed = 0.1f;
    public TextMeshPro moneyAmountText;
    public GameObject objToUnlock, objToHide;
    private AudioManager audioManager;

    private void Awake()
    {
        // For Test
        //PlayerPrefs.SetString(srNo + "Unlocked", "True");

        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();

        if (PlayerPrefs.HasKey(srNo + "Unlocked"))
        {
            UnlockObject();
        }

        purchaseAmount = PlayerPrefs.GetInt(srNo + "PurchaseAmount", purchaseAmount);

        ShowPurchaseAmount();
    }

    private void ShowPurchaseAmount()
    {
        moneyAmountText.text = purchaseAmount.ToString();
    }

    public void StartSpend()
    {
        if (purchaseAmount > 100)
            countAnimSpeed = 0.01f;
         else if (purchaseAmount > 500)
            countAnimSpeed = 0.001f;

        InvokeRepeating("Spend", countAnimSpeed, countAnimSpeed);
    }

    private void Spend()
    {
        if (gameManager.collectedMoney > 0)
        {
            audioManager.Play("BuyPoint");

            Vibration.Vibrate(30);
            purchaseAmount--;
            PlayerPrefs.SetInt(srNo + "PurchaseAmount", purchaseAmount);

            gameManager.LessMoney(1);
            ShowPurchaseAmount();

            if (purchaseAmount == 0)
            {
                PlayerPrefs.SetString(srNo + "Unlocked", "True");

                UnlockObject();

                audioManager.Play("Unlock");
            }
        }
    }

    private void UnlockObject()
    {
        objToUnlock.SetActive(true);

        Destroy(objToHide);

        Destroy(gameObject);
    }

    public void StopSpend()
    {
        CancelInvoke("Spend");
    }
}
