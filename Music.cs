using System;
using System.Media;

namespace Sound
{
	public class Music
	{
		SoundPlayer musicPlayer = new SoundPlayer ();

		public void playMusic()
		{
			musicPlayer.Play ();
		}
		public Music (string pathToMusic)
		{
			musicPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + pathToMusic;
		}

		 ~Music()
		{
			musicPlayer.Stop ();
			musicPlayer.Dispose ();
		}
	}

	public class SoundEffects //SFX
	{
		SoundPlayer soundEffectsPlayer = new SoundPlayer ();

		public void playSFX()
		{
			soundEffectsPlayer.Play ();
		}
		public SoundEffects (string pathToMusic)
		{
			soundEffectsPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + pathToMusic;
		}

		public void optionSelected()
		{
			System.Media.SystemSounds.Asterisk.Play();
		}

		~SoundEffects()
		{
			soundEffectsPlayer.Stop ();
			soundEffectsPlayer.Dispose ();
		}
	}

	public class SFX
	{
		public SoundPlayer SFX_player = new SoundPlayer();
		public SFX()
		{
		}

		public void optionSelected()
		{
			System.Media.SystemSounds.Asterisk.Play();
		}

		~SFX()
		{
			SFX_player.Stop ();
			SFX_player.Dispose ();
		}
	}
}

