using System.Collections.Generic;
// using System.Text; // Không cần dùng StringBuilder nữa nên có thể bỏ dòng này
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;
    [SerializeField] GameObject canvasFishid;

    [SerializeField] TextMeshProUGUI _text;
    public int _point = 0;
    public int totalPoint = 8;
    
    
    public List<Image> ListSprite; 
    public Color _green;
    

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canvasFishid.SetActive(false);
        setUpPoint(); 
    }

    void setUpPoint()
    {
        _point = 0;
        
        
        _text.text = $"{_point}/{totalPoint}"; 
    }

    
    public void AddPoint()
    {
        if (_point < totalPoint)
        {
            _point++; 
            
           
            _text.text = $"{_point}/{totalPoint}"; 

           
            
            for (int i = 0; i < ListSprite.Count; i++)
            {
                if (i < _point)
                    ListSprite[i].color = _green; 
                else
                    ListSprite[i].color = Color.white;
            }
            
        }
        if(_point == totalPoint)
        {
            Debug.Log("SangUI hoan thanhf");
            canvasFishid.SetActive(true);
        }
    }
}