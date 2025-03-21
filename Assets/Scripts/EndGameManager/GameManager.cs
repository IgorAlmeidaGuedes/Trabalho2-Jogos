using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public Text timerText; // UI do contador
    public float totalTime = 180f; // 3 minutos
    private float timeLeft;
    private bool gameEnded = false;
    private string gameResult; // "Voce ganhou" ou "Voce perdeu"

    void Start()
    {
        timeLeft = totalTime;
        StartCoroutine(WaitForCamera());
    }

    IEnumerator WaitForCamera()
    {
        yield return new WaitForSeconds(0.1f); // Pequeno delay para garantir que a câmera foi criada/movida
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.worldCamera = Camera.main; // Garante que o Canvas use a câmera correta
        }
    }

    void Update()
    {
        if (!gameEnded)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString() + "s";

            if (timeLeft <= 0)
            {
                EndGame(true); // Tempo acabou, vitória
            }
        }
    }

    public void PlayerDied()
    {
        EndGame(false); // Player morreu, derrota
    }

    private void EndGame(bool won)
    {
        gameEnded = true;
        gameResult = won ? "Voce ganhou" : "Voce perdeu";
        PlayerPrefs.SetString("GameResult", gameResult); // Salva o resultado para a próxima cena
        SceneManager.LoadScene("EndGame"); // Muda para a cena final
    }
}
