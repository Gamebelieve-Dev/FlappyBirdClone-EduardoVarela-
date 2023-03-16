using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasDeathPanel : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textBestScore;

    public void SetDeathPanel(int score,int bestScore)
    {
        root.SetActive(true);
        textScore.text = score.ToString();
        textBestScore.text = bestScore.ToString();
    }
    public void OnPlayAgainButton()
    {
        root.SetActive(false);        
        BirdController.Instance.Revive();
    }
}
