using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public DialogueContainer dialogue;

    private void OnMouseDown()
    {
        D_Manager.Instance.StartDialogue(dialogue);
    }
}