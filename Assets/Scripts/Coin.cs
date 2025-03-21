using UnityEngine;
using System.Collections;


public class Coin : MonoBehaviour
{
    public float raioSpawn = 10f;  // Raio da área de spawn ao redor do centro
    public LayerMask layerChao;    // Layer do chão
    public AudioSource coinSound;

    void Start()
    {
        // Debug.Log("LayerChao: " + layerChao.value); 
        Reposicionar();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona 1 moeda ao contador
            if (CoinCounter.instance != null)
            {
                CoinCounter.instance.AddCoin(1);
            }

            if (coinSound != null)
            {
                coinSound.Play();
            }

            StartCoroutine(DesativarAposSom());
            
            Reposicionar();
        }
    }

    IEnumerator DesativarAposSom()
    {
        yield return new WaitForSeconds(coinSound.clip.length);
    }

    void Reposicionar()
    {
        Vector3 novaPosicao = GerarPosicaoAleatoria();
        transform.position = novaPosicao;
    }

    Vector3 GerarPosicaoAleatoria()
    {
        for (int i = 0; i < 10; i++) // Tenta encontrar uma posição válida
        {
            float x = Random.Range(-raioSpawn, raioSpawn);
            float z = Random.Range(-raioSpawn, raioSpawn);
            Vector3 posicaoTentativa = new Vector3(x, 100f, z); // Começa do alto

            if (Physics.Raycast(posicaoTentativa, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerChao))
            {
                if(hit.collider != null)
                {
                    // Debug.Log($"Raycast atingiu: {hit.collider.gameObject.name} na posição {hit.point}");
                    return new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
                }
            }
        }
        
        return transform.position;
    }

}
