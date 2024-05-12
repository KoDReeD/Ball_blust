using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SceneLoad(int index)
    {
        if (index == 1 || index == 0)
        {
            Manager.score = 0;
            GenerateBalls.counter = 0;
            GenerateBalls.timer = 0;
            GenerateBalls.ball3InField = 0;
            GenerateBalls.ballsField = 0;
            Player.isDead = false;
        }
        SceneManager.LoadScene(index);
    }
}
