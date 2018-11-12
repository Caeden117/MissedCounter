using IllusionPlugin;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System;

namespace MissedCounter
{
    public class Plugin : IPlugin
    {
        public string Name => "MissedCounter";
        public string Version => "1.0.3";

        /*
         * MissedCounter | By Caeden117
         * This plugin was made for the Example Mod tutorial in the Beat Saber Wiki:
         * https://wiki.assistant.moe/modding/example-mod
         * 
         * Crashing help from ya bois Kyle1413 and brian
         */


        private readonly string[] env = { "DefaultEnvironment", "BigMirrorEnvironment", "TriangleEnvironment", "NiceEnvironment" };
        public static Vector3 counterPosition = new Vector3(-3.25f, 0.5f, 7f);

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1) { }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (!env.Contains(arg0.name)) return; //using System.Linq;
            new GameObject("MissedCounter").AddComponent<MissedCounter>();
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public void OnLevelWasLoaded(int level) { }
        public void OnLevelWasInitialized(int level) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }
    }
}
