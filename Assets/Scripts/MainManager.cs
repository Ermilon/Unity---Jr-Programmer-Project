using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color TeamColor;
    private const string savefileJson = "/savefile.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    [System.Serializable]
    private class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        var data = new SaveData
        {
            TeamColor = TeamColor
        };

        var json = JsonUtility.ToJson(data);

        var path = Application.persistentDataPath + savefileJson;
        File.WriteAllText(path, json);
    }

    public void LoadColor()
    {
        var path = Application.persistentDataPath + savefileJson;
        if (!File.Exists(path)) return;

        var json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<SaveData>(json);

        TeamColor = data.TeamColor;
    }
}