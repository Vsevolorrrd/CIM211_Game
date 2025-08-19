using TMPro;
using UnityEngine;

public class ScoreShower : MonoBehaviour
{
    public TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Approval: {ShiftManager.Instance.GetApprovalScore()}";
    }
}
