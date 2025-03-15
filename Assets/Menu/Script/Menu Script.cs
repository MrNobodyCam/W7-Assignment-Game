using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayNormal()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void PlayHard()
    {
        SceneManager.LoadSceneAsync(2);
    }
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
