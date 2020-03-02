using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> Clips = new List<AudioClip>();
    
    private void Awake()
    {
        GetComponent<AudioSource>().clip = Clips[Random.Range(0, Clips.Count)];
    }
}
