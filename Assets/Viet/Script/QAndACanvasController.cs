using System.Collections; // Bắt buộc phải có thư viện này để dùng Coroutine
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QAndACanvasController : MonoBehaviour
{
    [SerializeField] CauHoiScripable cauHoiScripable;
    [SerializeField] TextMeshProUGUI textQ;
    [SerializeField] List<TextMeshProUGUI> textAnswer = new List<TextMeshProUGUI>();

    [SerializeField] Sprite spriteWrongAnswer;
    [SerializeField] Sprite spriteCorrectAnswer;
    [SerializeField] Canvas canvasDapAnSai;

    [SerializeField] Canvas canvasGiaiThich;
    // Biến khóa nút: Ngăn người chơi bấm lung tung khi đang chạy hiệu ứng
    private bool isAnimating = false;
    
    
    [SerializeField] List<GameObject> UINotAnswer;
    [SerializeField] List<GameObject> UIAnswer;
    public int indexUIMap=0;
    #if UNITY_EDITOR
    private void OnValidate()
    {
        
        if (UINotAnswer != null)
        {
            for (int i = 0; i < UINotAnswer.Count; i++)
            {
               
                if (UINotAnswer[i] != null) 
                {
                    UINotAnswer[i].SetActive(true);
                }
            }
        }

        if (UIAnswer != null)
        {
            for (int i = 0; i < UIAnswer.Count; i++)
            {
                if (UIAnswer[i] != null) 
                {
                    UIAnswer[i].SetActive(false);
                }
            }
        }
    }
#endif
    void Start()
    {
        
        textQ.text = cauHoiScripable.TextQuestion;
        for (int i = 0; i < cauHoiScripable.TextListAnswer.Count; i++)
        {
            textAnswer[i].text = cauHoiScripable.TextListAnswer[i];
        }
    }

    public void AnswerIndex(int idx)
    {
        if (isAnimating) return;

        Image clickedButtonImage = textAnswer[idx].GetComponentInParent<Image>();
        if (clickedButtonImage == null)
        {
            Debug.LogWarning("Không tìm thấy Image cha ở nút số " + idx);
            return;
        }

        if (idx != cauHoiScripable.correctAnwer)
        {
            Debug.Log("Trả lời đáp án sai");
            StartCoroutine(StartAnimWrongAnswer(clickedButtonImage));
            
            // ĐÃ THÊM: Âm thanh trả lời sai (Phát 2D, tự động tắt)
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound2D(SoundType.UI_Wrong);
            }
        }
        else
        {
            // ĐÃ THÊM: Âm thanh trả lời đúng (Phát 2D, tự động tắt)
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound2D(SoundType.UI_Correct);
            }
            
            Debug.Log("Trả lời đáp án đúng");
            StartCoroutine(StartAnimCorrectAnswer(clickedButtonImage));
        }
    }

    IEnumerator StartAnimWrongAnswer(Image targetImage)
    {
        isAnimating = true; 
        Sprite originalSprite = targetImage.sprite;
        
        // Vòng lặp nhấp nháy trong khoảng 2 giây (Lặp 4 lần, mỗi lần mất 0.5s)
        for (int i = 0; i < 4; i++)
        {
            targetImage.sprite = spriteWrongAnswer;
            yield return new WaitForSeconds(0.25f); 
            
            targetImage.sprite = originalSprite;
            yield return new WaitForSeconds(0.25f); 
        }

        targetImage.sprite = spriteWrongAnswer;

        Debug.Log("===> [SỰ KIỆN]: Đã xong hiệu ứng SAI. Hãy trừ điểm hoặc hiện bảng Game Over tại đây!");
        canvasDapAnSai.gameObject.SetActive(true);
        gameObject.SetActive(false);
        isAnimating = false; 
    }

    IEnumerator StartAnimCorrectAnswer(Image targetImage)
    {
        UIAnswer[indexUIMap].SetActive(true);
        UINotAnswer[indexUIMap].SetActive(false);
        isAnimating = true; // Bắt đầu khóa input
        Sprite originalSprite = targetImage.sprite; 
        
        for (int i = 0; i < 4; i++)
        {
            targetImage.sprite = spriteCorrectAnswer;
            yield return new WaitForSeconds(0.25f);
            
            targetImage.sprite = originalSprite;
            yield return new WaitForSeconds(0.25f);
        }

        targetImage.sprite = spriteCorrectAnswer;

        Debug.Log(" câu hỏi tiếp theo hoặc cộng diemể tai đay!, âm thanh cộng điểm");
        
       
        CheckPointController.instance.AddPoint();
        isAnimating = false; 
        canvasGiaiThich.gameObject.SetActive(true);
        gameObject.SetActive(false);


    }
}