using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;

public class SpeechManager : MonoBehaviour
{
	KeywordRecognizer keywordRecognizer = null;
	Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	// Use this for initialization

	void Start()
	{
		keywords.Add("Halt", () => this.BroadcastMessage("OnStop"));

		keywords.Add("Move", () => this.BroadcastMessage("OnStart"));

		keywords.Add("Quick", () => this.BroadcastMessage("OnQuick"));

		keywords.Add("Slow", () => this.BroadcastMessage("OnSlow"));

		keywords.Add("Stop", () =>
			{
				var focusObject = GazeManager.Instance.FocusedObject;
				if (focusObject != null)
				{
					// Call the OnStop method on just the focused object.
					focusObject.SendMessage("OnStop");
				}
			});

		keywords.Add("Spin", () =>
			{
				var focusObject = GazeManager.Instance.FocusedObject;
				if (focusObject != null)
				{
					// Call the OnStop method on just the focused object.
					focusObject.SendMessage("OnStart");
				}
			});

		// Tell the KeywordRecognizer about our keywords.
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

		// Register a callback for the KeywordRecognizer and start recognizing!
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		keywordRecognizer.Start();
	}

	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		System.Action keywordAction;
		if (keywords.TryGetValue(args.text, out keywordAction))
		{
			keywordAction.Invoke();
		}
	}
}