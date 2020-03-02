using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject StickL;
    [SerializeField]
    private GameObject StickR;
    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_ANDROID
        if (StickL != null)
        {
            StickL.SetActive(true);
        }
        if (StickR != null)
        {
            StickR.SetActive(true);
        }
#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBGL
        if (StickL != null)
        {
            StickL.SetActive(false);
        }
        if (StickR != null)
        {
            StickR.SetActive(false);
        }
#endif
    }
}
