using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UICanhBaoController : MonoBehaviour
{
    [SerializeField] List<GameObject> vungNguyHiem;
    [SerializeField] GameObject player;
    
    [Header("--- 1. CÁC MỐC KHOẢNG CÁCH ---")]
    [SerializeField] float khoangCachVang = 5f;       // Mốc 1: Bắt đầu nháy vàng
    [SerializeField] float khoangCachBienBao = 3f;    // Mốc 2: Biển báo xuất hiện
    [SerializeField] float khoangCachDo = 1.2f;       // Mốc 3: Chuyển sang nháy đỏ

    [Header("--- 2. TỐC ĐỘ ĐỘNG (Càng gần càng nhanh) ---")]
    [SerializeField] float tocDoNhayBinhThuong = 1f;  // Tốc độ nháy khi ở mốc Vàng
    [SerializeField] float tocDoNhayToiDa = 3f;       // Tốc độ nháy (Scale/Rotate) nhanh nhất khi chạm mốc Đỏ

    [Header("--- 3. UI ELEMENTS ---")]
    [SerializeField] GameObject canvasVang;           // UI Cảnh báo Vàng
    [SerializeField] GameObject canvasBienBao;        // UI Biển báo
    [SerializeField] GameObject uiCanhBaoDo1;         // UI Đỏ 1
    [SerializeField] GameObject uiCanhBaoDo2;         // UI Đỏ 2

    [Header("--- 4. POST PROCESSING ---")]
    [SerializeField] Volume triggerWarning;
    [SerializeField] float maxVignetteIntensity = 0.5f; 
    [SerializeField] Color colorVang = Color.yellow; 
    [SerializeField] Color colorDo = Color.red;    

    // States
    private bool isVangActive, isBienBaoActive, isDoActive; 

    // Tweens (Khởi tạo 1 lần để tối ưu hiệu suất)
    private Sequence seqVang, seqBienBao, seqDo;
    private Tween tweenVignette;

    // Cache Transform
    private Vector3 origVang, origBienBao, origDo1, origDo2; 
    private Vignette vignetteEffect;

    void Awake()
    {
        // Lưu Scale gốc
        if (canvasVang) origVang = canvasVang.transform.localScale;
        if (canvasBienBao) origBienBao = canvasBienBao.transform.localScale;
        if (uiCanhBaoDo1) origDo1 = uiCanhBaoDo1.transform.localScale;
        if (uiCanhBaoDo2) origDo2 = uiCanhBaoDo2.transform.localScale;

        if (triggerWarning != null && triggerWarning.profile.TryGet(out vignetteEffect))
        {
            vignetteEffect.intensity.value = 0f;
        }
    }

    void Start()
    {
        TắtToànBộUI();
        KhởiTạoHiệuỨng();
    }

    void Update()
    {
        if (player == null || vungNguyHiem.Count == 0) return;

        float minDistance = float.MaxValue; 
        foreach(var hit in vungNguyHiem)
        {
            float dist = Vector3.Distance(player.transform.position, hit.transform.position);
            if (dist < minDistance) minDistance = dist;
        }

        // Nếu hoàn toàn nằm ngoài vùng nguy hiểm -> Tắt hết
        if (minDistance > khoangCachVang)
        {
            if (isVangActive || isBienBaoActive || isDoActive) TắtToànBộUI();
            return;
        }

        // --- CẬP NHẬT TỐC ĐỘ ĐỘNG THEO KHOẢNG CÁCH ---
        // Tính toán tỷ lệ phần trăm khoảng cách (0 = Đang ở mốc Vàng, 1 = Đã chạm mốc Đỏ)
        float distancePercent = Mathf.InverseLerp(khoangCachVang, khoangCachDo, minDistance);
        
        // Tốc độ sẽ tăng dần từ tocDoNhayBinhThuong lên tocDoNhayToiDa
        float currentSpeedMultiplier = Mathf.Lerp(tocDoNhayBinhThuong, tocDoNhayToiDa, distancePercent);

        // Áp dụng tốc độ động cho tất cả các UI đang chạy
        seqVang.timeScale = currentSpeedMultiplier;
        seqBienBao.timeScale = currentSpeedMultiplier;
        seqDo.timeScale = currentSpeedMultiplier;
        if (tweenVignette != null) tweenVignette.timeScale = currentSpeedMultiplier;

        // Cập nhật màu Vignette mượt mà từ Vàng sang Đỏ
        if (vignetteEffect != null)
        {
            vignetteEffect.color.value = Color.Lerp(colorVang, colorDo, distancePercent);
        }

        // --- KIỂM TRA TỪNG MỐC ĐỂ BẬT/TẮT UI ---
        ToggleVang(minDistance <= khoangCachVang);
        ToggleBienBao(minDistance <= khoangCachBienBao);
        ToggleDo(minDistance <= khoangCachDo);
    }

    private void ToggleVang(bool show)
    {
        if (isVangActive == show) return;
        isVangActive = show;

        if (show) 
        {
            if (canvasVang) canvasVang.SetActive(true);
            seqVang.Restart();
            if (tweenVignette != null) tweenVignette.Restart();
        }
        else 
        {
            if (canvasVang) { canvasVang.SetActive(false); canvasVang.transform.localScale = origVang; }
            seqVang.Pause();
            if (tweenVignette != null) tweenVignette.Pause();
            if (vignetteEffect != null) vignetteEffect.intensity.value = 0f;
        }
    }

    private void ToggleBienBao(bool show)
    {
        if (isBienBaoActive == show) return;
        isBienBaoActive = show;

        if (show)
        {
            if (canvasBienBao) canvasBienBao.SetActive(true);
            seqBienBao.Restart();
        }
        else
        {
            if (canvasBienBao) 
            { 
                canvasBienBao.SetActive(false); 
                canvasBienBao.transform.localScale = origBienBao;
                canvasBienBao.transform.localRotation = Quaternion.identity;
            }
            seqBienBao.Pause();
        }
    }

    private void ToggleDo(bool show)
    {
        if (isDoActive == show) return;
        isDoActive = show;

        if (show)
        {
            if (uiCanhBaoDo1) uiCanhBaoDo1.SetActive(true);
            if (uiCanhBaoDo2) uiCanhBaoDo2.SetActive(true);
            seqDo.Restart();

            // ---> ĐÃ THÊM: BẬT ÂM THANH KHI VÀO VÙNG ĐỎ <---
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.TurnOnWarning();
            }
        }
        else
        {
            if (uiCanhBaoDo1) { uiCanhBaoDo1.SetActive(false); uiCanhBaoDo1.transform.localScale = origDo1; }
            if (uiCanhBaoDo2) { uiCanhBaoDo2.SetActive(false); uiCanhBaoDo2.transform.localScale = origDo2; }
            seqDo.Pause();

            // ---> ĐÃ THÊM: TẮT ÂM THANH KHI THOÁT KHỎI VÙNG ĐỎ <---
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.TurnOffWarning();
            }
        }
    }

    private void TắtToànBộUI()
    {
        ToggleDo(false);
        ToggleBienBao(false);
        ToggleVang(false);
    }


    private void KhởiTạoHiệuỨng()
    {
        // 1. Sequence Vàng
        seqVang = DOTween.Sequence().SetAutoKill(false).Pause();
        if (canvasVang)
        {
            seqVang.Append(canvasVang.transform.DOScale(origVang * 1.1f, 0.4f).SetEase(Ease.InOutSine))
                   .Append(canvasVang.transform.DOScale(origVang, 0.4f).SetEase(Ease.InOutSine))
                   .SetLoops(-1);
        }

        // 2. Sequence Biển Báo (Scale + Lắc)
        seqBienBao = DOTween.Sequence().SetAutoKill(false).Pause();
        if (canvasBienBao)
        {
            seqBienBao.Append(canvasBienBao.transform.DOScale(origBienBao * 1.1f, 0.4f).SetEase(Ease.OutBack))
                      .Join(canvasBienBao.transform.DOLocalRotate(new Vector3(0, 0, 3f), 0.2f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo))
                      .Append(canvasBienBao.transform.DOScale(origBienBao, 0.4f).SetEase(Ease.InOutSine))
                      .SetLoops(-1);
        }

        // 3. Sequence Đỏ
        seqDo = DOTween.Sequence().SetAutoKill(false).Pause();
        if (uiCanhBaoDo1 && uiCanhBaoDo2)
        {
            seqDo.Append(uiCanhBaoDo1.transform.DOScale(origDo1 * 1.15f, 0.3f).SetEase(Ease.InOutSine))
                 .Join(uiCanhBaoDo2.transform.DOScale(origDo2 * 1.15f, 0.3f).SetEase(Ease.InOutSine))
                 .Append(uiCanhBaoDo1.transform.DOScale(origDo1, 0.3f).SetEase(Ease.InOutSine))
                 .Join(uiCanhBaoDo2.transform.DOScale(origDo2, 0.3f).SetEase(Ease.InOutSine))
                 .SetLoops(-1);
        }

        // 4. Tween Vignette
        if (vignetteEffect != null)
        {
            tweenVignette = DOTween.To(() => vignetteEffect.intensity.value, x => vignetteEffect.intensity.value = x, maxVignetteIntensity, 0.8f)
                                   .SetLoops(-1, LoopType.Yoyo)
                                   .SetEase(Ease.InOutSine)
                                   .SetAutoKill(false).Pause();
        }
    }

    private void OnDestroy()
    {
        if (seqVang != null) seqVang.Kill();
        if (seqBienBao != null) seqBienBao.Kill();
        if (seqDo != null) seqDo.Kill();
        if (tweenVignette != null) tweenVignette.Kill();
    }
}