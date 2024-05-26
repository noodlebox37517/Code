using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Narrative
{
    [CreateAssetMenu(fileName = "DialogueObject", menuName = "ScriptableObjects/DialogueObject", order = 1)]
    public class DialogueObject : ScriptableObject
    {
        [FormerlySerializedAs("RandomQue")] public bool randomQue;
        [TextArea(10,10)]
        [SerializeField] private string Dialogue;
    
    }
}
