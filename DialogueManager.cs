using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindows;

    private List<string> listSentence;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueWindows;

    private int count;  // 대화 진행 상황 카운트

    public Animator animSprite;
    public Animator animDialogueWindows;

    public string typeSound;
    public string enterSound;

    private AudioManager theAudio;

    public bool talking = false;
    private bool keyActivated = false;
    private bool onlyText = false;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text.text = "";
        listSentence = new List<string>();
        listSprites = new List<Sprite>();
        listDialogueWindows = new List<Sprite>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void ShowText(string[] _sentences)
    {
        talking = true;
        onlyText = true;

        for (int i = 0; i < _sentences.Length; ++i)
        {
            listSentence.Add(_sentences[i]);
        }

        StartCoroutine(StartTextCoroutine());
    }
    IEnumerator StartTextCoroutine()
    {
        keyActivated = true;
        for (int i = 0; i < listSentence[count].Length; ++i)
        {
            text.text += listSentence[count][i];    // 1글자씩 출력
            if (i % 7 == 1)
            {
                theAudio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true;
        onlyText = false;

        for (int i = 0; i < dialogue.sentence.Length; ++i)
        {
            listSentence.Add(dialogue.sentence[i]);
            listSprites.Add(dialogue.sprites[i]);
            listDialogueWindows.Add(dialogue.dialogueWindows[i]);
        }

        animSprite.SetBool("Appear", true);
        animDialogueWindows.SetBool("Appear", true);
        StartCoroutine(StartDialogueCoroutine());
    }
    IEnumerator StartDialogueCoroutine()
    {
        if (count > 0)
        {
            // 대사 바가 달라질 경우
            if (listDialogueWindows[count] != listDialogueWindows[count - 1])
            {
                animSprite.SetBool("Change", true);
                animDialogueWindows.SetBool("Appear", false);

                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindows.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                // rendererSprite.sprite = listSprites[count]; 와 같음
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];

                animDialogueWindows.SetBool("Appear", true);
                animSprite.SetBool("Change", false);
            }
            else
            {
                if (listSprites[count] != listSprites[count - 1])
                {
                    animSprite.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);

                    // rendererSprite.sprite = listSprites[count]; 와 같음
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];

                    animSprite.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else
        {
            rendererDialogueWindows.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
            // rendererSprite.sprite = listSprites[count]; 와 같음
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }

        keyActivated = true;
        for (int i = 0; i < listSentence[count].Length; ++i)
        {
            text.text += listSentence[count][i];    // 1글자씩 출력
            if (i % 7 == 1)
            {
                theAudio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.01f);
        }

    }

    public void ExitDialogue()
    {
        count = 0;
        text.text = "";
        listSentence.Clear();
        listSprites.Clear();
        listDialogueWindows.Clear();

        animSprite.SetBool("Appear", false);
        animDialogueWindows.SetBool("Appear", false);
        talking = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                count++;
                text.text = "";
                theAudio.Play(enterSound);

                if (count == listSentence.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    if (onlyText)
                    {
                        StartCoroutine(StartTextCoroutine());
                    }
                    else
                    {
                        StartCoroutine(StartDialogueCoroutine());
                    }
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }
}
