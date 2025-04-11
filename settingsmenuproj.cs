/* Weekly PR 9
Shaheen Nafeie
Working on an options menu that toggles a full screen mode, the resolution, and the volume. Will add audio files later?
*/


using UnityEngine;
using UnityEngine.Audio;




public class settingsmenu : MonoBehaviour

{
	public AudioMixer mix;


void Start()
{
    Debug.Log("Loading Up Settings Menu");
    Debug.Log("Fullscreen?: " + Screen.fullScreen);
    Debug.Log("Quality Level: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);
}


	public void volume(float vol){
		mix.SetFloat("vol", vol);
    Debug.Log("volume is at: " + vol);
	
	}


  public void SetToFullScreen(bool isFull){
  Screen.fullScreen = isFull;
  Debug.Log("Fullscreen is? " + isFull);
  
  }  
  
  public void quality(int qualityRes){ //0 is low, 1 is medium , 2 is high (0 indexing)
  
  	QualitySettings.SetQualityLevel(qualityRes);
    Debug.Log("Quality level is: " + qualityRes);
  
}


  
  } 
