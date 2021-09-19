using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio;

namespace OpenGL_GameEngine.BeEngine2D
{
    public class SoundPlayer
    {
        Random RandomID = new Random();

        public WaveOutEvent outputAudio;
        public AudioFileReader audioFile;

        public SoundPlayer()
        {
            
        }

        public SoundPlayer(Vector2 Position, string SoundURL, float VolumeMultiplier, float Radius)
        {
            this.Radius = Radius;
            this.Position = Position;
            this.SoundURL = SoundURL;
            this.VolumeMultiplier = VolumeMultiplier;

            ObjectID = RandomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            InitSoundPlayer();

            BeEngine2D.RegisterSoundPlayer(this);
        }

        public SoundPlayer(Vector2 Position, string SoundURL, float VolumeMultiplier, float Radius, string Tag)
        {
            this.Radius = Radius;
            this.Position = Position;
            this.SoundURL = SoundURL;
            this.VolumeMultiplier = VolumeMultiplier;
            this.Tag = Tag;

            ObjectID = RandomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            InitSoundPlayer();

            BeEngine2D.RegisterSoundPlayer(this);
        }

        public SoundPlayer(Vector2 Position, string SoundURL, float VolumeMultiplier, float Radius, bool Repeat)
        {
            this.Radius = Radius;
            this.Position = Position;
            this.SoundURL = SoundURL;
            this.VolumeMultiplier = VolumeMultiplier;
            this.Repeat = Repeat;

            ObjectID = RandomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            InitSoundPlayer();

            BeEngine2D.RegisterSoundPlayer(this);
        }

        public SoundPlayer(Vector2 Position, string SoundURL, float VolumeMultiplier, float Radius, string Tag, bool Repeat)
        {
            this.Radius = Radius;
            this.Position = Position;
            this.SoundURL = SoundURL;
            this.VolumeMultiplier = VolumeMultiplier;
            this.Tag = Tag;
            this.Repeat = Repeat;

            ObjectID = RandomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            InitSoundPlayer();

            BeEngine2D.RegisterSoundPlayer(this);
        }

        private void InitSoundPlayer()
        {
            outputAudio = new WaveOutEvent();
            audioFile = new AudioFileReader(SoundURL);
            audioFile.Volume = 0;
            outputAudio.PlaybackStopped += OutputAudio_PlaybackStopped;
            outputAudio.Init(audioFile);
            outputAudio.Play();
        }

        private void OutputAudio_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (Repeat)
            {
                audioFile = new AudioFileReader(SoundURL);
                outputAudio.Init(audioFile);
                outputAudio.Play();
            }
        }

        public float Radius { get; set; }
        public Vector2 Position { get; set; }
        public string SoundURL { get; set; }
        public string Tag { get; set; }
        public bool Repeat { get; set; }
        public int ObjectID { get; }
        public float VolumeMultiplier { get; set; }
    }
}
