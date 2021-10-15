using LitJson;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISavable
{
    string SaveID { get; }
    JsonData SavedData { get; }

    void LoadFromData(JsonData data);
}

public static class SavingService
{
    private const string ACTIVE_SCENE_KEY = "activeScene";
    private const string SCENES_KEY = "scenes";
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    static UnityEngine.Events.UnityAction<Scene, LoadSceneMode>
        LoadObjectsAfterSceneLoad;

    public static void SaveGame(string fileName)
    {
        var result = new JsonData();
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISavable>();

        if(allSaveableObjects.Count() > 0)
        {
            var savedObjects = new JsonData();
            foreach (var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;
                if (data.IsObject)
                {
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    savedObjects.Add(data);
                }
                else
                {
                    var behaviour = saveableObject as MonoBehaviour;
                    Debug.LogWarning($"{ behaviour}'s save data is not a dictionary. The {behaviour.name} object  was not saved");
                }
            }
            result[OBJECTS_KEY] = savedObjects;
        }
        else
        {
            Debug.LogWarning("The scene did not include any saveable objects");
        }

        var openScenes = new JsonData();
        var sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene.name);
        }

        result[SCENES_KEY] = openScenes;
        result[ACTIVE_SCENE_KEY] = SceneManager.GetActiveScene().name;

        var writer = new JsonWriter();
        writer.PrettyPrint = true;
        result.ToJson(writer);

        var outputPath = PathForFilename(fileName);
        File.WriteAllText(outputPath, writer.ToString());
        Debug.Log($"Wrote saved game to {outputPath}");

        result = null;
        System.GC.Collect();
    }

    public static bool LoadGame(string fileName)
    {
        var dataPath = PathForFilename(fileName);
        if(!File.Exists(dataPath))
        {
            Debug.LogError($"No file exists at {dataPath}");
            return false;
        }

        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);
        if(data == null || !data.IsObject)
        {
            Debug.LogError($"Data at {dataPath} is not a JSON object");
            return false;
        }

        if(!data.ContainsKey("scenes"))
        {
            Debug.LogWarning($"Data at {dataPath} does not contain any scenes; not loading any");
            return false;
        }

        var scenes = data[SCENES_KEY];
        int sceneCount = scenes.Count;
        if(sceneCount == 0)
        {
            Debug.LogWarning($"Data at {dataPath} doesn't specify any scenes to load");
            return false;
        }

        for (int i = 0; i < sceneCount; i++)
        {
            var scene = (string)scenes[i];
            if (i == 0)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

        if(data.ContainsKey(ACTIVE_SCENE_KEY))
        {
            var activeSceneName = (string)data[ACTIVE_SCENE_KEY];
            var activeScene = SceneManager.GetSceneByName(activeSceneName);

            if(!activeScene.IsValid())
            {
                Debug.LogError($"Data at {dataPath} scecifies  an active scene that doesn't exist. Stopping loading here");
                return false;
            }

            SceneManager.SetActiveScene(activeScene);
        }
        else
        {
            Debug.LogWarning($"Data at {dataPath} does not specify an active scene");
        }

        if(data.ContainsKey(OBJECTS_KEY))
        {
            var objects = data[OBJECTS_KEY];

            LoadObjectsAfterSceneLoad = (scene, loadSceneMode) =>
            {
                var allLoadableObjects = Object
                    .FindObjectsOfType<MonoBehaviour>()
                    .OfType<ISavable>()
                    .ToDictionary(o => o.SaveID, o => o);

                var objectsCount = objects.Count;
                for (int i = 0; i < objectsCount; i++)
                {
                    var objectData = objects[i];
                    var saveID = (string)objectData[SAVEID_KEY];

                    if (allLoadableObjects.ContainsKey(saveID))
                    {
                        var loadableObject = allLoadableObjects[saveID];
                        loadableObject.LoadFromData(objectData);
                    }
                }

                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                LoadObjectsAfterSceneLoad = null;
                System.GC.Collect();
            };

            SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
        }

        return true;
    }
    public static string PathForFilename(string filename)
    {
        var folderToStareFilesIn = Application.persistentDataPath;
        var path = System.IO.Path.Combine(folderToStareFilesIn, filename);

        return path;
    }
}
