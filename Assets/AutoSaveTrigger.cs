using UnityEngine;
using System.Globalization;
using System.IO;
using System.Collections.Generic;

public class AutoSaveTrigger : MonoBehaviour
{
    private string savePath;
    public Transform playerTransform;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/save_log.txt";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        SaveGame();
        Debug.Log("Otomatik kayýt tetiklendi.");
    }

    private void SaveGame()
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
    }
}