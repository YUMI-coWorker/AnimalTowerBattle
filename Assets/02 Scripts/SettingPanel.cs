using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // 프로필 이미지
    public Sprite[] profileIMG;
    public Image profile;
    public static int profileNum = 0;   // 저장용 프로필번호
    // 프로필 닉네임
    public InputField givenName;
    public Text nickName;
    public static string userName = "No Name";  // 저장용 이름
    // 회전 모드
    public bool is45Degree = true;      // 45도씩 회전
    public Toggle toggle45;
    // BGM 슬라이드
    public AudioSource bgmAudioSource;
    public Slider bgmSlider;
    // Sound Effect 슬라이드
    public AudioSource sfxAudioSource;
    public Slider sfxSlider;
    // WallPaper
    public GameObject playPanel;
    public GameObject titlePanel;
    public GameObject shopPanel;
    public GameObject gameOverPanel;
    public Sprite[] wallpapers;
    public ToggleGroup toggleBGI;
    public Color day = new Color(0.1921569f, 0.8156863f, 0.9843137f);
    public Color night = new Color(0.1137255f, 0.1647059f, 0.2470588f);
    // 효과음
    public SoundManager soundManager;
    

    void Start()
    {
        // 클릭할때마다 회전모드를 변경
        is45Degree = toggle45.isOn;
        toggle45.onValueChanged.AddListener(RotationSetChanged);
        // BGM 슬라이더를 변경하면 BGM 음량조절
        bgmSlider.value = bgmAudioSource.volume;
        bgmSlider.onValueChanged.AddListener(SetVolume);
        // SFX 슬라이더를 변경하면 SFX 음량조절
        sfxSlider.value = sfxAudioSource.volume;
        sfxSlider.onValueChanged.AddListener(SetSFX);
        // 토글선택에 따라 배경화면 적용
        ChangeBGI();    // 초기화
        foreach(Toggle toggle in toggleBGI.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(delegate { ChangeBGI(); });
            Debug.Log("BGI changed");
        }
    }


    void Update()
    {
        
    }

    // 프로필 이미지
    public void ProfileBear()
    {
        profile.sprite = profileIMG[0];
        profileNum = 0;
        soundManager.PlaySound(5);
    }
    public void ProfileCat()
    {
        profile.sprite = profileIMG[1];
        profileNum = 1;
        soundManager.PlaySound(5);
    }
    public void ProfileSnake()
    {
        profile.sprite = profileIMG[2];
        profileNum = 2;
        soundManager.PlaySound(5);
    }
    public void ProfileShark()
    {
        profile.sprite = profileIMG[3];
        profileNum = 3;
        soundManager.PlaySound(5);
    }

    // 프로필 닉네임
    public void ApplyNickname()
    {
        nickName.text = givenName.text;
        userName = givenName.text;
        soundManager.PlaySound(5);
    }
    // 회전 모드
    private void RotationSetChanged(bool isOn)
    {
        is45Degree = isOn;
        soundManager.PlaySound(5);
        Debug.Log("RotationSet is selected: " + is45Degree);
    }
    // BGM Volume
    private void SetVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }

    // Sound Effect volume
    private void SetSFX(float volume)
    {
        sfxAudioSource.volume = volume;
    }

    // BGI
    private void ChangeBGI()
    {
        foreach (Toggle toggle in toggleBGI.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                // 낮:0  밤:1
                int index = toggle.GetComponentInChildren<Text>().text == "낮" ? 0 : 1;
                Camera.main.backgroundColor = index == 0 ? day : night;
                playPanel.GetComponent<Image>().sprite = wallpapers[index+2];     // 게임 배경
                titlePanel.GetComponent<Image>().sprite = wallpapers[index];    // 타이틀 배경
                shopPanel.GetComponent<Image>().sprite = wallpapers[index];     // 뽑기 배경
                gameOverPanel.GetComponent<Image>().sprite = wallpapers[index]; // 게임오버 배경
            }
        }

        soundManager.PlaySound(5);
    }
}
