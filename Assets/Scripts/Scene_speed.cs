using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_speed : MonoBehaviour
{
    [SerializeField]
    public float timeFlow;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeFlow;
    }
}
