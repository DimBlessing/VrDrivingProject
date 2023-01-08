using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu
{
	public class OptionsMenuNew : MonoBehaviour
	{

		public enum Platform { Desktop, Mobile };
		public Platform platform;
		// toggle buttons
		[Header("MOBILE SETTINGS")]
		public GameObject mobileSFXtext;
		public GameObject mobileMusictext;
		public GameObject mobileShadowofftextLINE;
		public GameObject mobileShadowlowtextLINE;
		public GameObject mobileShadowhightextLINE;

		[Header("VIDEO SETTINGS")]
		public GameObject fullscreentext;
		public GameObject ambientocclusiontext;
		public GameObject shadowofftextLINE;
		public GameObject shadowlowtextLINE;
		public GameObject shadowhightextLINE;
		public GameObject aaofftextLINE;
		public GameObject aa2xtextLINE;
		public GameObject aa4xtextLINE;
		public GameObject aa8xtextLINE;
		public GameObject vsynctext;
		public GameObject motionblurtext;
		public GameObject texturelowtextLINE;
		public GameObject texturemedtextLINE;
		public GameObject texturehightextLINE;
		public GameObject cameraeffectstext;

		[Header("GAME SETTINGS")]
		public GameObject showhudtext;
		public GameObject tooltipstext;
		public GameObject difficultynormaltext;
		public GameObject difficultynormaltextLINE;
		public GameObject difficultyhardcoretext;
		public GameObject difficultyhardcoretextLINE;

		[Header("CONTROLS SETTINGS")]
		public GameObject invertmousetext;

		// sliders
		public GameObject musicSlider;
		public GameObject sensitivityXSlider;
		public GameObject sensitivityYSlider;
		public GameObject mouseSmoothSlider;

		private float sliderValue = 0.0f;
		private float sliderValueXSensitivity = 0.0f;
		private float sliderValueYSensitivity = 0.0f;
		private float sliderValueSmoothing = 0.0f;


		public void Start()
		{

		}

		public void Update()
		{

		}

		public void FullScreen()
		{
			Screen.fullScreen = !Screen.fullScreen;

			if (Screen.fullScreen == true)
			{
				fullscreentext.GetComponent<TMP_Text>().text = "on";
			}
			else if (Screen.fullScreen == false)
			{
				fullscreentext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void MusicSlider()
		{
			//PlayerPrefs.SetFloat("MusicVolume", sliderValue);
			
		}

		public void SensitivityXSlider()
		{
			
		}

		public void SensitivityYSlider()
		{
			
		}

		public void SensitivitySmoothing()
		{
			
		}

		// the playerprefs variable that is checked to enable hud while in game
		public void ShowHUD()
		{
			if (PlayerPrefs.GetInt("ShowHUD") == 0)
			{
				showhudtext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("ShowHUD") == 1)
			{
				showhudtext.GetComponent<TMP_Text>().text = "off";
			}
		}

		// the playerprefs variable that is checked to enable mobile sfx while in game
		public void MobileSFXMute()
		{
			if (PlayerPrefs.GetInt("Mobile_MuteSfx") == 0)
			{
				mobileSFXtext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("Mobile_MuteSfx") == 1)
			{
				mobileSFXtext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void MobileMusicMute()
		{
			if (PlayerPrefs.GetInt("Mobile_MuteMusic") == 0)
			{
				mobileMusictext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("Mobile_MuteMusic") == 1)
			{
				mobileMusictext.GetComponent<TMP_Text>().text = "off";
			}
		}

		// show tool tips like: 'How to Play' control pop ups
		public void ToolTips()
		{
			if (PlayerPrefs.GetInt("ToolTips") == 0)
			{
				tooltipstext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("ToolTips") == 1)
			{
				tooltipstext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void NormalDifficulty()
		{
			difficultyhardcoretextLINE.gameObject.SetActive(false);
			difficultynormaltextLINE.gameObject.SetActive(true);

		}

		public void HardcoreDifficulty()
		{
			difficultyhardcoretextLINE.gameObject.SetActive(true);
			difficultynormaltextLINE.gameObject.SetActive(false);

		}

		public void ShadowsOff()
		{
			shadowofftextLINE.gameObject.SetActive(true);
			shadowlowtextLINE.gameObject.SetActive(false);
			shadowhightextLINE.gameObject.SetActive(false);
		}

		public void ShadowsLow()
		{
			shadowofftextLINE.gameObject.SetActive(false);
			shadowlowtextLINE.gameObject.SetActive(true);
			shadowhightextLINE.gameObject.SetActive(false);
		}

		public void ShadowsHigh()
		{
			shadowofftextLINE.gameObject.SetActive(false);
			shadowlowtextLINE.gameObject.SetActive(false);
			shadowhightextLINE.gameObject.SetActive(true);
		}

		public void MobileShadowsOff()
		{
			mobileShadowofftextLINE.gameObject.SetActive(true);
			mobileShadowlowtextLINE.gameObject.SetActive(false);
			mobileShadowhightextLINE.gameObject.SetActive(false);
		}

		public void MobileShadowsLow()
		{
			mobileShadowofftextLINE.gameObject.SetActive(false);
			mobileShadowlowtextLINE.gameObject.SetActive(true);
			mobileShadowhightextLINE.gameObject.SetActive(false);
		}

		public void MobileShadowsHigh()
		{
			mobileShadowofftextLINE.gameObject.SetActive(false);
			mobileShadowlowtextLINE.gameObject.SetActive(false);
			mobileShadowhightextLINE.gameObject.SetActive(true);
		}

		public void vsync()
		{
			if (QualitySettings.vSyncCount == 0)
			{
				vsynctext.GetComponent<TMP_Text>().text = "on";
			}
			else if (QualitySettings.vSyncCount == 1)
			{
				vsynctext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void InvertMouse()
		{
			if (PlayerPrefs.GetInt("Inverted") == 1)
			{
				invertmousetext.GetComponent<TMP_Text>().text = "G29 Wheel";

			}
			else if (PlayerPrefs.GetInt("Inverted") == 0)
			{
				invertmousetext.GetComponent<TMP_Text>().text = "G29 Wheel";

			}
		}

		public void MotionBlur()
		{
			if (PlayerPrefs.GetInt("MotionBlur") == 0)
			{
				motionblurtext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("MotionBlur") == 1)
			{
				motionblurtext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void AmbientOcclusion()
		{
			if (PlayerPrefs.GetInt("AmbientOcclusion") == 0)
			{
				ambientocclusiontext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("AmbientOcclusion") == 1)
			{
				ambientocclusiontext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void CameraEffects()
		{
			if (PlayerPrefs.GetInt("CameraEffects") == 0)
			{
				cameraeffectstext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("CameraEffects") == 1)
			{
				cameraeffectstext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void TexturesLow()
		{
			texturelowtextLINE.gameObject.SetActive(true);
			texturemedtextLINE.gameObject.SetActive(false);
			texturehightextLINE.gameObject.SetActive(false);
		}

		public void TexturesMed()
		{
			texturelowtextLINE.gameObject.SetActive(false);
			texturemedtextLINE.gameObject.SetActive(true);
			texturehightextLINE.gameObject.SetActive(false);
		}

		public void TexturesHigh()
		{
			texturelowtextLINE.gameObject.SetActive(false);
			texturemedtextLINE.gameObject.SetActive(false);
			texturehightextLINE.gameObject.SetActive(true);
		}
	}
}