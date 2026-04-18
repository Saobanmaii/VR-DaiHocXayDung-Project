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

    [SerializeField] Canvas canvasGiaiThich;
    // Biến khóa nút: Ngăn người chơi bấm lung tung khi đang chạy hiệu ứng
    private bool isAnimating = false;

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
        }
        else
        {
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
        
        isAnimating = false; 
    }

    IEnumerator StartAnimCorrectAnswer(Image targetImage)
    {
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

        
        Debug.Log(" câu hỏi tiếp theo hoặc cộng diemể tai đay!");

        isAnimating = false; 
        canvasGiaiThich.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}