using System.Collections; 
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
    [SerializeField] Sprite _default; // Sprite gốc của nút, để reset sau khi đổi màu sai/đúng
    [SerializeField] Canvas canvasDapAnSai;

    [SerializeField] Canvas canvasGiaiThich;
    // Biến khóa nút: Ngăn người chơi bấm lung tung khi đang chạy hiệu ứng
    private bool isAnimating = false;
    
    [SerializeField] List<GameObject> UINotAnswer;
    [SerializeField] List<GameObject> UIAnswer;
    public int indexUIMap = 0;

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
            
            // Âm thanh trả lời sai (Phát 2D, tự động tắt)
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound2D(SoundType.UI_Wrong);
            }
        }
        else
        {
            // Âm thanh trả lời đúng (Phát 2D, tự động tắt)
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
        
        // Chỉ đổi Sprite sang màu sai 1 lần duy nhất, bỏ vòng lặp nhấp nháy
        targetImage.sprite = spriteWrongAnswer;

        // Đợi 0.5s để người chơi kịp nhìn thấy hình ảnh đáp án sai và nghe âm thanh
        yield return new WaitForSeconds(0.5f); 
        targetImage.sprite = _default; // Trả về sprite gốc sau khi đã hiển thị sai
        Debug.Log("===> [SỰ KIỆN]: Đã xong hiệu ứng SAI. Hãy trừ điểm hoặc hiện bảng Game Over tại đây!");
        // canvasDapAnSai.gameObject.SetActive(true);
        // gameObject.SetActive(false);
        
        isAnimating = false; 
    }

    IEnumerator StartAnimCorrectAnswer(Image targetImage)
    {
        UIAnswer[indexUIMap].SetActive(true);
        UINotAnswer[indexUIMap].SetActive(false);
        isAnimating = true; // Bắt đầu khóa input
        
        // CẬP NHẬT: Đổi Sprite sang màu đúng 1 lần duy nhất
        targetImage.sprite = spriteCorrectAnswer;

        // CẬP NHẬT: Đợi 0.5s để đồng bộ với âm thanh và cho người chơi kịp nhìn
        yield return new WaitForSeconds(0.5f);

        Debug.Log(" Câu hỏi tiếp theo hoặc cộng điểm tại đây!, âm thanh cộng điểm");
        
        CheckPointController.instance.AddPoint();
        
        // Trả về sprite gốc nếu sau này object này được bật lại dùng cho câu khác
        targetImage.sprite = _default; 
        
        isAnimating = false; 
        canvasGiaiThich.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}