using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NumberSystem : MonoBehaviour
{
    private AudioManager theAudio;
    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string correct_sound;

    private int count;              // �迭�� ũ��, �� �ڸ� �� ex) 1000 -> 3
    private int selectedTextBox;    // ���õ� �ڸ� ��
    private int result;             // �÷��̾ ������ ����
    private int correctNumber;      // ����

    private string tempNumber;

    public GameObject superObject;  // ȭ�� ��� ������ ����
    public GameObject[] panel;
    public Text[] Number_Text;

    public Animator anim;

    public bool activated;      // return new waitUntil
    private bool keyInput;      // Ű ó�� Ȱ��ȭ, ��Ȱ��ȭ
    private bool correctFlag;   // �������� �ƴ��� ����

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void ShowNumber(int _correctNumber)
    {
        correctNumber = _correctNumber;
        activated = true;
        correctFlag = false;

        string temp = correctNumber.ToString(); // length�� �̿��ϱ� ����
        for(int i = 0; i < temp.Length; ++i)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }
        superObject.transform.position = new Vector3(superObject.transform.position.x + (35 * count), 
                                                     superObject.transform.position.y,
                                                     superObject.transform.position.z);
        selectedTextBox = 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear", true);
        keyInput = true;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectedTextBox].text);    // ���õ� �ڸ����� �ؽ�Ʈ�� Integer ���� �������� ���� ����ȯ
        if(_arrow == "DOWN")
        {
            if (temp == 0)
                temp = 9;
            else
                temp--;
        }
        else if(_arrow == "UP")
        {
            if (temp == 9)
                temp = 0;
            else
                temp++;
        }
        Number_Text[selectedTextBox].text = temp.ToString();
    }
    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for(int i = 0; i <= count; ++i)
        {
            Number_Text[i].color = color;
        }
        color.a = 1f;
        Number_Text[selectedTextBox].color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox < count)
                    selectedTextBox++;
                else
                    selectedTextBox = 0;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox > 0)
                    selectedTextBox--;
                else
                    selectedTextBox = count;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z))       // ���� Ű
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X))       // ��� Ű
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }

    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;

        // ����ȣ���� �ֱ� ����
        for(int i = count; i >= 0; --i)
        {
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text;
        }

        yield return new WaitForSeconds(1f);

        result = int.Parse(tempNumber);
        if (result == correctNumber)
        {
            theAudio.Play(correct_sound);
            correctFlag = true;
        }
        else
        {
            theAudio.Play(cancel_sound);
            correctFlag = false;
        }
            
        StartCoroutine(ExitCoroutine());
    }
    IEnumerator ExitCoroutine()
    {
        Debug.Log("���� �� �� = " + result + "���� ���� = " + correctNumber);
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i <= count; ++i)
        {
            panel[i].SetActive(false);
        }
        superObject.transform.position = new Vector3(superObject.transform.position.x - (35 * count),
                                                     superObject.transform.position.y,
                                                     superObject.transform.position.z);
        activated = false;
    }
}
