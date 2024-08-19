using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    public int page;
    public int minPageLimit;
    public int maxPageLimit;
    public float lerpSpeed;
    public Transform mapContent;

    const float pageDist = 18f;

    // Update is called once per frame
    void Update()
    {
        mapContent.localPosition = Vector3.Lerp(mapContent.localPosition ,Vector3.left * page * pageDist, lerpSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        Debug.Log("On map mover Disabled callback!");
        MenuDataManager.saveData.currentMapTab = page;
    }

    public void SetMapMoverPageToSaveDataPage()
    {
        page = MenuDataManager.saveData.currentMapTab;
        mapContent.localPosition = Vector3.left * page * pageDist;
    }

    public void moveRight()
    {
        if(page < maxPageLimit)
        {
            page += 1;
        }
    }

    public void moveLeft()
    {
        if (page > minPageLimit)
        {
            page -= 1;
        }
    }
}
