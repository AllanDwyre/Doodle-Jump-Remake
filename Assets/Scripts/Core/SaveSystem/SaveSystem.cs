using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(ScoreController scoreController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves";

        FileStream stream = new FileStream(path, FileMode.Create);
        ScoreData data = new ScoreData(scoreController);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static ScoreData LoadData()
    {
        string path = Application.persistentDataPath + "/saves";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in : " + path);
            return null;
        }
    }
}
