using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasOnGameController : MonoBehaviour
{
    public static CanvasOnGameController Instance;
    [SerializeField] private  Canvas canvasRoot;
    [SerializeField] private  TextMeshProUGUI currentScoreText;
    [SerializeField] private  TextMeshProUGUI bestScoreText;
    [SerializeField] private  GameObject resumeTextRoot;
    [SerializeField] private  TextMeshProUGUI resumeText;

    [SerializeField] private GameObject highScoreBackMenuPanel;
    [SerializeField] private TextMeshProUGUI highScoreBackMenuText;


    [SerializeField] private  GameObject pausePanel;
    [SerializeField] private  GameObject mainMenuPanel;
    [field: SerializeField] public CanvasDeathPanel deathPanel { get; private set; }
    private Coroutine resumeProccesCoroutine;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        int bestScoreData = PlayerPrefs.GetInt("bestScore");
        UpdateBestScore(bestScoreData);
    }
    public void UpdateCurrentScore(int score)
    {
        currentScoreText.text = score.ToString();
    }
    public void UpdateBestScore(int score)
    {
        bestScoreText.text = score.ToString();
    }
    public void SetActiveCanvas(bool value)
    {
        canvasRoot.enabled = value;
    }
    public void OnButtonPlay()
    {
        LevelController.Instance.ResetPipes();

        resumeProccesCoroutine = StartCoroutine(ResumeProcces(2));

        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void OnButtonBackToMainMenu()
    {
        StartCoroutine(BackMenuProcces());
    }
    public void OpenPause(bool value)
    {
        if (value)
        {
            pausePanel.SetActive(true);
            BirdController.Instance.SetPause(true);
            LevelController.Instance.SetAllowUpdatePosition(false);
        }
        else
        {
            pausePanel.SetActive(false);

            if (resumeProccesCoroutine != null)
                StopCoroutine(resumeProccesCoroutine);
            resumeProccesCoroutine = StartCoroutine(ResumeProcces(3));
        }
    }
    private IEnumerator BackMenuProcces()
    {
        pausePanel.SetActive(false);
        highScoreBackMenuPanel.SetActive(true);
        highScoreBackMenuText.text = BirdController.Instance.bestScore.ToString();
        yield return new WaitForSeconds(1.5f);
        mainMenuPanel.SetActive(true);
        highScoreBackMenuPanel.SetActive(false);
    }
    private IEnumerator ResumeProcces(int timeWait)
    {
        resumeTextRoot.SetActive(true);
        while (timeWait > 0)
        {
            resumeText.text = timeWait.ToString();
            yield return new WaitForSeconds(1);
            timeWait--;
        }
        resumeTextRoot.SetActive(false);
        LevelController.Instance.SetAllowUpdatePosition(true);
        BirdController.Instance.SetPause(false);
    }
}
