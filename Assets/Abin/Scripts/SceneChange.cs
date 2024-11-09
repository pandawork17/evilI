using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int stage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    IEnumerator loadScene(){
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(stage);
    }
}
