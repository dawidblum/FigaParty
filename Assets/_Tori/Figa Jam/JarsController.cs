using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class JarsController : MonoBehaviour
{
    public float speed = 1f;
    public float beltSpeed = 1f;
    public float rollerSpeed = 1f;
    public float stopDistance = 3f;

    private bool isMoving = false;
    private float targetPosition;

    private int jarsFilled;
    
    [SerializeField] private Material beltMaterial;
    [SerializeField] private List<GameObject> rollers;

    [SerializeField] private List<Jar> jars;
    [SerializeField] private GameObject jarPrefab;
    [SerializeField] private int jarsAmount;
    [SerializeField] private Transform jarTarget;
    [SerializeField] private int jarOffset;
    [SerializeField] private Shader fillShader;
    [Space(20)] 
    [SerializeField] private CanvasGroup rating;
    [SerializeField] private TextMeshProUGUI ratingLabel;
    public AudioSource source;

    [SerializeField] private float goDownDuration = 1;
    [SerializeField] private float goUpDuration = 1;
    [SerializeField] private Transform pump;
    [SerializeField] private Transform pour;
    
    private int currentJar;

    private void Start()
    {
        CreateJars();
        jars[0].canFill = true;
        pump.DOLocalMove(new Vector3(0, -.5f, 0), goDownDuration).OnComplete((() => jars[currentJar].canFill = true));

    }

    private void CreateJars() {
        for (int i = 0; i < jarsAmount; i++) {
            var jar = Instantiate(jarPrefab, jarTarget).GetComponent<Jar>();
            jar.transform.localPosition = new Vector3(-i * jarOffset, 0, 0);
            var mat = jar.jarContent.GetComponent<MeshRenderer>().material = new Material(fillShader);
            mat.SetFloat("_Fill_Height", -.11f);
            jar.morpher._oldMat = mat;
            jar.morpher._newMat = mat;
            jar.morpher._finalMaterial = mat;
            jar.jarsController = this;
            jars.Add(jar);
        }
    }

    public void Rate(float _rating) {
        DOTween.Kill(gameObject);
        rating.DOFade(1, .5f).SetEase(Ease.OutBack).SetId(gameObject).SetId(gameObject).OnComplete(() => 
            rating.DOFade(0, .5f).SetEase(Ease.OutBack).SetId(gameObject).SetDelay(5f).SetId(gameObject));
        
        // Sequence sequence = DOTween.Sequence();
        //
        // sequence.Append(rating.transform.DOScale(1, .5f).SetEase(Ease.OutQuart)).SetId(gameObject);
        // sequence.Append(rating.transform.DOScale(0, .5f).SetEase(Ease.InQuart)).SetId(gameObject).SetDelay(.5f);
        rating.transform.DOScale(1, .5f).SetEase(Ease.OutQuart).SetId(gameObject).SetId(gameObject).OnComplete(() => {
            rating.transform.DOScale(0, .5f).SetEase(Ease.InQuart).SetId(gameObject).SetDelay(.5f).SetId(gameObject);
        });

        var grade = "";
        switch (_rating)
        {
            case float n when (n <= -0.1f):
                grade = "F";
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                break;
            case float n when (n > -0.1f && n <= -0.05f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "F+";
                break;
            case float n when (n > -0.05f && n <= 0f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "E";
                break;
            case float n when (n > 0f && n <= 0.01f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "E+";
                break;
            case float n when (n > 0.01f && n <= 0.02f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "D-";
                break;
            case float n when (n > 0.02f && n <= 0.03f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "D";
                break;
            case float n when (n > 0.03f && n <= 0.035f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "D+";
                break;
            case float n when (n > 0.035f && n <= 0.045f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "C-";
                break;
            case float n when (n > 0.045f && n <= 0.055f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "C";
                break;
            case float n when (n > 0.055f && n <= 0.065f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Win);
                grade = "C+";
                break;
            case float n when (n > 0.065f && n <= 0.075f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Win);
                grade = "B";
                break;
            case float n when (n > 0.075f && n <= 0.085f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Win);
                grade = "B+";
                break;
            case float n when (n > 0.085f && n <= 0.095f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Win);
                grade = "A";
                break;
            case float n when (n > 0.095f && n <= 0.11f):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Yay);
                grade = "A++";
                break;
            case float n when (n > 0.11f ):
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Jar_Fail);
                grade = "F";
                break;
            default:
                grade = "N"; // Indicating invalid rating
                break;
        }

        ratingLabel.text = grade;
    }


    public void Pour() {
        pour.DOLocalMove(new Vector3(0, -1.3f, 0), .2f).SetEase(Ease.OutExpo);
    }
    void Update()
    {
        if(jarsFilled == 6)
            SceneLoader.Instance.LoadScene(4);
        
        if(GameManager.Instance.gameStopped) return;
        if(Time.timeScale == 0) return;
        if (!isMoving) {
            if(jars.Count < 0) return;
            if (jars[currentJar].fillHeight > 0.11f && !jars[currentJar].overfill) {
                jars[currentJar].morpher.IsDeforming = true;
                StartCoroutine(ChangeSliderValueOverTime(jars[currentJar]));
                jars[currentJar].overfill = true;
            }
        }

        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            var beltMaterialMainTextureOffset = beltMaterial.mainTextureOffset;
            beltMaterial.mainTextureOffset =
                new Vector2(0, beltMaterialMainTextureOffset.y -= beltSpeed * Time.deltaTime);


            rollers.ForEach(roller => roller.transform.Rotate(0, 0, -rollerSpeed));
            if (transform.position.x >= targetPosition) {
                transform.position = new Vector3(targetPosition, transform.position.y, transform.position.z);
                isMoving = false;
                currentJar++;
                jarsFilled++;
                pump.DOLocalMove(new Vector3(0, -.5f, 0), goDownDuration).OnComplete((() => jars[currentJar].canFill = true));
            }
        }

        if (!isMoving && Input.GetKeyUp(KeyCode.Space))
        {
            pour.DOLocalMove(new Vector3(0, -0.945f, 0), 1f).SetEase(Ease.OutExpo);

            jars[currentJar].canFill = false;
            targetPosition = transform.position.x + jarOffset;

            pump.DOLocalMove(new Vector3(0, 0, 0), goUpDuration).OnComplete((() => {
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Belt);
                isMoving = true;
            }));
            StartCoroutine(FadeOutCoroutine());
            Rate(jars[currentJar].fillHeight);
        }
    }

    public void StopRoutine() {
        StopCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = source.volume;
        float timer = 0.0f;

        while (timer < .5f)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, timer / .5f);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();

    }
    IEnumerator ChangeSliderValueOverTime(Jar _jar)
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float endValue = 1f;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            _jar.morpher._finalMaterial.SetFloat("_Fill_Height", Mathf.Lerp(startValue, endValue, elapsedTime *2));
            _jar.morpher.SetSlider(Mathf.Lerp(startValue, endValue, elapsedTime / 1));
            yield return null;
        }

        _jar.morpher.SetSlider(endValue);
    }
}