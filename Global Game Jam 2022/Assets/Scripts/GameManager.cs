using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_canvas;
    [SerializeField] private GameObject m_mainMenuPanel;
    [SerializeField] private GameObject m_creditsPanel;
    [SerializeField] private AudioManager m_audioManager;


    [SerializeField] private Slider m_scoreBar;

    [SerializeField] private GameObject m_textBox;
    [SerializeField] private TextMeshProUGUI m_text;

    [SerializeField] private List<bool> m_order = new List<bool>(); //true is text false is round

    [SerializeField] private List<GameObject> m_act1Rounds = new List<GameObject>();
    [SerializeField] private List<GameObject> m_act2Rounds = new List<GameObject>();
    [SerializeField] private List<GameObject> m_act3Rounds = new List<GameObject>();

    [SerializeField] private List<string> m_storyText = new List<string>();

    [SerializeField] private float m_maxScore = 50;
    private int m_currentAct = 1;
    private int m_currentRound = 0;
    private int m_currentText = 0;
    private int m_currentOrderSpot = 0;
    private bool m_canContinueText = false;

    [Header("Animations")]
    [SerializeField] private Animator m_caveBackDrop;
    [SerializeField] private Animator m_throneBackDrop;
    [SerializeField] private Animator m_curtainAnim;
    [SerializeField] private Animator m_forestAnim;
    [SerializeField] private Animator m_kingAnim;

    public GameObject RoundObject;

    public int CurrentAct => m_currentAct;
    public int CurrentRound => m_currentRound;

    public void Start()
    {
        m_text.text = m_storyText[0];
    }

    public void UpdateScoreBar(float currentScore)
    {
        m_scoreBar.value = currentScore / m_maxScore;
        m_currentRound++;
        m_canContinueText = true;
    }

    public void ContinueText(InputAction.CallbackContext context)
    {
        if (context.performed && m_canContinueText == true && m_currentOrderSpot < m_order.Count - 1)
        {
            m_currentOrderSpot++;

            if(m_currentOrderSpot == 3)//play act 1 music
            {
                m_audioManager.PlayClip(1);
            }

            if(m_currentOrderSpot == 7) //start of act 2
            {
                m_currentAct++;
                m_audioManager.PlayClip(2);
            }

            if (m_currentOrderSpot == 7) //start of act 3
            {
                m_currentAct++;
                m_audioManager.PlayClip(3);
            }

            if (m_order[m_currentOrderSpot])
            {
                m_currentText++;
                ChangeText(m_currentText);
            }
            else
            {
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
                RoundObject = Instantiate(m_act2Rounds[m_currentRound], m_canvas);
                break;
            case 3:
                RoundObject = Instantiate(m_act3Rounds[m_currentRound], m_canvas);
                break;
        }
    }

    public void StartGame()
    {
        m_mainMenuPanel.SetActive(false);
        m_canContinueText = true;
        m_audioManager.PlayClip(0);
        m_curtainAnim.Play("Curtain Drop Down");
    }

    public void CreditsMenu()
    {
        if(m_creditsPanel.activeSelf)
        {
            m_creditsPanel.SetActive(false);
        }
        else
        {
            m_creditsPanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CaveAnim(int index)
    {
        switch(index)
        {
            case 1:
                m_caveBackDrop.Play("Cave Drop Down");
                break;
            case 2:
                m_caveBackDrop.Play("Cave Pull Up");
                break;
        }
    }

    public void ThroneAnim(int index)
    {
        switch (index)
        {
            case 1:
                m_throneBackDrop.Play("Cave Drop Down");
                break;
            case 2:
                m_throneBackDrop.Play("Cave Pull Up");
                break;
        }
    }

    public void ForestAnim(int index)
    {
        switch (index)
        {
            case 1:
                m_forestAnim.Play("Forest Slide In");
                break;
            case 2:
                m_forestAnim.Play("Forest Slide Out");
                break;
        }
    }

    public void KingAnim(int index)
    {
        switch (index)
        {
            case 1:
                m_kingAnim.Play("King Slide In");
                break;
            case 2:
                m_kingAnim.Play("King Slide Out");
                break;
        }
    }

    private void CheckToPlayAnim()
    {
        if (m_currentOrderSpot == 1) //play throne open
        {
            ThroneAnim(1);
        }

        if (m_currentOrderSpot == 1) //play throne close
        {
            ThroneAnim(2);
        }

        if (m_currentOrderSpot == 1) //play cave open
        {
            CaveAnim(1);
        }

        if (m_currentOrderSpot == 1) //play cave close
        {
            CaveAnim(2);
        }

        if (m_currentOrderSpot == 1) //play forest open
        {
            ForestAnim(1);
        }

        if (m_currentOrderSpot == 1) //play forest close
        {
            ForestAnim(2);
        }

        if (m_currentOrderSpot == 1) //play king open
        {
            KingAnim(1);
        }

        if (m_currentOrderSpot == 1) //play king close
        {
            KingAnim(2);
        }
    }
}
