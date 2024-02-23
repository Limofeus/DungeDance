using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSummoner : MonoBehaviour
{
    public GameObject pauseWindowPrefab;
    public bool paused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            GameObject pauseWindow = Instantiate(pauseWindowPrefab, transform);
            pauseWindow.GetComponent<PauseWindow>().pauseSummoner = this;
        }
    }
}
