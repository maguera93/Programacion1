using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image loadImage;

    protected virtual void Start()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        loadImage.DOFade(0, 0.5f).OnComplete(() => loadImage.gameObject.SetActive(false));
    }

    public void FadeOut()
    {
        loadImage.gameObject.SetActive(true);
        loadImage.DOFade(1, 0.5f).OnComplete(() => SceneManager.LoadScene("Gameplay"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
