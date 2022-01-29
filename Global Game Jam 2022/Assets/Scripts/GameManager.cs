using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider m_scoreBar;

    [SerializeField] private List<GameObject> m_act1Rounds = new List<GameObject>();
    [SerializeField] private List<GameObject> m_act2Rounds = new List<GameObject>();
    [SerializeField] private List<GameObject> m_act3Rounds = new List<GameObject>();

    private float m_maxScore = 50;

    public void UpdateScoreBar(float currentScore)
    {
        m_scoreBar.value = currentScore / m_maxScore;
    }


}
