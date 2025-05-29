using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public AudioSource musicSource;

    // Sessiz olması gereken sahne indexi (sadece 1 sahne için örnek: savaş sahnesi indexi 3)
    public int muteSceneIndex = 3;

    public static MusicManager GetInstance() => instance;
    public static bool InstanceExists() => instance != null;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Sahne değişimlerini dinle
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Oyunun açılış sahnesi için de kontrol et
        CheckMusicState(SceneManager.GetActiveScene().buildIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckMusicState(scene.buildIndex);
    }

    void CheckMusicState(int index)
    {
        Debug.Log("MusicManager: Aktif sahne indexi = " + index);

        if (index == muteSceneIndex)
        {
            Debug.Log("MusicManager: Savaş sahnesi — ses kapatıldı.");
            musicSource.Stop(); // direkt kesiyoruz
        }
        else
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
                Debug.Log("MusicManager: Savaş dışında — müzik tekrar başlatıldı.");
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    

    public void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

}

