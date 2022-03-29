using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStreamingTrigger : MonoBehaviour
{
    [SerializeField]
    private string streamTargetScene;

    [SerializeField]
    private string triggerOwnScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if(dir < 90f)
            {
                StartCoroutine(StreamingTargetScene());
            }
            else
            {
                StartCoroutine(UnloadStreamingScene());
            }
        }
    }

    private IEnumerator StreamingTargetScene()
    {
        var targetScene = SceneManager.GetSceneByName(streamTargetScene);

        if(!targetScene.isLoaded)
        {
            var op = SceneManager.LoadSceneAsync(streamTargetScene, LoadSceneMode.Additive);

            while(!op.isDone)
            {
                yield return null;
            }
        }
    }

    private IEnumerator UnloadStreamingScene()
    {
        var targetScene = SceneManager.GetSceneByName(streamTargetScene);
        if(targetScene.isLoaded)
        {
            var currentScene = SceneManager.GetSceneByName(triggerOwnScene);

            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), currentScene);

            var op = SceneManager.UnloadSceneAsync(streamTargetScene);

            while(!op.isDone)
            {
                yield return null;
            }
        }
    }
}
