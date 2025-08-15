using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] DialogueContainer defaultResponse;
    public DialogueContainer beforeDrinkDialogue;
    public DialogueContainer correctDrinkDialogue;
    public DialogueContainer wrongDrinkDialogue;
    private bool activated = false;

    private void OnMouseDown()
    {
        if (activated)
        {
            if (!DrinkManager.Instance.DrinkDone())
            {
                if (defaultResponse)
                D_Manager.Instance.StartDialogue(defaultResponse);
                return;
            }

            if (DrinkManager.Instance.FinishDrink())
            {
                D_Manager.Instance.StartDialogue(correctDrinkDialogue);
                return;
            }
            else
            {
                D_Manager.Instance.StartDialogue(wrongDrinkDialogue);
                return;
            }

        }

        D_Manager.Instance.StartDialogue(beforeDrinkDialogue);
        activated = true;
    }
}