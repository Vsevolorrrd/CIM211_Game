using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using UnityEngine;

public class D_Start : MonoBehaviour
{
    [SerializeField] DialogueContainer container;
    void Start()
    {
        D_Manager.Instance.StartDialogue(container, 1f);
    }
}
