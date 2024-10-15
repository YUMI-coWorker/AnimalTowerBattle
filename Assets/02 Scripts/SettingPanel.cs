using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // 프로필 이미지
    public Sprite[] profileIMG;
    public Image profile;
    // 프로필 닉네임
    public InputField givenName;
    public Text nickName;
    // 회전 모드
    public bool is45Degree = true;      // 45도씩 회전
    public Toggle toggle45;
    // BGM 슬라이드
    public AudioSource BgmAudioSource;
    public Slider BgmSlider;

    void Start()
    {
        // 클릭할때마다 회전모드를 변경
        is45Degree = toggle45.isOn;
        toggle45.onValueChanged.AddListener(RotationSetChanged);
        // BGM 슬라이더를 변경하면 BGM 음량조절
        BgmSlider.value = BgmAudioSource.volume;
        BgmSlider.onValueChanged.AddListener(SetVolume);
    }


    void Update()
    {
        
    }

    // 프로필 이미지
    public void ProfileBear()
    {
        profile.sprite = profileIMG[0];
    }
    public void ProfileCat()
    {
        profile.sprite = profileIMG[1];
    }
    public void ProfileSnake()
    {
        profile.sprite = profileIMG[2];
    }
    public void ProfileShark()
    {
        profile.sprite = profileIMG[3];
    }

    // 프로필 닉네임
    public void ApplyNickname()
    {
        nickName.text = givenName.text;
    }
    // 회전 모드
    private void RotationSetChanged(bool isOn)
    {
        is45Degree = isOn;
        Debug.Log("RotationSet is selected: " + is45Degree);
    }
    // BGM Volume
    private void SetVolume(float volume)
    {
        BgmAudioSource.volume = volume;
    }
}
