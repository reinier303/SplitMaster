using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KongregateUserNameText : MonoBehaviour
{
    private void Start()
    {
        KongregateAPIBehaviour kongregateAPI = KongregateAPIBehaviour.Instance;
        if(kongregateAPI == null)
        {
            return;
        }
        Text textComponent = GetComponent<Text>();
        if (kongregateAPI.Username != null)
        {
            textComponent.text = kongregateAPI.Username;
        }
        else
        {
            textComponent.text = "NaN";
        }
    }
}
