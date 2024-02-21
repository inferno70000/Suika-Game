using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveScore(int score)
    {
        BinaryFormatter formatter = new();

        string path = Application.persistentDataPath + "/Score.fun";
        FileStream stream = new(path, FileMode.Create);
        SavedData data = new(score);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadScore()
    {
        string path = Application.persistentDataPath + "/Score.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            SavedData data = formatter.Deserialize(stream) as SavedData;
            stream.Close();

            return data.Score;
        }
        else
        {
            Debug.LogWarning("File not existed in: " + path);
            return -1;
        }
    }
}
