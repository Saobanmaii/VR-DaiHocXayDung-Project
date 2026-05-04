using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Định nghĩa sẵn các loại âm thanh để gọi cho dễ, không lo gõ sai tên String
public enum SoundType
{
    WarningErr,             // err err
    WarningNoPPE,           // Chưa trang bị đầy đủ đồ bảo hộ
    WarningDistance,        // Vi phạm khoảng cách an toàn
    PlayerMove,             // Âm thanh khi di chuyển
    TouchCone,              // Khi chạm vào cone
    SocketCone,             // Khi đặt cone vào socket
    ExcavatorEngine,        // Âm thanh máy xúc (tiếng động cơ)
    ExcavatorDig,           // Âm thanh xúc đất
    UI_Select,              // Khi chọn đáp án
    UI_Correct,             // Khi trả lời đúng
    UI_Wrong,               // Khi trả lời sai
    ElectricShock,          // Tiếng điện giật
    PlayerFall,              // Âm thanh khi player ngã
    GateOpen // âm thanh mổ công
}

[System.Serializable]
public class SoundDetail
{
    public SoundType soundType;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool isLoop = false;
    
    [Header("Cài đặt 3D")]
    public float minDistance = 1f; // Khoảng cách bắt đầu nhỏ dần
    public float maxDistance = 20f; // Khoảng cách không còn nghe thấy gì
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Danh sách Âm thanh")]
    public List<SoundDetail> soundList = new List<SoundDetail>();

    [Header("Cài đặt Pool")]
    public int initialPoolSize = 10; // Số lượng AudioSource tạo sẵn
    
    private List<AudioSource> audioSourcePool = new List<AudioSource>();
    private GameObject poolContainer;

    // 1. Khai báo 1 biến để lưu lại âm thanh cảnh báo
    private AudioSource warningSound;

    // 2. Lệnh để BẬT âm thanh (Lưu ý phải gán vào biến warningSound)
    public void TurnOnWarning()
    {
        warningSound = AudioManager.Instance.PlaySound2D(SoundType.WarningErr);
    }

    // 3. Lệnh để TẮT âm thanh
    public void TurnOffWarning()
    {
        // Gọi hàm StopSound có sẵn trong AudioManager
        AudioManager.Instance.StopSound(warningSound);
    }

    private void Awake()
    {
        // Setup Singleton chuẩn
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ qua các Scene nếu cần
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    

    // Tạo sẵn các AudioSource ẩn để dùng chung
    private void InitializePool()
    {
        poolContainer = new GameObject("AudioSource_Pool");
        poolContainer.transform.SetParent(this.transform);

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewAudioSource();
        }
    }

    private AudioSource CreateNewAudioSource()
    {
        GameObject obj = new GameObject("Pooled_AudioSource");
        obj.transform.SetParent(poolContainer.transform);
        AudioSource source = obj.AddComponent<AudioSource>();
        source.playOnAwake = false;
        audioSourcePool.Add(source);
        return source;
    }

    // Tìm AudioSource đang rảnh rỗi
    private AudioSource GetAvailableSource()
    {
        foreach (var source in audioSourcePool)
        {
            if (!source.isPlaying) return source;
        }
        // Nếu thiếu thì tự tạo thêm
        return CreateNewAudioSource();
    }

    private SoundDetail GetSoundDetail(SoundType type)
    {
        return soundList.Find(s => s.soundType == type);
    }

    /// <summary>
    /// PHÁT ÂM THANH 3D TẠI MỘT VỊ TRÍ CỤ THỂ (Xa nhỏ, gần to)
    /// </summary>
    public AudioSource PlaySound3D(SoundType type, Vector3 position)
    {
        SoundDetail soundInfo = GetSoundDetail(type);
        if (soundInfo == null || soundInfo.clip == null) return null;

        AudioSource source = GetAvailableSource();
        
        source.transform.position = position;
        source.clip = soundInfo.clip;
        source.volume = soundInfo.volume;
        source.loop = soundInfo.isLoop;
        
        // Cấu hình 3D
        source.spatialBlend = 1f; // 1 = Hoàn toàn 3D
        source.rolloffMode = AudioRolloffMode.Linear; // Giảm âm lượng tuyến tính
        source.minDistance = soundInfo.minDistance;
        source.maxDistance = soundInfo.maxDistance;

        source.Play();

        // Nếu không lặp thì tự động tắt khi phát xong (để trả về Pool)
        if (!soundInfo.isLoop)
        {
            StartCoroutine(ReturnToPoolAfterDelay(source, soundInfo.clip.length));
        }

        return source;
    }

    /// <summary>
    /// PHÁT ÂM THANH 2D (Nghe rõ mồn một ở mọi nơi - Dành cho UI, Cảnh báo hệ thống)
    /// </summary>
    public AudioSource PlaySound2D(SoundType type) // Đổi void thành AudioSource
    {
        SoundDetail soundInfo = GetSoundDetail(type);
        if (soundInfo == null || soundInfo.clip == null) return null;

        AudioSource source = GetAvailableSource();
        
        source.clip = soundInfo.clip;
        source.volume = soundInfo.volume;
        source.loop = soundInfo.isLoop;
        
        // Cấu hình 2D
        source.spatialBlend = 0f; 
        
        source.Play();

        if (!soundInfo.isLoop)
        {
            StartCoroutine(ReturnToPoolAfterDelay(source, soundInfo.clip.length));
        }

        return source; // THÊM DÒNG NÀY ĐỂ TRẢ VỀ
    }

    // Cho phép tắt một âm thanh đang lặp (Ví dụ tắt tiếng máy xúc khi nó dừng)
    public void StopSound(AudioSource sourceToStop)
    {
        if (sourceToStop != null && sourceToStop.isPlaying)
        {
            sourceToStop.Stop();
        }
    }

    private IEnumerator ReturnToPoolAfterDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Stop();
        source.clip = null; // Dọn dẹp
    }
}