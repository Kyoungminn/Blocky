using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice;
using UnityEngine.UI;

namespace Photon.Voice.Unity
{
    public class Talking : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Sprite[] talkingEmotions;

        private Sprite currentEmotion;
        private Animator animator;
        private GameObject voiceManager;
        private Recorder recorder;
        private bool isPlaying = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            voiceManager = GameObject.Find("VoiceManager");
            recorder = voiceManager.GetComponent<Recorder>();
            currentEmotion = spriteRenderer.sprite;
        }

        void Update()
        {
            if (recorder.LevelMeter.CurrentAvgAmp > 0.006 && !isPlaying)
            {
                StartCoroutine(talkingAnimation());
            }
        }

        IEnumerator talkingAnimation()
        {
            isPlaying = true;
            currentEmotion = spriteRenderer.sprite;

            spriteRenderer.sprite = talkingEmotions[0];
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.sprite = talkingEmotions[1];
            yield return new WaitForSeconds(0.2f);

            isPlaying = false;
            spriteRenderer.sprite = currentEmotion;
        }
    }
}
