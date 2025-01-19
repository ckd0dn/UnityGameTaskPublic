using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : UIBase
{
    [SerializeField] private Image Img;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI gradeText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI healthText;

    public void SetupTooltip(Sprite sprite, string name, string grade, string speed, string health)
    {
        Img.sprite = sprite;
        nameText.text = name;
        gradeText.text = $"등급 {grade}";
        speedText.text = $"스피드 {speed}";
        healthText.text = $"체력 {health}"; 
    }


}
