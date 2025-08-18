using Characters;
using UnityEngine;

public class ShiftManager : Singleton<ShiftManager>
{
    [Header("Shift Data")]
    public NightShift[] nightShifts;
    public NightShift currentShift;
    private int currentShiftIndex = 0;

    private CustomerVisit currentCustomer;
    private int currentCustomerIndex = 0;

    private int approvalScore = 100;

    public CustomerVisit GetCustomerVisit() => currentCustomer;
    public int GetApprovalScore() => approvalScore;

    void Start()
    {
        StartShift();
    }
    private void SpawnCustomer(CustomerVisit visit)
    {
        currentCustomer = visit;
        Character character = CharacterDatabase.GetCharacterByID(visit.customer.CharacterID);
        var spawnedCharacter = CharacterManager.Instance.SpawnCharacter(character);

        var dialogueActiv = spawnedCharacter.GetComponent<DialogueActivator>();
        dialogueActiv.beforeDrinkDialogue = visit.startDialogue;
        dialogueActiv.correct = visit.correctAnswer;
        dialogueActiv.wrong = visit.wrongAnswer;
        dialogueActiv.endDialogue = visit.endDialogue;

        Debug.Log($"Customer {visit.customer.CharacterID} arrived");
    }

    public void OnNextCustomer()
    {
        if (currentCustomerIndex < currentShift.customerVisits.Count)
        {
            var nextCustomer = currentShift.customerVisits[currentCustomerIndex];
            SpawnCustomer(nextCustomer);
            currentCustomerIndex++;
        }
        else
        {
            EndShift();
        }
    }

    public void StartShift()
    {
        currentShift = nightShifts[currentShiftIndex];
        currentCustomerIndex = 0;
        approvalScore = 100;

        Invoke("OnNextCustomer", 4f);
    }

    public void EndShift()
    {
        CharacterManager.Instance.RemoveCurrentCharacter();
        ShiftUI.instance.FinishShift();
        currentShiftIndex++;
        Debug.Log("Shift ended with approval: " + approvalScore);
    }

    public void Penalize(int amount)
    {
        approvalScore -= amount;
        Debug.Log($"Penalty: -{amount}");
    }

    public void Reward(int amount)
    {
        approvalScore += amount;
        Debug.Log($"Reward: +{amount}");
    }
}