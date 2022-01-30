using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> m_audioClips = new List<AudioClip>();

    public void PlayClip(int index)
    {
        gameObject.GetComponent<AudioSource>().clip = m_audioClips[index];
        gameObject.GetComponent<AudioSource>().Play();
    }
}
