using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_keys = new List<GameObject>();

    public void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame) //remove key with any key press
        {
            Destroy(m_keys[0]);
            m_keys.RemoveAt(0);
        }
    }
}
