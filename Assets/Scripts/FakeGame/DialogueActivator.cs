using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] DialogueContainer defaultResponse;
    public DialogueContainer beforeDrinkDialogue;
    public DialogueContainer endDialogue;
    private bool activated = false;

    private void OnMouseDown()
    {
        if (D_Manager.Instance.IsSpeaking()) return;

        if (activated)
        {
            if (!DrinkManager.Instance.DrinkDone())
            {
                string drink = ShiftManager.Instance.GetCustomerVisit().requestedDrink.drinkName;
                D_Manager.Instance.SetTextTo("I ordered " + drink + ".");
                return;
            }

            if (DrinkManager.Instance.FinishDrink())
            {
                D_Manager.Instance.StartDialogue(endDialogue);
                return;
            }
            else
            {
                D_Manager.Instance.StartDialogue(endDialogue);
                return;
            }

        }

        D_Manager.Instance.StartDialogue(beforeDrinkDialogue);
        activated = true;
    }
}