using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;      // Prefab da moeda
    public int quantidadeMoedas = 10;  // Quantidade de moedas a serem instanciadas
    public float raioMin = 3f;         // Raio mínimo
    public float raioMax = 10000f;     // Raio máximo (10k para testar)
    public LayerMask layerChao;        // Layer do chão para Raycast
    public Transform playerTransform;  // Referência ao jogador

    private List<Vector3> posicoesUsadas = new List<Vector3>(); // Lista para evitar sobreposição

    void Start()
    {
        for (int i = 0; i < quantidadeMoedas; i++)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        Vector3 posicao = GerarPosicaoAleatoria();
        GameObject novaMoeda = Instantiate(coinPrefab, posicao, Quaternion.identity);
        posicoesUsadas.Add(posicao); // Armazena a posição para evitar sobreposição

        // Debug para verificar a posição das moedas no editor
        // Debug.Log($"Moeda gerada em: {posicao}");
    }

    Vector3 GerarPosicaoAleatoria()
    {
        for (int i = 0; i < 10; i++) // Tenta encontrar uma posição válida
        {
            float angulo = Random.Range(0f, 360f); // Ângulo aleatório
            float distancia = Mathf.Sqrt(Random.Range(0f, 1f)) * (raioMax - raioMin) + raioMin; // Distribuição uniforme
            float x = playerTransform.position.x + Mathf.Cos(angulo * Mathf.Deg2Rad) * distancia;
            float z = playerTransform.position.z + Mathf.Sin(angulo * Mathf.Deg2Rad) * distancia;
            Vector3 posicaoTentativa = new Vector3(x, 100f, z); // Raycast começa do alto

            // Debug.Log($"Tentando spawn em: {posicaoTentativa}"); // Debug para testar o raio

            if (Physics.Raycast(posicaoTentativa, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerChao))
            {
                Vector3 spawnPos = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);

                // Verifica se já existe uma moeda próxima
                bool muitoPerto = posicoesUsadas.Exists(pos => Vector3.Distance(pos, spawnPos) < 1.5f);

                if (!muitoPerto)
                {
                    return spawnPos;
                }
            }
        }

        // Se não encontrar posição válida, gera aleatoriamente dentro do raio máximo
        float fallbackAngulo = Random.Range(0f, 360f);
        float fallbackDistancia = Random.Range(raioMin, raioMax);
        float fallbackX = playerTransform.position.x + Mathf.Cos(fallbackAngulo * Mathf.Deg2Rad) * fallbackDistancia;
        float fallbackZ = playerTransform.position.z + Mathf.Sin(fallbackAngulo * Mathf.Deg2Rad) * fallbackDistancia;
        Vector3 fallbackPos = new Vector3(fallbackX, 2f, fallbackZ);

        // Debug.LogWarning($"Usando fallback para spawn em: {fallbackPos}");

        return fallbackPos;
    }
}
