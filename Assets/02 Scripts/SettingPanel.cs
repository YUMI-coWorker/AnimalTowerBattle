using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // ������ �̹���
    public Sprite[] profileIMG;
    public Image profile;
    // ������ �г���
    public InputField givenName;
    public Text nickName;
    // ȸ�� ���
    public bool is45Degree = true;      // 45���� ȸ��
    public Toggle toggle45;
    // BGM �����̵�
    public AudioSource BgmAudioSource;
    public Slider BgmSlider;

    void Start()
    {
        // Ŭ���Ҷ����� ȸ����带 ����
        is45Degree = toggle45.isOn;
        toggle45.onValueChanged.AddListener(RotationSetChanged);
        // BGM �����̴��� �����ϸ� BGM ��������
        BgmSlider.value = BgmAudioSource.volume;
        BgmSlider.onValueChanged.AddListener(SetVolume);
    }


    void Update()
    {
        
    }

    // ������ �̹���
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

    // ������ �г���
    public void ApplyNickname()
    {
        nickName.text = givenName.text;
    }
    // ȸ�� ���
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
