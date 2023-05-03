using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SoundManager 
{
    public enum Sound
    {
    	Acceleration,
    	Decelaration,
    	Hit,
    	Bonus,
    	Coin,
    	Start,
    	PlayerDie,
    	End,
    }
    
    private static Dictionary<Sound, float> soundTimerDictionary;
    
    public static void Initialize()		//À appeler dans Awake joueur
    {
    	soundTimerDictionary = new Dictionary<Sound, float>();
    	soundTimerDictionary[Sound.Acceleration] = 0f;
    }
    
    public static void PlaySound(Sound sound)
    {
    	if(CanPlaySound(sound))
    	{
    		GameObject soundGameObject = new GameObject("Sound");
    		AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
    		audioSource.PlayOneShot(GetAudioClip(sound));
    	}
    
    }
    
    private static bool CanPlaySound(Sound sound)
    {
    	switch(sound)
    	{
    		default:
    			return true;
    		case Sound.Acceleration:
    			if(soundTimerDictionary.ContainsKey(sound))
    			{
    				float lastTimePlayed = soundTimerDictionary[sound];
    				float accelerationTimerMax = .05f;
    				
    				if(lastTimePlayed + accelerationTimerMax < Time.time)
    				{
    					soundTimerDictionary[sound] = Time.time;
    					return true;
    				}
    				else
    				{
    					return false;
    				}
    			}
                else
                {
	                return false;
                }

            case Sound.Decelaration:
	            if (soundTimerDictionary.ContainsKey(sound))
	            {
		            float lastTimePlayed = soundTimerDictionary[sound];
		            float decelerationTimerMax = .05f;
    				
		            if(lastTimePlayed + decelerationTimerMax < Time.time)
		            {
			            soundTimerDictionary[sound] = Time.time;
			            return true;
		            }
		            else
		            {
			            return false;
		            }
	            }
	            else
	            {
		            return true;
	            }
    			//break;
    	}
    }
    
    private static AudioClip GetAudioClip(Sound sound)
    {
    	foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
    	{
    		if (soundAudioClip.sound == sound) 
	            return soundAudioClip.audioClip;
    	}

        return null;
    }
}
