using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class ShortCuts : MonoBehaviour
{
    [SerializeField] DialogueContainer testDialogue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            D_Manager.Instance.StartDialogue(testDialogue);
        }
    }
}
