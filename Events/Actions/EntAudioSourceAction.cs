using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ent2D.Events.Actions {
    public class EntAudioSourceAction : EntAction {
        public enum CycleMethod {QUEUE=0, SHUFFLE}

        public List<AudioClip> Sounds = new List<AudioClip>();
        public CycleMethod Cycle = CycleMethod.QUEUE;
        public bool Loop = false;
        public bool Interruptible = false;

        [SerializeField]
        private float _Pitch = 1f;
        public virtual float Pitch {
            get { return _Pitch; }
        }

        private int _Index = -1;

        public EntAudioSourceAction() {}
        public EntAudioSourceAction(AudioClip clip) {
            Sounds.Add(clip);
        }
        public EntAudioSourceAction(List<AudioClip> clips) {
            Sounds.AddRange(clips);
        }
        public EntAudioSourceAction(List<AudioClip> clips, CycleMethod cycle) :
            this(clips) {
                Cycle = cycle;
        }

        public AudioClip GetClip() {
            if (IsEmpty()) {
                return null;
            } else if (IsSolo()) {
                return Sounds[0];
            }

            switch (Cycle) {
                case CycleMethod.QUEUE:
                    _Index += 1;
                    _Index = _Index < Sounds.Count ? _Index : 0;
                    return Sounds[_Index];
                case CycleMethod.SHUFFLE:
                    return Sounds[Random.Range(0, Sounds.Count -1)];
            }

            Debug.LogErrorFormat("[EntAudioSourceAction] Couldn't find any audio clips");
            return null;
        }
        public bool IsEmpty() {
            return Sounds.Count == 0;
        }
        public bool IsSolo() {
            return Sounds.Count == 1;
        }
    }
}
