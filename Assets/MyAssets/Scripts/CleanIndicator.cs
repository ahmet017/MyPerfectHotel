using UnityEngine;

public class CleanIndicator : MonoBehaviour
{
    public Animator cleanTimerAnim;

    public void StartCleanCount()
    {
        cleanTimerAnim.enabled = true;
    }

    public void StopCleanCount()
    {
        cleanTimerAnim.enabled = false;
    }

    public void CleanDone()
    {
        transform.parent.GetComponentInParent<Animator>().SetBool("IsDirty", false);
        Reset();

        GetComponentInParent<Room>().Cleaned();
    }

    private void Reset()
    {
        cleanTimerAnim.Rebind();
        cleanTimerAnim.enabled = false;
        transform.parent.gameObject.SetActive(false);
    }
}
