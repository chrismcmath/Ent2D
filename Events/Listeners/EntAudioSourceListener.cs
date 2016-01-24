using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ent2D.Events.Actions;

namespace Ent2D.Events.Listeners {
    [RequireComponent(typeof(AudioSource))]
    public class EntAudioSourceListener : EntListener {
        private AudioSource _AudioSource;

        public void Awake() {
            _AudioSource = GetComponent<AudioSource>();
        }

        public override Type GetActionType() {
            return typeof(EntAudioSourceAction);
        }

        protected override void OnAction(EntAction action) {
            EntAudioSourceAction audioAction = (EntAudioSourceAction) action;
            TryPlay(audioAction);
        }

        public void TryPlay(EntAudioSourceAction action) {
            TryPlay(action.GetClip(),
                    action.Loop,
                    action.Force,
                    action.Pitch);
        }

        public void TryPlay(AudioClip clip,
                bool loop = false,
                bool force = true,
                float volume = 1f,
                float pitch = 1f) {

            if (clip == null) {
                _AudioSource.Stop();
                return;
            }

            if (!_AudioSource.isPlaying
                    || (clip != _AudioSource.clip && force)) {
                Play(clip, loop, volume, pitch);
            }
        }

        private void Play(AudioClip clip, bool loop, float volume, float pitch) {
            _AudioSource.Stop();

            _AudioSource.clip = clip;
            _AudioSource.volume = volume;
            _AudioSource.loop = loop;
            _AudioSource.pitch = pitch;

            _AudioSource.Play();
        }
    }
}
