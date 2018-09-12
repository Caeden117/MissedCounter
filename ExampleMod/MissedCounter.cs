﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Threading;

namespace ExampleMod
{
    class MissedCounter : MonoBehaviour
    {
        int counter = 0;
        private ScoreController score;
        GameObject countGO;
        TextMeshPro counterText;

        async void Awake()
        {
            await GetScore();
        }

        Task GetScore()
        {
            return Task.Run(() => {
                while (true)
                {
                    score = Resources.FindObjectsOfTypeAll<ScoreController>().FirstOrDefault();
                    if (score != null) break;
                    Thread.Sleep(10);
                }
                Init();
            });
        }

        void Init()
        {
            counterText = this.gameObject.AddComponent<TextMeshPro>();
            counterText.text = "0";
            counterText.fontSize = 4;
            counterText.color = Color.white;
            counterText.font = Resources.Load<TMP_FontAsset>("Beon SDF No-Glow");
            counterText.alignment = TextAlignmentOptions.Center;
            counterText.rectTransform.position = Plugin.counterPosition + new Vector3(0, -0.4f, 0);

            countGO = new GameObject("Label");
            TextMeshPro label = countGO.AddComponent<TextMeshPro>();
            label.text = "Misses";
            label.fontSize = 3;
            label.color = Color.white;
            label.font = Resources.Load<TMP_FontAsset>("Beon SDF No-Glow");
            label.alignment = TextAlignmentOptions.Center;
            label.rectTransform.position = Plugin.counterPosition;

            if (score != null)
            {
                score.noteWasCutEvent += onNoteCut;
                score.noteWasMissedEvent += onNoteMiss;
            }
        }

        private void onNoteCut(NoteData data, NoteCutInfo info, int c)
        {
            if (data.noteType == NoteType.Bomb || !info.allIsOK) incrementCounter();
        }

        private void onNoteMiss(NoteData data, int c)
        {
            if (data.noteType != NoteType.Bomb) incrementCounter();
        }

        private void incrementCounter()
        {
            counter++;
            counterText.text = counter.ToString();
        }
    }
}
