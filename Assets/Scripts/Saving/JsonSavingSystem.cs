using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonSavingSystem : MonoBehaviour
{
    private const string extension = ".json";

    public IEnumerator LoadLastScene(string saveFile)
    {
        JObject state = LoadJsonFromFile(saveFile);
        IDictionary<string, JToken> stateDict = state;
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (stateDict.ContainsKey("lastSceneBuildIndex"))
        {
            buildIndex = (int)stateDict["lastSceneBuildIndex"];
        }
        yield return SceneManager.LoadSceneAsync(buildIndex);
        RestoreFromToken(state);
    }

    public void Save(string saveFile)
    {
        JObject state = LoadJsonFromFile(saveFile);
        CaptureAsToken(state);
        SaveFileAsJSon(saveFile, state);
    }

    public void Delete(string saveFIle)
    {
        File.Delete(GetPathFromSaveFile(saveFIle));
    }

    public void Load(string saveFile)
    {
        RestoreFromToken(LoadJsonFromFile(saveFile));
    }

    public IEnumerator<string> ListSaves()
    {
        foreach(string path in Directory.EnumerateFiles(Application.persistentDataPath))
        {
            if (Path.GetExtension(path) == extension)
            {
                yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }

    private JObject LoadJsonFromFile(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path))
        {
            return new JObject();
        }

        using (var textReader = File.OpenText(path))
        {
            using (var reader = new JsonTextReader(textReader))
            {
                reader.FloatParseHandling = FloatParseHandling.Double;

                return JObject.Load(reader);
            }
        }
    }

    private void SaveFileAsJSon(string saveFile, JObject state)
    {
        string path = GetPathFromSaveFile(saveFile);
        print("Saving to " + path);
        using (var textWriter = File.CreateText(path))
        {
            using(var writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;
                state.WriteTo(writer);
            }
        }
    }

    private void CaptureAsToken(JObject state)
    {
        IDictionary<string, JToken> stateDict = state;
        foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
        {
            stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureAsJtoken();
        }

        stateDict["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
    }

    private void RestoreFromToken(JObject state)
    {
        IDictionary<string, JToken> stateDict = state;
        foreach(JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
        {
            string id = saveable.GetUniqueIdentifier();
            if (stateDict.ContainsKey(id))
            {
                saveable.RestoreFromJToken(stateDict[id]);
            }
        }
    }

    private string GetPathFromSaveFile(string saveFIle)
    {
        return Path.Combine(Application.persistentDataPath, saveFIle + extension);
    }
}
