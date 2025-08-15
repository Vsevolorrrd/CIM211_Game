using UnityEngine;
using System;
using System.Collections.Generic;
using Subtegral.DialogueSystem.DataContainers;

[CreateAssetMenu(fileName = "NewNightShift", menuName = "NightShift System/Night Shift")]
public class NightShift : ScriptableObject
{
    public float shiftDurationInMinutes;

    [Tooltip("List of all customers during the night")]
    public List<CustomerVisit> customerVisits;
}

[Serializable]
public class CustomerVisit
{
    public Character customer;

    [Tooltip("At what minute into the shift the customer appears.")]
    public float appearanceTimeInMinutes;

    [Tooltip("What drink they want tonight.")]
    public Drink requestedDrink;

    [Tooltip("All dialogues for the night.")]
    public DialogueContainer startDialogue;
    public DialogueContainer correctDialogue;
    public DialogueContainer wrongDialogue;
}