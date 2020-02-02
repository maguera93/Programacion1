using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private IntVariable scoreValue;
    [SerializeField] private Text scoreText;
    [Header("Lives")]
    [SerializeField] private IntVariable lifesValue;
    [SerializeField] private Image[] lifesImage;
    [SerializeField] private Image hurtImage;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;
    [Header("Load")]
    [SerializeField] private Image loadImage;

    private void Start()
    {
        OnScoreChange();
        scoreValue.RuntimeValue = scoreValue.InitialValue;
        FadeIn();
    }

    public void OnScoreChange()
    {
        scoreText.text = string.Concat("Score: ", scoreValue.RuntimeValue.ToString());
    }

    public void OnTimeOver()
    {
        for (int i = 0; i < lifesImage.Length; i++)
        {
            lifesImage[i].sprite = emptyHeart;
        }

        for (int i = 0; i < lifesValue.RuntimeValue; i++)
        {
            lifesImage[i].sprite = fullHeart;
        }

        if (lifesValue.RuntimeValue <= 0)
        {
            FadeOut();
        }

        HurtFade();
    }

    private void FadeIn()
    {
        loadImage.DOFade(0, 0.5f).OnComplete(() => loadImage.gameObject.SetActive(false));
    }

    private void FadeOut()
    {
        loadImage.DOFade(0, 0.5f).OnComplete(() => SceneManager.LoadScene("GameOver"));
    }

    private void HurtFade()
    {
        hurtImage.gameObject.SetActive(true);
        hurtImage.DOFade(0.8f, 0.1f).SetEase(Ease.OutExpo);
        hurtImage.DOFade(0, 0.1f).SetEase(Ease.InExpo).SetDelay(0.1f).OnComplete(() => hurtImage.gameObject.SetActive(false));
    }
}
