using UnityEngine;
using UnityEngine.UI;

public class LightSettings : MonoBehaviour
{
    [SerializeField] private Slider lightSlider;
    
    // Kéo Directional Light của bạn (thường tên là Sun) vào biến này trên Inspector
    [SerializeField] private Light sunLight; 

    private void Start()
    {
        // Kiểm tra xem đã lưu cài đặt ánh sáng trước đó chưa
        if (PlayerPrefs.HasKey("lightIntensity"))
        {
            lightSlider.value = PlayerPrefs.GetFloat("lightIntensity");
        }
        else
        {
            // Nếu chưa có, lấy giá trị mặc định của bầu trời hiện tại
            lightSlider.value = RenderSettings.skybox.GetFloat("_Exposure");
        }
        
        SetBrightness(lightSlider.value);
    }

    // Hàm này gọi khi kéo Slider
    public void SetBrightness(float value)
    {
        // 1. Thay đổi thông số Exposure của Material Skybox
        RenderSettings.skybox.SetFloat("_Exposure", value);

        // 2. Thay đổi cường độ của đèn mặt trời
        if (sunLight != null)
        {
            // Bạn có thể nhân thêm hệ số nếu thấy nắng gắt quá hoặc tối quá
            // Ví dụ: value * 1.5f;
            sunLight.intensity = value; 
        }

       
        DynamicGI.UpdateEnvironment();

        // Lưu lại cài đặt
        PlayerPrefs.SetFloat("lightIntensity", value);
    }
}