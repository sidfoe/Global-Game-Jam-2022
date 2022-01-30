using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;

    //Time variables
    public float m_timeBetweenPress = .5f;
    [SerializeField] private float m_timePassed;
    private bool m_startTime = false;

    [Header ("Score Variables")]
    [SerializeField] private float m_currentScore;
    [SerializeField] private float m_failedNoteAmount = .25f;
    [SerializeField] private float m_correctNoteAmount = 1.5f;
    [SerializeField] private float m_perfectBonus = 1.5f;

    private List<string> m_playerInput = new List<string>();

    private int m_correctCount = 0;

    //Round Lists
    [SerializeField] private List<string> m_round1 = new List<string>();
    [SerializeField] private List<string> m_round2 = new List<string>();
    [SerializeField] private List<string> m_round3 = new List<string>();

    public void Start()
    {
        gameObject.scene.IsValid();
    }

    void Update()
    {
        if(m_startTime == true)
        {
            m_timePassed += Time.deltaTime;
        }
    }

    //Called after a press to see if it was the correct key
    private void CheckPress()
    {
        if (m_playerInput.Count == 1)
        {
            m_timePassed = 0;
            m_startTime = true;
        }

        if (m_timePassed <= m_timeBetweenPress)
        {
            m_timePassed = 0;

            if (m_playerInput[m_playerInput.Count - 1] == m_gameManager.RoundObject.GetComponent<RoundBehaviour>().RoundInfo[m_playerInput.Count - 1]) //player made correct button press
            {
                m_correctCount++;
            }
            else //player hit wrong button
            {

            }

            if (m_playerInput.Count == m_round1.Count)
            {
                CalculateScore();
            }

            m_gameManager.RoundObject.GetComponent<RoundBehaviour>().RemoveKey();
        }
        else //did not press fast enough and they failed that press
        {
            
        }
    }

    private void CalculateScore()
    {
        m_startTime = false;

        if (m_correctCount == m_playerInput.Count)
        {
            m_currentScore += ((m_correctNoteAmount * m_playerInput.Count) * m_perfectBonus);
        }
        else
        {
            m_currentScore += (m_correctNoteAmount * m_correctCount) + (m_failedNoteAmount * (m_correctCount - m_playerInput.Count));
        }

        m_gameManager.UpdateScoreBar(m_currentScore);

        ClearPlayerInput();
    }

    private void ClearPlayerInput()
    {
        m_playerInput.Clear();
        m_correctCount = 0;
    }

    public void APress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerInput.Add("a");
            CheckPress();
        }
    }

    public void BPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerInput.Add("b");
            CheckPress();
        }
    }

    public void CPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerInput.Add("c");
            CheckPress();
        }
    }

    public void DPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerInput.Add("d");
            CheckPress();
        }
    }

    public void EPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerInput.Add("e");
            CheckPress();
        }
    }

    public void FPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_playerInput.Add("f");
            CheckPress();
        }
    }
}
