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
            Play(audioAction);
        }

        public void Play(EntAudioSourceAction action) {
            Play(action.GetClip(),
                    action.Loop,
                    action.Interruptible,
                    action.Pitch);
        }

        public void Play(AudioClip clip,
                bool loop = false,
                bool interruptible = true,
                float pitch = 1f) {

            if (clip == null) {
                _AudioSource.Stop();
                return;
            }

            if (!_AudioSource.isPlaying
                    || (clip != _AudioSource.clip && interruptible)) {
                _AudioSource.Stop();
                _AudioSource.clip = clip;
                _AudioSource.Play();
            }

            _AudioSource.loop = loop;
            _AudioSource.pitch = pitch;
        }

        /*
        public void Play(string state,
                string folderFormat,
                bool loop = false,
                bool interruptible = true,
                float pitch = 1f) {

            AudioClip clip = GetClip(state, folderFormat);
            if (!_AudioSource.isPlaying
                    || (clip != _AudioSource.clip && interruptible)) {
                _AudioSource.Stop();
                _AudioSource.clip = GetClip(state, folderFormat);
                _AudioSource.Play();
            }

            _AudioSource.loop = loop;
            _AudioSource.pitch = pitch;
        }
        */
    }
}
