using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadSceneScript: MonoBehaviour {

    public string m_sceneName;

	private void UnloadCurrentScene() {
        SceneManager.UnloadSceneAsync(m_sceneName);
    }

    public void Update() {
        if (Input.anyKey) {
            UnloadCurrentScene();
        }
    }
}
