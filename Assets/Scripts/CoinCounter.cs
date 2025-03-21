using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;  // Para acesso global (singleton)
    public Text coinText;                // Referência para o Text da UI

    private int coinCount = 0;

    void Awake()
    {
        // Configuração do Singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para adicionar moedas
    public void AddCoin(int value)
    {
        coinCount += value;
        UpdateUI();
    }

    // Atualiza o texto da UI
    void UpdateUI()
    {
        coinText.text = "Moedas: " + coinCount;
    }
}
