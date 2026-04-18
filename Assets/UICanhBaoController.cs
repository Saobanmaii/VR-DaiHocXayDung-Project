using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Bắt buộc phải có để gọi được Vignette (Nếu dùng URP)

public class UICanhBaoController : MonoBehaviour
{
    [SerializeField] List<GameObject> vungNguyHiem;
    [SerializeField] GameObject player;
    [SerializeField] float khoangCach = 3f;
    
    [Header("UI Elements")]
    [SerializeField] GameObject canvasKhoangCach; 
    [SerializeField] GameObject canvasBienBao;    
    
    [Header("Post Processing / Volume")]
    [SerializeField] Volume triggerWarning;
    [SerializeField] float maxVignetteIntensity = 0.5f; // Cường độ viền đỏ lớn nhất
    [SerializeField] float vignetteBlinkSpeed = 0.4f;   // Tốc độ nhấp nháy (giây/lần)

    private bool isWarningActive = false; 
    private Sequence warningSequence;

    private Vector3 originalScaleKhoangCach; 
    private Vector3 originalScaleBienBao;

    // Biến lưu trữ hiệu ứng Vignette và Tween của nó
    private Vignette vignetteEffect;
    private Tween vignetteTween;

    void Awake()
    {
        if (canvasKhoangCach != null) originalScaleKhoangCach = canvasKhoangCach.transform.localScale;
        if (canvasBienBao != null) originalScaleBienBao = canvasBienBao.transform.localScale;

        // Lấy component Vignette từ Volume Profile
        if (triggerWarning != null && triggerWarning.profile.TryGet(out vignetteEffect))
        {
            // Đảm bảo viền đỏ tắt hoàn toàn lúc mới chạy game
            vignetteEffect.intensity.value = 0f;
        }
    }

    void Start()
    {
        if (canvasKhoangCach != null) canvasKhoangCach.SetActive(false);
        if (canvasBienBao != null) canvasBienBao.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        bool isNearDanger = false;
        foreach(var hit in vungNguyHiem)
        {
            if (Vector3.Distance(player.transform.position, hit.transform.position) < khoangCach)
            {
                isNearDanger = true;
                break;
            }
        }

        if (isNearDanger && !isWarningActive) ShowWarning();
        else if (!isNearDanger && isWarningActive) HideWarning();
    }

    private void ShowWarning()
    {
        isWarningActive = true;
        
        if (canvasKhoangCach != null) canvasKhoangCach.SetActive(true);
        if (canvasBienBao != null) canvasBienBao.SetActive(true);

        ResetTransforms();

        warningSequence = DOTween.Sequence();

        // 1. Hiệu ứng UI
        warningSequence.Insert(0, canvasKhoangCach.transform.DOScale(originalScaleKhoangCach * 1.1f, 0.4f).SetEase(Ease.InOutSine))
                       .Insert(0.4f, canvasKhoangCach.transform.DOScale(originalScaleKhoangCach, 0.4f).SetEase(Ease.InOutSine));

        warningSequence.Insert(0, canvasKhoangCach.transform.DOLocalRotate(new Vector3(0, 0, 3f), 0.2f).SetEase(Ease.InOutSine))
                       .Insert(0.2f, canvasKhoangCach.transform.DOLocalRotate(new Vector3(0, 0, -3f), 0.4f).SetEase(Ease.InOutSine))
                       .Insert(0.6f, canvasKhoangCach.transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.InOutSine));

        warningSequence.Insert(0, canvasBienBao.transform.DOScale(originalScaleBienBao * 1.3f, 0.4f).SetEase(Ease.OutBack))
                       .Insert(0.4f, canvasBienBao.transform.DOScale(originalScaleBienBao, 0.4f).SetEase(Ease.InOutSine));

        warningSequence.SetLoops(-1);

        // 2. Hiệu ứng Post-Processing Vignette nhấp nháy
        if (vignetteEffect != null)
        {
            vignetteEffect.intensity.value = 0f; // Reset trước khi chạy
            
            // Dùng DOTween.To để thay đổi giá trị float của cường độ Vignette
            vignetteTween = DOTween.To(() => vignetteEffect.intensity.value, 
                                       x => vignetteEffect.intensity.value = x, 
                                       maxVignetteIntensity, 
                                       vignetteBlinkSpeed)
                                   .SetLoops(-1, LoopType.Yoyo) // Yoyo giúp nó tự đập lên rồi hạ xuống mượt mà
                                   .SetEase(Ease.InOutSine);
        }
    }

    private void HideWarning()
    {
        isWarningActive = false;

        // Dừng UI
        if (warningSequence != null && warningSequence.IsActive())
        {
            warningSequence.Kill();
        }

        // Dừng và reset Vignette
        if (vignetteTween != null && vignetteTween.IsActive())
        {
            vignetteTween.Kill();
        }
        if (vignetteEffect != null)
        {
            vignetteEffect.intensity.value = 0f;
        }

        ResetTransforms();
        if (canvasKhoangCach != null) canvasKhoangCach.SetActive(false);
        if (canvasBienBao != null) canvasBienBao.SetActive(false);
    }

    private void ResetTransforms()
    {
        if (canvasKhoangCach != null)
        {
            canvasKhoangCach.transform.localScale = originalScaleKhoangCach;
            canvasKhoangCach.transform.localRotation = Quaternion.identity;
        }

        if (canvasBienBao != null)
        {
            canvasBienBao.transform.localScale = originalScaleBienBao;
            canvasBienBao.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnDestroy()
    {
        if (warningSequence != null) warningSequence.Kill();
        if (vignetteTween != null) vignetteTween.Kill();
    }
}