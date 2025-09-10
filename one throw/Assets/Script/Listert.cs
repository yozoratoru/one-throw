using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listert : MonoBehaviour
{
    private Camera mainCam;
    private Vector2 screenBounds;

    void Start()
    {
        mainCam = Camera.main;
        screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x < mainCam.transform.position.x - screenBounds.x ||
            pos.x > mainCam.transform.position.x + screenBounds.x ||
            pos.y < mainCam.transform.position.y - screenBounds.y ||
            pos.y > mainCam.transform.position.y + screenBounds.y)
        {
            GameManager.Instance.RestartGame();;
        }
    }
}
