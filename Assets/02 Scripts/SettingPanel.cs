using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // ������ �̹���
    public Sprite[] profileIMG;
    public Image profile;
    public static int profileNum = 0;   // ����� �����ʹ�ȣ
    // ������ �г���
    public InputField givenName;
    public Text nickName;
    public static string userName = "No Name";  // ����� �̸�
    // ȸ�� ���
    public bool is45Degree = true;      // 45���� ȸ��
    public Toggle toggle45;
    // BGM �����̵�
    public AudioSource BgmAudioSource;
    public Slider BgmSlider;
    // WallPaper
    public GameObject playPanel;
    public Sprite[] wallpapers;
    public ToggleGroup toggleBGI;

    void Start()
    {
        // Ŭ���Ҷ����� ȸ����带 ����
        is45Degree = toggle45.isOn;
        toggle45.onValueChanged.AddListener(RotationSetChanged);
        // BGM �����̴��� �����ϸ� BGM ��������
        BgmSlider.value = BgmAudioSource.volume;
        BgmSlider.onValueChanged.AddListener(SetVolume);
        // ��ۼ��ÿ� ���� ���ȭ�� ����
        ChangeBGI();    // �ʱ�ȭ
        foreach(Toggle toggle in toggleBGI.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(delegate { ChangeBGI(); });
            Debug.Log("BGI changed");
        }
    }


    void Update()
    {
        
    }

    // ������ �̹���
    public void ProfileBear()
    {
        profile.sprite = profileIMG[0];
        profileNum = 0;
    }
    public void ProfileCat()
    {
        profile.sprite = profileIMG[1];
        profileNum = 1;
    }
    public void ProfileSnake()
    {
        profile.sprite = profileIMG[2];
        profileNum = 2;
    }
    public void ProfileShark()
    {
        profile.sprite = profileIMG[3];
        profileNum = 3;
    }

    // ������ �г���
    public void ApplyNickname()
    {
        nickName.text = givenName.text;
        userName = givenName.text;
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

    // BGI
    private void ChangeBGI()
    {
        foreach (Toggle toggle in toggleBGI.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                // ��:0  ��:1
                int index = toggle.GetComponentInChildren<Text>().text == "��" ? 0 : 1;
                playPanel.GetComponent<Image>().sprite = wallpapers[index];
            }
        }
    }
}
