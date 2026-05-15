using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UICanhBaoController : MonoBehaviour
{
    [Header("--- 1. NHỊP ĐIỆU (Thời gian 1 nhịp - Càng nhỏ càng nháy nhanh) ---")]
    [SerializeField] float nhipNhayVang = 0.5f; // Thời gian chớp viền Vignette Vàng
    [SerializeField] float nhipNhayDo = 0.2f;   // Thời gian nháy của UI Đỏ và Vignette Đỏ

    [Header("--- 2. UI ELEMENTS ---")]
    [SerializeField] GameObject uiCanhBaoDo1;        // UI Đỏ 1
    [SerializeField] GameObject uiCanhBaoDo2;        // UI Đỏ 2

    [Header("--- 3. POST PROCESSING ---")]
    [SerializeField] Volume triggerWarning;
    [SerializeField] float maxVignetteIntensity = 0.5f;
    [SerializeField] Color colorVang = Color.yellow;
    [SerializeField] Color colorDo = Color.red;

    // Quản lý số lượng vùng đang đứng bên trong (để xử lý việc vùng nọ lồng vùng kia)
    private int soVungVangDangDung = 0;
    private int soVungDoDangDung = 0;

    // States
    private enum State { AnToan, Vang, Do }
    private State currentState = State.AnToan;

    // Tweens
    private Sequence seqDo;
    private Tween tweenVignette;

    // Cache Transform
    private Vector3 origDo1, origDo2;
    private Vignette vignetteEffect;

    void Awake()
    {
        // Lưu Scale gốc
        if (uiCanhBaoDo1) origDo1 = uiCanhBaoDo1.transform.localScale;
        if (uiCanhBaoDo2) origDo2 = uiCanhBaoDo2.transform.localScale;

        if (triggerWarning != null && triggerWarning.profile.TryGet(out vignetteEffect))
        {
            vignetteEffect.intensity.value = 0f;
        }
    }

    void Start()
    {
        KhởiTạoHiệuỨng();
        CapNhatTrangThaiUI(); // Đảm bảo tắt hết lúc mới bắt đầu
    }

    // XỬ LÝ KHI BƯỚC VÀO COLLIDER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VungVang"))
        {
            soVungVangDangDung++;
            CapNhatTrangThaiUI();
        }
        else if (other.CompareTag("VungDo"))
        {
            soVungDoDangDung++;
            CapNhatTrangThaiUI();
        }
    }

    // XỬ LÝ KHI BƯỚC RA KHỎI COLLIDER
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VungVang"))
        {
            soVungVangDangDung--;
            CapNhatTrangThaiUI();
        }
        else if (other.CompareTag("VungDo"))
        {
            soVungDoDangDung--;
            CapNhatTrangThaiUI();
        }
    }

    // Logic quyết định hiện UI/Hiệu ứng nào dựa trên các vùng đang đứng
    private void CapNhatTrangThaiUI()
    {
        // Đảm bảo số lượng không bị âm do lỗi vật lý
        soVungVangDangDung = Mathf.Max(0, soVungVangDangDung);
        soVungDoDangDung = Mathf.Max(0, soVungDoDangDung);

        // Ưu tiên Vùng Đỏ trước
        if (soVungDoDangDung > 0)
        {
            ChuyenTrangThai(State.Do);
        }
        else if (soVungVangDangDung > 0)
        {
            ChuyenTrangThai(State.Vang);
        }
        else
        {
            ChuyenTrangThai(State.AnToan);
        }
    }

    private void ChuyenTrangThai(State newState)
    {
        if (currentState == newState) return;
        currentState = newState;

        // Tắt hết trước khi bật cái mới
        TatToanBo();

        switch (currentState)
        {
            case State.Vang:
                // Vùng vàng giờ chỉ bật hiệu ứng Vignette chớp vàng
                if (vignetteEffect != null)
                {
                    vignetteEffect.color.value = colorVang;
                    // Dùng timeScale để thay đổi tốc độ nháy
                    tweenVignette.timeScale = 1f / nhipNhayVang; 
                    tweenVignette.Restart();
                }
                break;

            case State.Do:
                if (uiCanhBaoDo1) uiCanhBaoDo1.SetActive(true);
                if (uiCanhBaoDo2) uiCanhBaoDo2.SetActive(true);
                seqDo.Restart();

                if (vignetteEffect != null)
                {
                    vignetteEffect.color.value = colorDo;
                    // Dùng timeScale để thay đổi tốc độ nháy
                    tweenVignette.timeScale = 1f / nhipNhayDo; 
                    tweenVignette.Restart();
                }

                if (AudioManager.Instance != null) AudioManager.Instance.TurnOnWarning();
                break;

            case State.AnToan:
                // Không làm gì thêm, hàm TatToanBo() đã xử lý
                break;
        }
    }

    private void TatToanBo()
    {
        // Tắt UI Đỏ
        if (uiCanhBaoDo1) { uiCanhBaoDo1.SetActive(false); uiCanhBaoDo1.transform.localScale = origDo1; }
        if (uiCanhBaoDo2) { uiCanhBaoDo2.SetActive(false); uiCanhBaoDo2.transform.localScale = origDo2; }
        seqDo.Pause();

        if (AudioManager.Instance != null) AudioManager.Instance.TurnOffWarning();

        // Tắt Vignette (dùng chung cho cả 2 vùng)
        if (tweenVignette != null) tweenVignette.Pause();
        if (vignetteEffect != null) vignetteEffect.intensity.value = 0f;
    }

    private void KhởiTạoHiệuỨng()
    {
        // 1. Sequence Đỏ
        seqDo = DOTween.Sequence().SetAutoKill(false).Pause();
        if (uiCanhBaoDo1 && uiCanhBaoDo2)
        {
            seqDo.Append(uiCanhBaoDo1.transform.DOScale(origDo1 * 1.15f, nhipNhayDo).SetEase(Ease.InOutSine))
                 .Join(uiCanhBaoDo2.transform.DOScale(origDo2 * 1.15f, nhipNhayDo).SetEase(Ease.InOutSine))
                 .Append(uiCanhBaoDo1.transform.DOScale(origDo1, nhipNhayDo).SetEase(Ease.InOutSine))
                 .Join(uiCanhBaoDo2.transform.DOScale(origDo2, nhipNhayDo).SetEase(Ease.InOutSine))
                 .SetLoops(-1);
        }

        // 2. Tween Vignette (Để thời gian gốc là 1f, tốc độ sẽ được chỉnh qua timeScale ở hàm ChuyenTrangThai)
        if (vignetteEffect != null)
        {
            tweenVignette = DOTween.To(() => vignetteEffect.intensity.value, x => vignetteEffect.intensity.value = x, maxVignetteIntensity, 1f)
                                   .SetLoops(-1, LoopType.Yoyo)
                                   .SetEase(Ease.InOutSine)
                                   .SetAutoKill(false).Pause();
        }
    }

    private void OnDestroy()
    {
        if (seqDo != null) seqDo.Kill();
        if (tweenVignette != null) tweenVignette.Kill();
    }
}