using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [System.Serializable]
    public class DialogueData
    {
        [TextArea] public string dialogueText;
        public string[] choices;
        public int[] results;
        [TextArea] public string[] responseTexts;
    }

    [Header("UI Bileşenleri")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Button[] choiceButtons;

    [Header("Diyaloglar")]
    public List<DialogueData> dialogues = new List<DialogueData>();

    public delegate void DialogueResponse(int dialogueID, int choiceIndex);
    public DialogueResponse onDialogueResponse;


    private Dictionary<int, int> chosenOptions = new Dictionary<int, int>();
    private Coroutine timeoutCoroutine;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        foreach (var btn in choiceButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void StartDialogue(int id)
    {
        if (chosenOptions.ContainsKey(id)) {
            dialogueText.text = "Hadi Git!!";
            foreach (var btn in choiceButtons)
                btn.gameObject.SetActive(false);

            StopAllCoroutines();
            StartCoroutine(FadeInPanel());
            return;
        }
        if (id < 0 || id >= dialogues.Count) return;

        DialogueData data = dialogues[id];
        dialogueText.text = data.dialogueText;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < data.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = data.choices[i];

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(id, choiceIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        StopAllCoroutines();
        StartCoroutine(FadeInPanel());

        if (timeoutCoroutine != null) StopCoroutine(timeoutCoroutine);
        timeoutCoroutine = StartCoroutine(TimeoutWarning());
    }

    private void OnChoiceSelected(int dialogueID, int choiceIndex)
    {
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
            timeoutCoroutine = null;
        }

        chosenOptions[dialogueID] = choiceIndex;
        int resultID = dialogues[dialogueID].results[choiceIndex];

        if (dialogues[dialogueID].responseTexts.Length > choiceIndex)
        {
            dialogueText.text = dialogues[dialogueID].responseTexts[choiceIndex];
        }

        foreach (var btn in choiceButtons)
        {
            btn.gameObject.SetActive(false);
        }

        onDialogueResponse?.Invoke(dialogueID, choiceIndex);
    }

    public void CloseDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutPanel());
    }

    private IEnumerator FadeInPanel()
    {
        dialoguePanel.SetActive(true);
        canvasGroup.alpha = 0f;

        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * 3f;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutPanel()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * 3f;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        dialoguePanel.SetActive(false);
    }

    private IEnumerator TimeoutWarning()
    {
        yield return new WaitForSeconds(15f);
        dialogueText.text = "KoNuş ArTık!!";
        Debug.Log("Zaman aşımı: Oyuncu seçim yapmadı.");
    }

    public bool HasChosen(int dialogueID, int choiceIndex)
    {
        return chosenOptions.ContainsKey(dialogueID) && chosenOptions[dialogueID] == choiceIndex;
    }
    public Dictionary<int, int> GetAllChoices()
    {
        return chosenOptions;
    }

    public void LoadChoices(Dictionary<int, int> loaded)
    {
        chosenOptions = loaded;
    }

    public void ResetChoices()
    {
        chosenOptions.Clear();
    }
}