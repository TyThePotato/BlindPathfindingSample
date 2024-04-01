using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PathfindingGame.Sensory {

    public class SensoryHelper : MonoBehaviour {

        private static SensoryHelper _instance;

        public GameObject soundEffectPrefab;

        public static SoundEvent NewSound = new SoundEvent();
        
        // AUDIO //
        
        public AudioClip[] grassFootstepAudioClips;
        public float grassFootstepStrength;
        
        public AudioClip[] leavesFootstepAudioClips;
        public float leavesFootstepStrength;
        
        public AudioClip[] waterFootstepAudioClips;
        public float waterFootstepStrength;

        public AudioClip[] playerAudioClips;
        public float playerAudioStrength;
        
        // SMELLS //
        // ...
        
        // DEBUGGING //
        
        public bool visualize;
        public GameObject audioVisualizationPrefab;
        public GameObject smellVisualizationPrefab;

        public static float AudioVisualizationRange = 10.0f;
        public static float SmellVisualizationRange = 10.0f;

        private void Awake() {
            _instance = this; // singleton
        }
        
        
        public static void PlayFootstepSound(FootstepType type, Vector3 position, float strengthMultiplier) {
            AudioClip clip = null;
            var strength = 0.0f;
            
            switch (type) {
                case FootstepType.Grass:
                    clip = GetRandomAudioClip(_instance.grassFootstepAudioClips);
                    strength = _instance.grassFootstepStrength;
                    break;
                case FootstepType.Leaves:
                    clip = GetRandomAudioClip(_instance.leavesFootstepAudioClips);
                    strength = _instance.leavesFootstepStrength;
                    break;
                case FootstepType.Water:
                    clip = GetRandomAudioClip(_instance.waterFootstepAudioClips);
                    strength = _instance.waterFootstepStrength;
                    break;
            }

            if (clip == null)
                return;
            
            PlaySoundGeneric(clip, position, strength * strengthMultiplier);
        }

        public static void PlayPlayerSound(Vector3 position) {
            var clip = GetRandomAudioClip(_instance.playerAudioClips);
            PlaySoundGeneric(clip, position, _instance.playerAudioStrength);
        }


        public static FootstepType GetFootstepMaterial(Vector3 rayOrigin, Vector3 rayDir) {
            if (Physics.Raycast(rayOrigin, rayDir, out var hit)) {
                var tag = hit.transform.tag;
                return tag switch {
                    "Grass" => FootstepType.Grass,
                    "Leaves" => FootstepType.Leaves,
                    "Water" => FootstepType.Water
                };
            }

            return FootstepType.Grass;
        }
        
        
        private static void PlaySoundGeneric(AudioClip clip, Vector3 position, float strength) {
            var go = Instantiate(_instance.soundEffectPrefab);
            go.transform.position = position;
            
            var src = go.GetComponent<AudioSource>();
            src.clip = clip;
            src.volume = strength;
            src.Play();
            
            Destroy(go, clip.length); // destroy object after sound finishes

            if (_instance.visualize) {
                var vis = Instantiate(_instance.audioVisualizationPrefab);
                vis.transform.position = position;
                vis.transform.localScale = Vector3.one * strength * AudioVisualizationRange;
                Destroy(vis, 1.0f);
            }
            
            // invoke event
            NewSound.Invoke(position, strength);
        }

        private static AudioClip GetRandomAudioClip(AudioClip[] clips) {
            var i = Random.Range(0, clips.Length);
            return clips[i];
        }

        public enum FootstepType {

            Grass,
            Leaves,
            Water

        }

    }

    [System.Serializable]
    public class SoundEvent : UnityEvent<Vector3, float> {} // Position, Strength
    
}