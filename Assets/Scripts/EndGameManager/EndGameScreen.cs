using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        resultText.text = PlayerPrefs.GetString("GameResult", "Erro: Sem resultado");
    }
}
