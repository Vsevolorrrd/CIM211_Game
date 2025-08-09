using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string CharacterID;
    public string CharacterName;
    public GameObject characterPrefab;
}