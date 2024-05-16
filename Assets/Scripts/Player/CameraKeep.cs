using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeep : MonoBehaviour
{
    private static CameraKeep _instance;
    public static CameraKeep Instance { get { return _instance; } }

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
