using UnityEngine;
using UnityEngine.Video;

public class IntroOverlay : MonoBehaviour
{
    private static bool hasPlayedThisRun = false;

    [Header("Referanslar")]
    public VideoPlayer videoPlayer;   // IntroVideo GameObject’indeki VideoPlayer
    public GameObject introCanvas;    // Video + Skip UI
    public GameObject menuPanel;      // Ana menü paneli

     [SerializeField] private AudioSource musicSource;   // ★ Inspector’dan atayacaksın

    // -------------------- AWAKE --------------------
    void Awake()
    {
        if (hasPlayedThisRun)          // ▶ İlk açılış DEĞİL → intro atla
        {
            SkipCompletely();
            return;
        }

        // ▶ İlk kez çalışıyor → intro göster
        menuPanel.SetActive(false);
        introCanvas.SetActive(true);

        videoPlayer.time = 0;
        videoPlayer.isLooping = true;  // ★ Bittiğinde tekrar başa sar
        videoPlayer.Play();
    }

    // Skip butonundan çağrılır
    public void SkipIntro() => CloseIntro();

    // -------------------- INTRO’YU KAPAT --------------------
    void CloseIntro()
    {
        // 1) Video + bütün ses kanallarını kesin durdur
        videoPlayer.Pause();
        for (ushort i = 0; i < videoPlayer.audioTrackCount; i++)
        {
            videoPlayer.SetDirectAudioMute(i, true);
            var src = videoPlayer.GetTargetAudioSource(i);
            if (src != null) src.Stop();
        }

        // 2) VideoPlayer GameObject’ini tamamen sil
        Destroy(videoPlayer.gameObject);

        // 3) UI geçişi
        introCanvas.SetActive(false);
        menuPanel.SetActive(true);

        // 4) ▶▶  BAYRAĞI ŞİMDİ kaldır
        hasPlayedThisRun = true;

          UnmuteMusic();        
    }

    // -------------------- INTRO’YU TAMAMEN ATLAMA --------------------
    void SkipCompletely()
    {
        if (videoPlayer != null) Destroy(videoPlayer.gameObject);
        introCanvas.SetActive(false);
        menuPanel.SetActive(true);
         UnmuteMusic();        
    }


      private void UnmuteMusic()     // ★
    {
        if (!musicSource) return;

        musicSource.mute = false;          // tiki kaldır
        if (!musicSource.isPlaying)        // hâlâ çalmıyorsa başlat
            musicSource.Play();
    }

}
