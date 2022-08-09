using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private AudioSource audioSource = null;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        SceneManager.LoadScene(1);
    }

}
