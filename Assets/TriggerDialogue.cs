using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerDialogue : MonoBehaviour
{

    public int dialogueID = 0;
    public DialogueManager dialogueManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogueID);

        }
    }
    private void OnTriggerExit2D (Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (dialogueManager != null)
        {
            dialogueManager.CloseDialogue();

        }
    }
}