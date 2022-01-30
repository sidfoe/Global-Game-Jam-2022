using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_canvas;

    [SerializeField] private Slider m_scoreBar;

    [SerializeField] private GameObject m_textBox;
    [SerializeField] private TextMeshProUGUI m_text;

    [SerializeField] private List<bool> m_order = new List<bool>(); //true is text false is round

    [SerializeField] private List<GameObject> m_act1Rounds = new List<GameObject>();
    [SerializeField] private List<GameObject> m_act2Rounds = new List<GameObject>();
    [SerializeField] private List<GameObject> m_act3Rounds = new List<GameObject>();

    [SerializeField] private List<string> m_storyText = new List<string>();

    private float m_maxScore = 50;
    private int m_currentAct = 1;
    private int m_currentRound = 0;
    private int m_currentText = 0;
    private int m_currentOrderSpot = 0;
    private bool m_canContinueText = true;

    public GameObject RoundObject;

    public void Start()
    {
        m_text.text = m_storyText[0];
    }

    public void UpdateScoreBar(float currentScore)
    {
        Debug.Log("update score");
        m_scoreBar.value = currentScore / m_maxScore;
        m_currentRound++;
        m_canContinueText = true;
    }

    public void ContinueText(InputAction.CallbackContext context)
    {
        if (context.performed && m_canContinueText == true)
        {
            m_currentOrderSpot++;
            Debug.Log("orderspot: " + m_currentOrderSpot);

            if(m_order[m_currentOrderSpot])
            {
                Debug.Log("text: " + m_currentText);
                m_currentText++;
                ChangeText(m_currentText);
            }
            else
            {
                Debug.Log("round");
                StartRound();
            }
        }
    }

    private void ChangeText(int index)
    {
        m_text.text = m_storyText[index];
    }

    private void StartRound()
    {
        m_canContinueText = false; 

        switch(m_currentAct)
        {
            case 1:
                RoundObject = Instantiate(m_act1Rounds[m_currentRound], m_canvas);
                break;
            case 2:
                RoundObject = Instantiate(m_act2Rounds[m_currentRound]);
                break;
            case 3:
                RoundObject = Instantiate(m_act3Rounds[m_currentRound]);
                break;
        }
    }
}
