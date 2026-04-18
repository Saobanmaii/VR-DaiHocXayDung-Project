using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CanhBaoCuaController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject canvasKhoangCach; // Nền vàng cảnh báo
    [SerializeField] GameObject canvasBienBao;    // Biển tam giác
    
    [Header("Post Processing / Volume")]
    [SerializeField] Volume triggerWarning;
    [SerializeField] float maxVignetteIntensity = 0.5f; 
    [SerializeField] float vignetteBlinkSpeed = 0.4f;   

    private bool isWarningActive = false; 
    private Sequence warningSequence;

    private Vector3 originalScaleKhoangCach; 
    private Vector3 originalScaleBienBao;

    private Vignette vignetteEffect;
    private Tween vignetteTween;

    void Awake()
    {
        if (canvasKhoangCach != null) originalScaleKhoangCach = canvasKhoangCach.transform.localScale;
        if (canvasBienBao != null) originalScaleBienBao = canvasBienBao.transform.localScale;

        if (triggerWarning != null && triggerWarning.profile.TryGet(out vignetteEffect))
        {
            vignetteEffect.intensity.value = 0f;
        }
    }

    void Start()
    {
        if (canvasKhoangCach != null) canvasKhoangCach.SetActive(false);
        if (canvasBienBao != null) canvasBienBao.SetActive(false);
    }

    // Khi có một object chạm vào vùng trigger của cửa
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem object chạm vào có Tag là "Player" hay không
        if (other.CompareTag("Player"))
        {
            // Lấy instance từ LogicTrigger để kiểm tra trạng thái mặc đồ
            if (LogicTrigger.instance != null)
            {
                // Nếu chưa mặc đủ đồ (ThanhCongVaoCua == false) thì bật cảnh báo
                if (!LogicTrigger.instance.ThanhCongVaoCua && !isWarningActive)
                {
                    ShowWarning();
                }
            }
            else
            {
                Debug.LogWarning("Không tìm thấy LogicTrigger.instance! Hãy đảm bảo script LogicTrigger đã có trên scene.");
            }
        }
    }

    // Khi Player đi lùi ra khỏi vùng cửa
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWarningActive)
            {
                HideWarning();
            }
        }
    }

    private void ShowWarning()
    {
        isWarningActive = true;
        
        if (canvasKhoangCach != null) canvasKhoangCach.SetActive(true);
        if (canvasBienBao != null) canvasBienBao.SetActive(true);

        ResetTransforms();

        warningSequence = DOTween.Sequence();

        // 1. Hiệu ứng UI (Lắc và Phóng to/Thu nhỏ)
        warningSequence.Insert(0, canvasKhoangCach.transform.DOScale(originalScaleKhoangCach * 1.1f, 0.4f).SetEase(Ease.InOutSine))
                       .Insert(0.4f, canvasKhoangCach.transform.DOScale(originalScaleKhoangCach, 0.4f).SetEase(Ease.InOutSine));

        warningSequence.Insert(0, canvasKhoangCach.transform.DOLocalRotate(new Vector3(0, 0, 3f), 0.2f).SetEase(Ease.InOutSine))
                       .Insert(0.2f, canvasKhoangCach.transform.DOLocalRotate(new Vector3(0, 0, -3f), 0.4f).SetEase(Ease.InOutSine))
                       .Insert(0.6f, canvasKhoangCach.transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.InOutSine));

        warningSequence.Insert(0, canvasBienBao.transform.DOScale(originalScaleBienBao * 1.3f, 0.4f).SetEase(Ease.OutBack))
                       .Insert(0.4f, canvasBienBao.transform.DOScale(originalScaleBienBao, 0.4f).SetEase(Ease.InOutSine));

        warningSequence.SetLoops(-1);

        // 2. Hiệu ứng Viền Đỏ (Vignette)
        if (vignetteEffect != null)
        {
            vignetteEffect.intensity.value = 0f; 
            
            vignetteTween = DOTween.To(() => vignetteEffect.intensity.value, 
                                       x => vignetteEffect.intensity.value = x, 
                                       maxVignetteIntensity, 
                                       vignetteBlinkSpeed)
                                   .SetLoops(-1, LoopType.Yoyo)
                                   .SetEase(Ease.InOutSine);
        }
    }

    private void HideWarning()
    {
        isWarningActive = false;

        if (warningSequence != null && warningSequence.IsActive())
        {
            warningSequence.Kill();
        }

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