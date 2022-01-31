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

    [SerializeField] private GameObject m_resultObject;
    [SerializeField] private TextMeshProUGUI m_resultText;


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
    [SerializeField] private Animator m_knightHelmetAnim;
    [SerializeField] private Animator m_knightAnim;
    [SerializeField] private Animator m_princessAnim;
    [SerializeField] private Animator m_turtleAnim;

    public GameObject RoundObject;

    public int CurrentAct => m_currentAct;
    public int CurrentRound => m_currentRound;

    public void Start()
    {
        m_text.text = m_storyText[0];
    }

    public void UpdateScoreBar(float currentScore)
    {
        CheckPuppetAnim();

        m_scoreBar.value = currentScore / m_maxScore;
        m_currentRound++;
        m_canContinueText = true;
    }

    public void ContinueText(InputAction.CallbackContext context)
    {
        if (context.performed && m_canContinueText == true && m_currentOrderSpot < m_order.Count - 1)
        {
            m_currentOrderSpot++;

            CheckToPlayAnim();

            if(m_currentOrderSpot == 6)//play act 1 music
            {
                m_audioManager.PlayClip(1);
            }

            if(m_currentOrderSpot == 14) //start of act 2
            {
                m_currentAct++;
                m_currentRound = 0;
                m_audioManager.PlayClip(2);
            }

            if (m_currentOrderSpot == 26) //start of act 3
            {
                m_currentAct++;
                m_currentRound = 0;
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

            if(m_currentOrderSpot == m_order.Count) //just updated to last segment in order
            {
                m_canContinueText = false;

                m_resultObject.SetActive(true);
                m_resultText.text = "About " + m_scoreBar.value + " percent of the audience enjoyed the show";
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

    private void KnightAnim(int index)
    {
        switch (index)
        {
            case 1:
                m_knightHelmetAnim.Play("KnightPuppet_SwordSwipe");
                break;
            case 2:
                m_knightHelmetAnim.Play("KnightPuppet_DownSwipe");
                break;
            case 3:
                m_knightHelmetAnim.Play("KnightPuppet_Walk");
                break;
            case 4:
                m_knightHelmetAnim.Play("KnightPuppet_SwordRaise");
                break;
            case 5:
                m_knightHelmetAnim.Play("MaskedknightPuppetDropdown");
                break;
        }
    }

    private void PrincessAnim(int index)
    {
        switch (index)
        {
            case 1:
                m_princessAnim.Play("PrincessPuppet_Handhold");
                break;
            case 2:
                m_princessAnim.Play("PrincessPuppet_idle");
                break;
            case 3:
                m_princessAnim.Play("PrincessPuppet_ragdoll");
                break;
            case 4:
                m_princessAnim.Play("PrincessPuppet_walking");
                break;
        }
    }

    private void TurtleAnim(int index)
    {
        switch (index)
        {
            case 1:
                m_turtleAnim.Play("DragonTurt_Kidnap");
                break;
            case 2:
                m_turtleAnim.Play("DragonTurt_ClawSwipe");
                break;
            case 3:
                m_turtleAnim.Play("DragonTurt_Roar");
                break;
            case 4:
                m_turtleAnim.Play("DragonTurt_Bite");
                break;
            case 5:
                m_turtleAnim.Play("DragonTurt_SLump");
                break;
        }
    }

    private void CheckPuppetAnim()
    {
        //HELMET KNIGHT
        if (m_currentOrderSpot == 17 || m_currentOrderSpot == 21) //knight slash 1
        {
            KnightAnim(1);
        }

        if (m_currentOrderSpot == 20 || m_currentOrderSpot == 22) //knight slash 2
        {
            KnightAnim(2);
        }

        if (m_currentOrderSpot == 18) //knight run
        {
            KnightAnim(3);
        }

        if (m_currentOrderSpot == 13) //knight raise hand
        {
            KnightAnim(4);
        }

        if (m_currentOrderSpot == 18) //knight ragdoll
        {
            KnightAnim(5);
        }

        //PRINCESS
        if (m_currentOrderSpot == 18) //princess hand hold
        {
            PrincessAnim(1);
        }

        if (m_currentOrderSpot == 3) //princess idle
        {
            PrincessAnim(2);
        }

        if (m_currentOrderSpot == 18) //princess ragdoll
        {
            PrincessAnim(3);
        }

        if (m_currentOrderSpot == 9) //princess kidnap swipe
        {
            PrincessAnim(4);
        }
        
        //TURTLE
        if (m_currentOrderSpot == 9) //kidnaop
        {
            TurtleAnim(1);
        }

        if (m_currentOrderSpot == 21) //swipe
        {
            TurtleAnim(2);
        }

        if (m_currentOrderSpot == 20) //roar
        {
            TurtleAnim(3);
        }

        if (m_currentOrderSpot == 18) //bite
        {
            TurtleAnim(4);
        }

        if (m_currentOrderSpot == 22) //slump
        {
            TurtleAnim(5);
        }

        //NO HELMET KNIGHT
        if (m_currentOrderSpot == 18) //hand hold
        {
            m_knightAnim.Play("KnightPuppetUnmasked_handholding");
        }
    }

    private void CheckToPlayAnim()
    {
        if (m_currentOrderSpot == 5 || m_currentOrderSpot == 10 || m_currentOrderSpot == 26) //play throne open
        {
            ThroneAnim(1);
        }

        if (m_currentOrderSpot == 8 || m_currentOrderSpot == 14) //play throne close
        {
            ThroneAnim(2);
        }

        if (m_currentOrderSpot == 18) //play cave open
        {
            CaveAnim(1);
        }

        if (m_currentOrderSpot == 26) //play cave close
        {
            CaveAnim(2);
        }

        if (m_currentOrderSpot == 8 || m_currentOrderSpot == 14) //play forest open
        {
            ForestAnim(1);
        }

        if (m_currentOrderSpot == 10 || m_currentOrderSpot == 26) //play forest close
        {
            ForestAnim(2);
        }

        if (m_currentOrderSpot == 6 || m_currentOrderSpot == 10 || m_currentOrderSpot == 27) //play king open
        {
            KingAnim(1);
        }

        if (m_currentOrderSpot == 7 || m_currentOrderSpot == 14) //play king close
        {
            KingAnim(2);
        }
    }
}
