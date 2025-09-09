using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TriggerTypewriterText : MonoBehaviour
{
    [Header("UI Bileþenleri")]
    public Image targetImage;
    public Text targetText;

    [Header("Gösterilecek içerik")]
    public Sprite spriteToShow;
    [TextArea] public string textToShow;

    [Header("Görünürlük ayarlarý")]
    public bool showImage = true;
    public bool showText = true;

    [Header("Daktilo ayarlarý")]
    public float charInterval = 0.04f;
    public AudioClip typeSfx;
    [Range(0f, 1f)] public float typeSfxVolume = 0.6f;

    [Header("Fade ayarlarý")]
    public float fadeDuration = 0.5f;

    [Header("Diyalog sistemi")]
    public bool triggerDialogue = false;
    public DialogueManager dialogueManager;
    public int dialogueID = 0;

    private Coroutine typeCoroutine;
    private Coroutine fadeCoroutine;
    private AudioSource audioSource;

    private void Start()
    {
        if (targetImage != null) { SetAlpha(targetImage, 0f); targetImage.enabled = false; }
        if (targetText != null) { SetAlpha(targetText, 0f); targetText.enabled = false; }

        audioSource = GetComponent<AudioSource>();
        if (typeSfx != null && audioSource == null)
            Debug.LogWarning("[TriggerTypewriterText] AudioSource eksik.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        if (showImage && targetImage != null)
        {
            targetImage.sprite = spriteToShow;
            targetImage.enabled = true;
            fadeCoroutine = StartCoroutine(FadeIn(targetImage));
        }

        if (showText && targetText != null)
        {
            targetText.enabled = true;
            targetText.text = "";
            fadeCoroutine = StartCoroutine(FadeIn(targetText));

            if (typeCoroutine != null) StopCoroutine(typeCoroutine);
            typeCoroutine = StartCoroutine(TypeText(textToShow));
        }

        if (triggerDialogue && dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogueID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (typeCoroutine != null) { StopCoroutine(typeCoroutine); typeCoroutine = null; }
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        if (showImage && targetImage != null)
            fadeCoroutine = StartCoroutine(FadeOut(targetImage));

        if (showText && targetText != null)
            fadeCoroutine = StartCoroutine(FadeOut(targetText));
    }

    private IEnumerator TypeText(string fullText)
    {
        targetText.text = "";

        foreach (char ch in fullText)
        {
            targetText.text += ch;

            if (!char.IsWhiteSpace(ch) && typeSfx != null && audioSource != null)
                audioSource.PlayOneShot(typeSfx, typeSfxVolume);

            yield return new WaitForSeconds(charInterval);
        }

        typeCoroutine = null;
    }

    private IEnumerator FadeIn(Graphic ui)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            SetAlpha(ui, Mathf.Lerp(0f, 1f, time / fadeDuration));
            yield return null;
        }
        SetAlpha(ui, 1f);
    }

    private IEnumerator FadeOut(Graphic ui)
    {
        float startAlpha = ui.color.a;
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            SetAlpha(ui, Mathf.Lerp(startAlpha, 0f, time / fadeDuration));
            yield return null;
        }
        SetAlpha(ui, 0f);
        ui.enabled = false;
    }

    private void SetAlpha(Graphic ui, float alpha)
    {
        Color c = ui.color;
        c.a = alpha;
        ui.color = c;
    }
}