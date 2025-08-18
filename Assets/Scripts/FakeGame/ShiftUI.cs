using UnityEngine;

public class ShiftUI : Singleton<ShiftUI>
{
    [SerializeField] Animator anim;
    
    public void FinishShift()
    {
        if (anim != null)
        {
            anim.SetBool("Fade", true);
        }
        Invoke("NoTimeToThink", 4f);
    }
    private void NoTimeToThink()
    {
        if (anim != null)
        {
            anim.SetBool("Fade", false);
        }
        ShiftManager.instance.StartShift();
    }
}
