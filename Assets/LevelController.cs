using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private TimerController timerController;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private int ShowLevelTextDurationSec = 3;

    private int levelNum = 1;

    private void Start()
    {
        StartLevel();
    }

    public async void StartLevel(bool isWin = false)
    {
        if(isWin) levelNum++;

        levelText.text = $"Level {levelNum}";
        
        await Task.Delay(TimeSpan.FromSeconds(ShowLevelTextDurationSec));
        
        levelText.text = "";

        timerController.SetupTimer();
    }
}
