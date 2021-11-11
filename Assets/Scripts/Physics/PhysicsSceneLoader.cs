using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSceneLoader : MonoBehaviour
{
    [SerializeField]
    string physicsSceneName;
    [SerializeField]
    float physicsSceneTimeScale = 1;

    PhysicsScene physicsScene;

    private void Start()
    {
        LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
        Scene scene = SceneManager.LoadScene(physicsSceneName, param);

        physicsScene = scene.GetPhysicsScene();
    }

    private void FixedUpdate()
    {
        if (physicsScene != null)
        {
            physicsScene.Simulate(Time.fixedDeltaTime *  physicsSceneTimeScale);
        }
    }
}
