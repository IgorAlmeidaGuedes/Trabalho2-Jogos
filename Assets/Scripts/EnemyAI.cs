using UnityEngine;
using UnityEngine.AI;
using ASD; 

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger ativado com: " + other.gameObject.name);
        Debug.Log(other.tag); 
        if (other.gameObject.GetComponent<ASD.FootControllerIK>() != null)
        {
            Debug.Log("AAAAAAAAAAAAAAAAA");
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                Debug.Log("Chamando PlayerDied()");
                gameManager.PlayerDied();
            }
            else
            {
                Debug.LogError("GameManager não encontrado!");
            }
        }
        else
        {
            Debug.Log("O objeto NÃO é o Player.");
        }
    }




    private void OnCollisionStay(Collision collision) // Para testar se a colisão persiste
    {
        Debug.Log("Mantendo contato com: " + collision.gameObject.name);
    }
}
