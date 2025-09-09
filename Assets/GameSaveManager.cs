using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public class GameSaveManager : MonoBehaviour
{
    [Header("UI Butonlarý")]
    public Button saveButton;
    public Button loadButton;
    public Button newGameButton;

    [Header("Oyuncu")]
    public Transform playerTransform;

    private string savePath;
    private Vector3 initialPosition;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/save_log.txt";
        initialPosition = playerTransform.position;

        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        newGameButton.onClick.AddListener(NewGame);
    }

    public void SaveGame()
    {
        using (StreamWriter writer = new StreamWriter(savePath))
        {
            Vector3 pos = playerTransform.position;
            writer.WriteLine("POS:" + pos.x.ToString(CultureInfo.InvariantCulture) + "," +
                                     pos.y.ToString(CultureInfo.InvariantCulture) + "," +
                                     pos.z.ToString(CultureInfo.InvariantCulture));

            foreach (var entry in DialogueManager.Instance.GetAllChoices())
            {
                writer.WriteLine("CHOICE:" + entry.Key + ":" + entry.Value);
            }
        }

        Debug.Log("Oyun kaydedildi: " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Henüz kayýt alýnmamýþ.");
            return;
        }

        string[] lines = File.ReadAllLines(savePath);
        Dictionary<int, int> loadedChoices = new Dictionary<int, int>();

        foreach (string line in lines)
        {
            if (line.StartsWith("POS:"))
            {
                string[] coords = line.Substring(4).Split(',');
                float x = float.Parse(coords[0], CultureInfo.InvariantCulture);
                float y = float.Parse(coords[1], CultureInfo.InvariantCulture);
                float z = float.Parse(coords[2], CultureInfo.InvariantCulture);
                playerTransform.position = new Vector3(x, y, z);
            }
            else if (line.StartsWith("CHOICE:"))
            {
                string[] parts = line.Substring(7).Split(':');
                int dialogueID = int.Parse(parts[0]);
                int choiceIndex = int.Parse(parts[1]);
                loadedChoices[dialogueID] = choiceIndex;
            }
        }

        DialogueManager.Instance.LoadChoices(loadedChoices);
        Debug.Log("Kayýt yüklendi.");
    }

    public void NewGame()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Yeni oyun baþlatýldý, kayýt sýfýrlandý.");
        }

        DialogueManager.Instance.ResetChoices();
        playerTransform.position = initialPosition;
    }

    // Otomatik kayýt tetikleyici olarak kullanýlabilir
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        SaveGame();
        Debug.Log("Otomatik kayýt tetiklendi.");
    }
}