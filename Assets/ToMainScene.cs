using UnityEngine;
using UnityEngine.SceneManagement;
public class ToMainScene : MonoBehaviour
{
    public void MoveToMainScene()
    {
        SceneManager.LoadScene(0);
    }
}
