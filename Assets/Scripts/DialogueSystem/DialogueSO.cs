using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public DialogueData[] dialogues;
}

[Serializable]
public struct DialogueData
{
    public string Name;
    public string Dialogue;
}
