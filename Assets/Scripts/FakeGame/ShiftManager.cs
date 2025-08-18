using Characters;
using UnityEngine;

public class ShiftManager : Singleton<ShiftManager>
{
    [Header("Shift Data")]
    public NightShift currentShift;

    private CustomerVisit currentCustomer;
    private int currentCustomerIndex = 0;

    private int approvalScore = 100;

    public CustomerVisit GetCustomerVisit() => currentCustomer;
    public int GetApprovalScore() => approvalScore;

    void Start()
    {
        StartShift(currentShift);
    }
    private void SpawnCustomer(CustomerVisit visit)
    {
        currentCustomer = visit;
        Character character = CharacterDatabase.GetCharacterByID(visit.customer.CharacterID);
        var spawnedCharacter = CharacterManager.Instance.SpawnCharacter(character);

        var dialogueActiv = spawnedCharacter.GetComponent<DialogueActivator>();
        dialogueActiv.beforeDrinkDialogue = visit.startDialogue;
        //dialogueActiv.correctDrinkDialogue = visit.correctDialogue;
        //dialogueActiv.wrongDrinkDialogue = visit.wrongDialogue;
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

    public void StartShift(NightShift shift)
    {
        currentShift = shift;
        currentCustomerIndex = 0;
        approvalScore = 100;

        OnNextCustomer();
    }

    public void EndShift()
    {
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