using UnityEngine;
using UnityEngine.Events;

public class ShiftManager : MonoBehaviour
{
    [Header("Shift Data")]
    public NightShift currentShift;

    [Header("Settings")]
    public float timeScale = 1f;

    [Header("Events")]
    public UnityEvent<CustomerVisit> onCustomerArrived;
    public UnityEvent onShiftEnded;

    private float shiftTimer; // In minutes
    private int currentCustomerIndex = 0;
    private bool isShiftActive;

    private float approvalScore = 100f;

    public float GetCurrentTime() => shiftTimer;
    public float GetApprovalScore() => approvalScore;


    void Start()
    {
        StartShift(currentShift);
    }

    void Update()
    {
        if (!isShiftActive) return;

        shiftTimer += Time.deltaTime / 60f * timeScale;

        if (currentCustomerIndex < currentShift.customerVisits.Count)
        {
            var next = currentShift.customerVisits[currentCustomerIndex];

            if (shiftTimer >= next.appearanceTimeInMinutes)
            {
                onCustomerArrived?.Invoke(next);
                currentCustomerIndex++;
            }
        }

        if (shiftTimer >= currentShift.shiftDurationInMinutes)
        {
            EndShift();
        }
    }

    public void StartShift(NightShift shift)
    {
        currentShift = shift;
        shiftTimer = 0f;
        currentCustomerIndex = 0;
        isShiftActive = true;
        approvalScore = 100f;
    }

    public void EndShift()
    {
        isShiftActive = false;
        Debug.Log("Shift ended with approval: " + approvalScore);
        onShiftEnded?.Invoke();
    }

    public void Penalize(float amount)
    {
        approvalScore = Mathf.Max(0f, approvalScore - amount);
        Debug.Log($"Penalty: -{amount}");
    }

    public void Reward(float amount)
    {
        approvalScore = Mathf.Min(100f, approvalScore + amount);
        Debug.Log($"Reward: +{amount}");
    }
}