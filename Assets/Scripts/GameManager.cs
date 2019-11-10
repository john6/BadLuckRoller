using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
                m_instance.Init();
            }

            return m_instance;
        }
    }

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            Init();
        }
        else if (m_instance != this)
        {
            Debug.LogWarning("Multiple game managers in the scene! Destroying this one");
            Destroy(this);
        }
    }

    [SerializeField] private int winningRoll;
    [SerializeField] private int numThrows;
    [SerializeField] private int oneStarPar;
    [SerializeField] private int twoStarPar;
    [SerializeField] private int threeStarPar;
    [SerializeField] private int startingLuck;
    [SerializeField] private Text luckMessagePrefab;
    [SerializeField] private float luckMessageDuration;
    [SerializeField] private Transform luckMessageContainer;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private Text die1Text;
    [SerializeField] private Text die2Text;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite fullStar;

    private int numThrown = 0;
    private int luck;

    private void Init()
    {
        luck = startingLuck;
    }

    public void AlterLuck(int luckChange, string message)
    {
        luck += luckChange;
        Text text = Instantiate(luckMessagePrefab);
        text.text = message;
        text.transform.SetParent(luckMessageContainer, true);
        Destroy(text.gameObject, luckMessageDuration);
    }

    public void OnDiceThrown(GameObject[] dice)
    {
        ++numThrown;
        if (luck <= 0)
        {
            StartCoroutine(DoWin(dice));
        }
        else if (numThrown >= numThrows)
        {
            StartCoroutine(DoLoss(dice));
        }
        else
        {
            StartCoroutine(DoNeutral(dice));
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private IEnumerator DoWin(GameObject[] dice)
    {
        yield return StartCoroutine(ShowRoll(dice, 7));
        winScreen.SetActive(true);

        int numStars = 0;
        if (numThrown < threeStarPar) ++numStars;
        if (numThrown < twoStarPar) ++numStars;
        if (numThrown < oneStarPar) ++numStars;

        for (int i = 0; i < numStars; ++i)
        {
            stars[i].sprite = fullStar;
        }
        for (int i = numStars; i < 3; ++i)
        {
            stars[i].sprite = emptyStar;
        }
    }

    private IEnumerator DoLoss(GameObject[] dice)
    {
        yield return StartCoroutine(ShowRoll(dice, winningRoll));
        lossScreen.SetActive(false);
    }

    private IEnumerator DoNeutral(GameObject[] dice)
    {
        List<int> possibles = new List<int>();
        for (int i = 2; i <= 12; ++i)
        {
            if (i != winningRoll && i != 7)
            {
                for (int j = 0; j < Mathf.Min(i - 1, 13 - i); ++j)
                {
                    possibles.Add(i);
                }
            }
        }
        int roll = Random.Range(0, possibles.Count);
        yield return StartCoroutine(ShowRoll(dice, possibles[roll]));
    }

    private IEnumerator ShowRoll(GameObject[] dice, int roll)
    {
        Rigidbody b1 = dice[0].GetComponent<Rigidbody>();
        Rigidbody b2 = dice[1].GetComponent<Rigidbody>();

        while (b1.velocity.magnitude > 0.1f || b2.velocity.magnitude > 0.1f)
        {
            yield return new WaitForSeconds(0.5f);
        }

        int die1 = Random.Range(1, Mathf.Min(roll, 7));
        die1Text.text = "" + die1;
        die2Text.text = "" + (roll - die1);
    }
}
