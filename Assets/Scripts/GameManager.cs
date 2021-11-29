using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;

    [SerializeField]
    private FruitFactory fruitFactory;
    [SerializeField]
    private TimerController timerController;
    [SerializeField]
    private HumanBehaviour humanBehaviour;
    [SerializeField]
    private LevelController levelController;

    [SerializeField]
    private TMP_Text textScore;
    [SerializeField]
    private TMP_Text resultText;
    [SerializeField]
    private int countOfFruicts;

    private bool isWin = false;

    public void FructCollected()
    {
        score++;
        UpdateScore();
    }

    private void Awake()
    {
        fruitFactory.CreateFruits(countOfFruicts);

        timerController.UpdateTimer += LevelStart;
        timerController.TimerEnded += Restart;
    }

    private async void Restart()
    {
        LevelResult();
        fruitFactory.RemoveAllFruits();
        humanBehaviour.ResetPosition();
        humanBehaviour.ResetSpeedText();
        timerController.ResetTimer();
        humanBehaviour.enabled = false;
        ResetScore();

        await Task.Delay(TimeSpan.FromSeconds(3));

        levelController.StartLevel(isWin);
        
        if (isWin) countOfFruicts++;

        fruitFactory.CreateFruits(countOfFruicts);
    }

    private void LevelResult()
    {
        string result = "You Win";
        isWin = true;

        if (score < countOfFruicts)
        {
            result = "Wasted";
            isWin = false;
        }

        resultText.text = result;
    }

    private void UpdateScore()
    {
        textScore.text = $"{score}/{countOfFruicts}";
        if (score == countOfFruicts)
        {
            timerController.ResetTimer();
        }
    }

    private void ResetScore()
    {
        score = 0;
        textScore.text = "";
    }

    private void LevelStart()
    {
        UpdateScore();

        humanBehaviour.enabled = true;
        humanBehaviour.UpdateSpeedText();
    }
}
