using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverManager : MenuManager
{
    [SerializeField] private Text scoreText;
    [SerializeField] private IntVariable score;

    protected override void Start()
    {
        base.Start();
        scoreText.DOText(string.Concat("Score: ", score.RuntimeValue), 0.5f).SetEase(Ease.InCubic);
    }
}
