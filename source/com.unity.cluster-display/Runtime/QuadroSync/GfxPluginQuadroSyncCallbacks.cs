﻿using System;
using System.Collections;
using UnityEngine;

namespace Unity.ClusterDisplay
{
    using static GfxPluginQuadroSyncSystem;

    public class GfxPluginQuadroSyncCallbacks : MonoBehaviour
    {
        static GfxPluginQuadroSyncCallbacks s_Instance;
        bool m_Initialized = false;
        const int k_InitDelayFrames = 2;
        private int m_VSYNCMode;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(s_Instance.gameObject);
            }

            DontDestroyOnLoad(gameObject);
            s_Instance = this;
        }

        void OnEnable()
        {
            StartCoroutine(DelayedInit());

            m_VSYNCMode = QualitySettings.vSyncCount;
            ClusterDebug.Log("Enabling VSYNC");
            QualitySettings.vSyncCount = 0;
        }

        void OnDisable()
        {
            if (m_Initialized)
            {
                ClusterDebug.Log("Disposing Quadro Sync.");

                Instance.ExecuteQuadroSyncCommand(EQuadroSyncRenderEvent.QuadroSyncDispose, new IntPtr());
                m_Initialized = false;

                ClusterDebug.Log($"Reverting VSYNC mode to: {m_VSYNCMode}");
                QualitySettings.vSyncCount = m_VSYNCMode;
            }
        }

        IEnumerator DelayedInit()
        {
            if (!m_Initialized)
            {
                for (int i = 0; i < k_InitDelayFrames; i++)
                {
                    yield return null;
                }

                ClusterDebug.Log("Initializing Quadro Sync.");
                Instance.ExecuteQuadroSyncCommand(EQuadroSyncRenderEvent.QuadroSyncInitialize, new IntPtr());
                m_Initialized = true;
            }
        }
    }
}
