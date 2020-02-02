using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StudentBehaviour : MonoBehaviour
{
    public Text screamText;
    public Text timmerText;
    public Text scoreText;
    public Image problemBar;
    public RectTransform containerTransform;
    public CanvasGroup canvasGroup;
    public GameObject myCanvas;
    public Collider2D studentCollider;
    private float problemPoints;
    private float startProblemPoints;
    private int score;
    private float timmer = 10f;
    private Sequence sequence;

    [SerializeField] private GameEvent problemSolvedEvent;
    [SerializeField] private GameEvent timeOverEvent;
    [SerializeField] private IntVariable global;
    [SerializeField] private IntVariable lifes;
    [SerializeField] private AudioManager audioPlayer;
    [SerializeField] private Animator animator;
    private bool needHelp;
    public bool NeedHelp
    {
        get
        {
            return needHelp;
        }
    }

    public void SendHelp(string helpText, int problemPoints)
    {
        needHelp = true;
        myCanvas.SetActive(true);
        containerTransform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);
        canvasGroup.DOFade(0.5f, 0.3f).SetEase(Ease.OutCubic);
        timmer = 10f;
        audioPlayer.PlayAudio(0, 0.2f, Random.Range(1.2f, 1.5f));
        animator.SetBool("NeedHelp", true);

        studentCollider.enabled = true;
        screamText.text = helpText;

        this.problemPoints = (float)problemPoints;
        score = problemPoints * 100;
        problemBar.fillAmount = 0;
        startProblemPoints = problemPoints;
        timmerText.text = timmer.ToString();

        sequence = DOTween.Sequence();
        sequence.SetDelay(1);
        sequence.OnStepComplete(() =>
        {
            timmer--;
            timmerText.text = timmer.ToString();
            });
        sequence.SetLoops(10);
        sequence.OnComplete(TimeOver);
    }

    public void GetHelp(int resolvePoints)
    {
        problemPoints -= (float)resolvePoints;
        problemBar.fillAmount = 1f - problemPoints / startProblemPoints;
        audioPlayer.PlayAudio(3, 0.1f, Random.Range(0.8f, 1.2f));
        sequence.Pause();

        if (problemPoints <= 0)
        {
            ProblemSolved();
        }
    }

    private void ProblemSolved()
    {
        studentCollider.enabled = false;
        
        global.RuntimeValue += score;
        audioPlayer.PlayAudio(1, 0.5f, 1f);
        ScoreTextAnimaion();
        problemSolvedEvent.Raise();
    }

    private void TimeOver()
    {
        ClosePopup();
        timmerText.text = string.Empty;
        lifes.RuntimeValue--;
        audioPlayer.PlayAudio(2, 0.5f, Random.Range(1.2f, 1.5f));
        timeOverEvent.Raise();
    }

    private void ClosePopup()
    {
        containerTransform.DOScale(0.3f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => myCanvas.SetActive(false));
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.OutCubic);
        studentCollider.enabled = false;
        needHelp = false;
        animator.SetBool("NeedHelp", false);
    }

    private void ScoreTextAnimaion()
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = string.Concat("+", score.ToString());
        scoreText.rectTransform.DOAnchorPosY(-0.5F, 0);
        scoreText.DOFade(1, 0.2f).SetEase(Ease.InCirc);
        scoreText.DOFade(0, 0.2f).SetEase(Ease.InCirc).SetDelay(0.4f);
        scoreText.rectTransform.DOAnchorPosY(0.5F, 0.5F).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            scoreText.gameObject.SetActive(false);
            ClosePopup();
        });
    }
}
