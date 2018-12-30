using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sceneController : MonoBehaviour {
	
	private Slider slider;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ClickAsync(int level)
	{
		//chama tele de loading
		StartCoroutine(LoadLevelWithBar(level));

	}

	IEnumerator LoadLevelWithBar (int level)
	{	
		Application.LoadLevel(2);
		yield return new WaitForSecondsRealtime (3);
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();
		yield return new WaitForSecondsRealtime (2);
		AsyncOperation async = Application.LoadLevelAsync(level);
		while (!async.isDone)
		{
			float progress = Mathf.Clamp01 (async.progress/.9f);
			slider.value = progress;
			yield return null;
		}
	}

	public void exitGame(){
		print ("saiu");
		Application.Quit ();
	}


}
