using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestChangeScene : MonoBehaviour
{
    public int id;
    public void ChangeScene()
    {
        SceneManager.LoadScene(id);
    }
}
