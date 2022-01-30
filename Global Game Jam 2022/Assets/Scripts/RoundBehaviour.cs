using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_keys = new List<GameObject>();

    [SerializeField] private List<string> m_roundInfo = new List<string>();
    public List<string> RoundInfo => m_roundInfo;

    public void RemoveKey()
    {
        if (m_keys.Count > 0)
        {
            GameObject temp = m_keys[0];
            
            m_keys.RemoveAt(0);
            Destroy(temp);

            if (m_keys.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
