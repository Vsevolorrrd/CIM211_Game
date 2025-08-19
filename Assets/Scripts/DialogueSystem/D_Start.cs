using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class D_Start : MonoBehaviour
{
    [SerializeField] DialogueContainer container;
    [SerializeField] D_Manager manager;
    void Start()
    {
        manager.StartDialogue(container, 1f);
    }
}
