using Characters;
using UnityEngine;
using UnityEngine.Events;

public class ShiftManager : Singleton<ShiftManager>
{
    [Header("Shift Data")]
    public NightShift currentShift;

    [Header("Settings")]
    public float timeScale = 1f;

    [Header("Events")]
    public UnityEvent onShiftEnded;

    private CustomerVisit currentCustomer;
    private float shiftTimer; // In minutes
    private int currentCustomerIndex = 0;
    private bool isShiftActive;

    private int approvalScore = 100;

    public CustomerVisit GetCustomerVisit() => currentCustomer;
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
                currentCustomerIndex++;
                currentCustomer = next;
                Character character = CharacterDatabase.GetCharacterByID(currentCustomer.customer.CharacterID);
                CharacterManager.Instance.SetCharacterVisuals(character);
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
        approvalScore = 100;
    }

    public void EndShift()
    {
        isShiftActive = false;
        Debug.Log("Shift ended with approval: " + approvalScore);
        onShiftEnded?.Invoke();
    }

    public void Penalize(int amount)
    {
        approvalScore = approvalScore - amount;
        Debug.Log($"Penalty: -{amount}");
    }

    public void Reward(int amount)
    {
        approvalScore = approvalScore + amount;
        Debug.Log($"Reward: +{amount}");
    }
}