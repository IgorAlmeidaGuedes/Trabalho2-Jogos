using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene("StartGame");    
    }
}
