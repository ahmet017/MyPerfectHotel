using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUiManager : MonoBehaviour
{
    public Text collectedMoney;
    public GameObject dragToMoveWindow;
    public AudioManager audioManager;
    public AdsManager adsManager;

    private void Update()
    {
        if (Input.GetMouseButton(0) && dragToMoveWindow)
        {
            audioManager.Play("Click");
            Destroy(dragToMoveWindow);
        }
    }

    public void SetMoneyText(int amount)
    {
        collectedMoney.text = "$" + amount.ToString();
    }

    public void Reload()
    {
        audioManager.Play("Click");

        adsManager.ShowInterstitialAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GetRewardCash()
    {
        audioManager.Play("Click");
        adsManager.ShowRewardedAd();
    }
}
