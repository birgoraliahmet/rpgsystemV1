using UnityEngine;

public class NPCResponse : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void Start()
    {
        if (dialogueManager != null)
        {
            dialogueManager.onDialogueResponse += HandleResponse;
        }
    }

    private void HandleResponse(int dialogueID, int choiceIndex)
    {
        // �rnek tepki: metin, animasyon, ses vs.
        if (dialogueID == 0 && choiceIndex == 0)
        {
            Debug.Log("NPC: G�zel se�im, kap�y� a��yorum.");
            // animator.SetTrigger("OpenDoor");
        }
        else if (dialogueID == 0 && choiceIndex == 1)
        {
            Debug.Log("NPC: Hmm, beklemek mi? �lgin�...");
            // animator.SetTrigger("Idle");
        }
    }

    private void OnDestroy()
    {
        if (dialogueManager != null)
        {
            dialogueManager.onDialogueResponse -= HandleResponse;
        }
    }
}