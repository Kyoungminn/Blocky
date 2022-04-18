using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{
    public class RecordButton : MonoBehaviour
    {
		private GCSpeechRecognition _speechRecognition;

		public Button _startRecordButton;

		public InputField _resultText;

		private bool isStart = true;

		private void Start()
		{
			_speechRecognition = GCSpeechRecognition.Instance;
			_speechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
			_speechRecognition.RecognizeFailedEvent += RecognizeFailedEventHandler;

			_speechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;

			_speechRecognition.EndTalkigEvent += EndTalkigEventHandler;

			_speechRecognition.RequestMicrophonePermission(null);

			// select first microphone device
			if (_speechRecognition.HasConnectedMicrophoneDevices())
			{
				_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[0]);
			}
		}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
				RecordButtonClick();
            }
        }

        private void OnDestroy()
		{
			_speechRecognition.RecognizeSuccessEvent -= RecognizeSuccessEventHandler;
			_speechRecognition.RecognizeFailedEvent -= RecognizeFailedEventHandler;

			_speechRecognition.FinishedRecordEvent -= FinishedRecordEventHandler;

			_speechRecognition.EndTalkigEvent -= EndTalkigEventHandler;
		}

		public void RecordButtonClick()
		{
			if (isStart)
			{
				Debug.Log("Rec Start");
				_resultText.text = "";
				_speechRecognition.StartRecord(false);
				_startRecordButton.GetComponent<Animator>().SetBool("isBlink", true);
			}
            else
            {
				Debug.Log("Rec Stop");
				_speechRecognition.StopRecord();
				_startRecordButton.GetComponent<Animator>().SetBool("isBlink", false);
			}
			isStart = !isStart;
		}

		private void EndTalkigEventHandler(AudioClip clip, float[] raw)
		{
			FinishedRecordEventHandler(clip, raw);
		}

		private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
		{
			if (clip == null)
				return;

			RecognitionConfig config = RecognitionConfig.GetDefault();
			config.languageCode = Enumerators.LanguageCode.ko_KR.Parse();
			config.audioChannelCount = clip.channels;
			// configure other parameters of the config if need

			GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
			{
				audio = new RecognitionAudioContent()
				{
					content = raw.ToBase64()
				},
				//audio = new RecognitionAudioUri() // for Google Cloud Storage object
				//{
				//	uri = "gs://bucketName/object_name"
				//},
				config = config
			};

			_speechRecognition.Recognize(recognitionRequest);
		}

		private void RecognizeFailedEventHandler(string error)
		{
			_resultText.text = "Recognize Failed: " + error;
		}

		private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
		{
			foreach (var result in recognitionResponse.results)
			{
				foreach (var alternative in result.alternatives)
				{
					if (_resultText.text == "")
						_resultText.text += alternative.transcript.TrimEnd('.');
				}
			}
		}

	}
}
