using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public DialogueContainer beforeDrinkDialogue;
    public DialogueContainer endDialogue;
    public string correct;
    public string wrong;
    private bool activated = false;
    private bool readyToGo = false;
    private bool stop = false;

    public GameObject Outline;

    private void OnMouseDown()
    {
        if(stop) return;

        if (D_Manager.Instance.IsSpeaking()) return;
        Outline.SetActive(false);

        if (readyToGo)
        {
            D_Manager.Instance.StartDialogue(endDialogue);
            stop = true;
            return;
        }

        if (activated)
        {
            if (!DrinkManager.Instance.DrinkDone())
            {
                string drink = ShiftManager.Instance.GetCustomerVisit().requestedDrink.drinkName;
                D_Manager.Instance.SetTextTo("I ordered " + drink.ToLower() + ".");
                return;
            }

            if (DrinkManager.Instance.FinishDrink())
            {
                string name = ShiftManager.Instance.GetCustomerVisit().customer.CharacterName;
                D_Manager.Instance.SetTextTo($"<b>{name}</b>\n{correct}");
                readyToGo = true;
                return;
            }
            else
            {
                string name = ShiftManager.Instance.GetCustomerVisit().customer.CharacterName;
                D_Manager.Instance.SetTextTo($"<b>{name}</b>\n{wrong}");
                readyToGo = true;
                return;
            }

        }

        D_Manager.Instance.StartDialogue(beforeDrinkDialogue);
        activated = true;
    }

    private void OnMouseEnter()
    {
        if (D_Manager.Instance.IsSpeaking()) return;
        Outline.SetActive(true);
    }

    private void OnMouseExit()
    {
        Outline.SetActive(false);
    }
}