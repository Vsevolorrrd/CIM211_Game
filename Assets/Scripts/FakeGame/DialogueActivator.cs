using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] DialogueContainer defaultResponse;
    public DialogueContainer dialogue;
    private bool activated = false;

    private void OnMouseDown()
    {
        if (activated)
        {
            if(defaultResponse)
            D_Manager.Instance.StartDialogue(defaultResponse);
            return;
        }

        D_Manager.Instance.StartDialogue(dialogue);
        activated = true;
    }
}