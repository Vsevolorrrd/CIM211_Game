using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

namespace Subtegral.DialogueSystem.Runtime
{
    public class D_EventManager : MonoBehaviour
    {
        public void DialogueEvent(DialogueNodeData nodeData, D_conditionManager conditionManager, D_TrustManager trustManager)
        {
            switch (nodeData.EventType)
            {
                case DialogueEventType.Custom:
                    // add
                    break;

                case DialogueEventType.SetStringCondition:
                    conditionManager.AddStringCondition(nodeData.EventName.ToLowerInvariant());
                    break;

                case DialogueEventType.SetBooleanCondition:
                    if (nodeData.EventValue > 0)
                        conditionManager.SetBoolCondition(nodeData.EventName.ToLowerInvariant(), true);
                    else
                        conditionManager.SetBoolCondition(nodeData.EventName.ToLowerInvariant(), false);
                    break;

                case DialogueEventType.ChangeInteger:
                    trustManager.UpdateTrust((int)nodeData.EventValue);
                    break;

                case DialogueEventType.PlaySound:
                    AudioClip clip = AudioManager.Instance.GetSoundByName(nodeData.EventName);
                    if (clip != null) AudioManager.Instance.PlaySound(clip, nodeData.EventValue);
                    else Debug.LogWarning($"Sound '{nodeData.EventName}' not found in AudioManager.");
                    break;

                case DialogueEventType.PlayMusic:
                    AudioClip music = AudioManager.Instance.GetSoundByName(nodeData.EventName.ToLowerInvariant());
                    if (music != null)
                        AudioManager.Instance.PlaySound(music, nodeData.EventValue, true);
                    else Debug.LogWarning($"Music '{nodeData.EventName}' not found in AudioManager.");
                    break;

                case DialogueEventType.StopAllMusic:
                    AudioManager.Instance.StopAllLoopSources(nodeData.EventValue);
                    break;

                default:
                    Debug.LogWarning($"Invalid event: {nodeData.NodeType}");
                    break;
            }
        }
    }
}