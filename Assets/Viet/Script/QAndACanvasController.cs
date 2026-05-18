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

    // Dùng OnEnable thay vì Start để mỗi khi bật Canvas này lên nó sẽ cập nhật đúng ngôn ngữ
    void OnEnable()
    {
        UpdateLanguage();
    }

    // Hàm cập nhật ngôn ngữ cho câu hỏi và đáp án
    public void UpdateLanguage()
    {
        // Kiểm tra xem hiện tại đang chọn tiếng Việt hay tiếng Anh
        bool isVN = LanguageController.instance != null ? LanguageController.instance.switchVN : true;

        if (isVN)
        {
            textQ.text = cauHoiScripable.TextQuestion;
            for (int i = 0; i < cauHoiScripable.TextListAnswer.Count; i++)
            {
                if (i < textAnswer.Count)
                    textAnswer[i].text = cauHoiScripable.TextListAnswer[i];
            }
        }
        else
        {
            textQ.text = cauHoiScripable.TextQuestion_EN;
            for (int i = 0; i < cauHoiScripable.TextListAnswer_EN.Count; i++)
            {
                if (i < textAnswer.Count)
                    textAnswer[i].text = cauHoiScripable.TextListAnswer_EN[i];
            }
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
            
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound2D(SoundType.UI_Wrong);
            }
        }
        else
        {
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
        
        targetImage.sprite = spriteWrongAnswer;

        yield return new WaitForSeconds(0.5f); 
        targetImage.sprite = _default; 
        Debug.Log("===> [SỰ KIỆN]: Đã xong hiệu ứng SAI. Hãy trừ điểm hoặc hiện bảng Game Over tại đây!");
        
        isAnimating = false; 
    }

    IEnumerator StartAnimCorrectAnswer(Image targetImage)
    {
        UIAnswer[indexUIMap].SetActive(true);
        UINotAnswer[indexUIMap].SetActive(false);
        isAnimating = true; 
        
        targetImage.sprite = spriteCorrectAnswer;

        yield return new WaitForSeconds(0.5f);

        Debug.Log(" Câu hỏi tiếp theo hoặc cộng điểm tại đây!, âm thanh cộng điểm");
        
        if(CheckPointController.instance != null)
            CheckPointController.instance.AddPoint();
        
        targetImage.sprite = _default; 
        
        isAnimating = false; 
        if(canvasGiaiThich != null) canvasGiaiThich.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}