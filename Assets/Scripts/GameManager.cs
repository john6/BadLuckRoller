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
    [SerializeField] private Image[] diceImages;
    [SerializeField] private int oneStarPar;
    [SerializeField] private int twoStarPar;
    [SerializeField] private int threeStarPar;
    [SerializeField] private int startingLuck;
    [SerializeField] private string nextScene;
    [SerializeField] private string winMessage;
    [SerializeField] private string lossMessage;
    [SerializeField] private Text luckMessagePrefab;
    [SerializeField] private float luckMessageDuration;
    [SerializeField] private float diceDisplayDuration;
    [SerializeField] private Transform luckMessageContainer;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private Image die1Image;
    [SerializeField] private Image die2Image;
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
        for (int i = 0; i < numThrows - numThrown; ++i)
        {
            diceImages[i].gameObject.SetActive(true);
        }
        for (int i = numThrows - numThrown; i < diceImages.Length; ++i)
        {
            diceImages[i].gameObject.SetActive(false);
        }
    }

    public void AlterLuck(int luckChange, string message)
    {
        luck += luckChange;
        Text text = Instantiate(luckMessagePrefab);
        text.text = message;
        text.transform.SetParent(luckMessageContainer, true);
        Destroy(text.gameObject, luckMessageDuration);
    }

    public void OnDiceThrown(PlayerController player, params GameObject[] dice)
    {
        ++numThrown;
        for (int i = 0; i < numThrows - numThrown; ++i)
        {
            diceImages[i].gameObject.SetActive(true);
        }
        for (int i = numThrows - numThrown; i < diceImages.Length; ++i)
        {
            diceImages[i].gameObject.SetActive(false);
        }
        //player.enabled = false;

        StartCoroutine(ShowRoll(player, dice));
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator ShowRoll(PlayerController player, GameObject[] dice)
    {
        Rigidbody b1 = dice[0].GetComponent<Rigidbody>();
        Rigidbody b2 = dice[1].GetComponent<Rigidbody>();

        while (b1.velocity.magnitude > 0.1f || b2.velocity.magnitude > 0.1f)
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (luck <= 0)
        {
            yield return StartCoroutine(DoWin(player));
        }
        else if (numThrown >= numThrows)
        {
            yield return StartCoroutine(DoLoss(player));
        }
        else
        {
            yield return StartCoroutine(DoNeutral(player));
        }
    }

    private IEnumerator DoWin(PlayerController player)
    {
        int roll = luck <= -10 ? 13 : 7;
        yield return StartCoroutine(ShowRoll(roll));
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        player.enabled = false;

        int numStars = 0;
        if (numThrown <= threeStarPar) ++numStars;
        if (numThrown <= twoStarPar) ++numStars;
        if (numThrown <= oneStarPar) ++numStars;
        if (roll == 13) numStars = 3;

        for (int i = 0; i < numStars; ++i)
        {
            stars[i].sprite = fullStar;
        }
        for (int i = numStars; i < 3; ++i)
        {
            stars[i].sprite = emptyStar;
        }
    }

    private IEnumerator DoLoss(PlayerController player)
    {
        yield return StartCoroutine(ShowRoll(winningRoll));
        lossScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        player.enabled = false;
    }

    private IEnumerator DoNeutral(PlayerController player)
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
        yield return StartCoroutine(ShowRoll(possibles[roll]));

        //player.enabled = true;
    }

    private IEnumerator ShowRoll(int roll)
    {
        int die1 = Random.Range(Mathf.Max(1, roll - 6), Mathf.Min(roll, 7));
        die1Image.enabled = true;
        die2Image.enabled = true;
        die1Text.text = "" + die1;
        die2Text.text = "" + (roll - die1);
        yield return new WaitForSeconds(diceDisplayDuration);
        die1Image.enabled = false;
        die2Image.enabled = false;
        die1Text.text = string.Empty;
        die2Text.text = string.Empty;
    }
}
