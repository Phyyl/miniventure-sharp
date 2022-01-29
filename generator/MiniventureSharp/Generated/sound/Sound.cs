namespace com.mojang.ld22.sound;


public class Sound {
	public static readonly Sound playerHurt = new Sound("/playerhurt.wav"); //creates a sound from playerhurt.wav file
	public static readonly Sound playerDeath = new Sound("/death.wav"); //creates a sound from death.wav file
	public static readonly Sound monsterHurt = new Sound("/monsterhurt.wav"); //creates a sound from monsterhurt.wav file
	public static readonly Sound test = new Sound("/test.wav"); //creates a sound from test.wav file
	public static readonly Sound pickup = new Sound("/pickup.wav"); //creates a sound from pickup.wav file
	public static readonly Sound bossdeath = new Sound("/bossdeath.wav"); //creates a sound from bossdeath.wav file
	public static readonly Sound craft = new Sound("/craft.wav"); //creates a sound from craft.wav file

	private AudioClip clip; // Creates a audio clip to be played

	private Sound(string name) {
		try {
			clip = Applet.newAudioClip(typeof(Sound).getResource(name)); //tries to load the audio clip from the name you gave above.
		} catch (Exception e) {
			e.printStackTrace(); // else it will throw an error
		}
	}

	public virtual void play() {
		try {
			new Thread() { //creates a new thread (string of events)
				run = () => { //runs the thread
					clip.play(); // plays the sound clip when called
				}
			}.start(); // starts the thread
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}