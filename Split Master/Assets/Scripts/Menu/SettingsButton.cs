using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;
    private bool open;
    private Animator animator;

    private void Start()
    {
        open = false;
        animator = settingsPanel.GetComponent<Animator>();
    }

    public void OpenSettings()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Expand") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Contract"))
        {
            if (open)
            {
                StartCoroutine(CloseAnimation());
            }
            else
            {
                StartCoroutine(OpenAnimation());
            }
            open = !open;
        }
    }

    private IEnumerator CloseAnimation()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.34f);
        settingsPanel.SetActive(false);
    }

    private IEnumerator OpenAnimation()
    {
        settingsPanel.SetActive(true);
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(0.1f);
    }
}
