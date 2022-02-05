﻿using UnityEngine;
using OmiyaGames.Global;

namespace OmiyaGames.TimeSettings
{
	///-----------------------------------------------------------------------
	/// <copyright file="TimeManager.cs" company="Omiya Games">
	/// The MIT License (MIT)
	/// 
	/// Copyright (c) 2014-2022 Omiya Games
	/// 
	/// Permission is hereby granted, free of charge, to any person obtaining a copy
	/// of this software and associated documentation files (the "Software"), to deal
	/// in the Software without restriction, including without limitation the rights
	/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	/// copies of the Software, and to permit persons to whom the Software is
	/// furnished to do so, subject to the following conditions:
	/// 
	/// The above copyright notice and this permission notice shall be included in
	/// all copies or substantial portions of the Software.
	/// 
	/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
	/// THE SOFTWARE.
	/// </copyright>
	/// <list type="table">
	/// <listheader>
	/// <term>Revision</term>
	/// <description>Description</description>
	/// </listheader>
	/// <item>
	/// <term>
	/// <strong>Date:</strong> 5/18/2015<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>Initial verison.</description>
	/// </item><item>
	/// <term>
	/// <strong>Version:</strong> 1.0.0-pre.1<br/>
	/// <strong>Date:</strong> 2/4/2022<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>
	/// Converting from Singleton script to its own unique package.
	/// </description>
	/// </item>
	/// </list>
	/// </remarks>
	///-----------------------------------------------------------------------
	/// <summary>
	/// A settings file that allows adjusting the time scale.  Used for manually
	/// pausing the game.  Also allows temporarily slowing down or quickening time,
	/// useful for creating common juicy effects.
	/// </summary>
	public class TimeManager : ScriptableObject
	{
		/// <summary>
		/// The name this settings will appear in the
		/// Project Setting's left-sidebar.
		/// </summary>
		public const string SidebarDisplayPath = "Project/Omiya Games/Time";

		public event System.Action<TimeManager> OnManuallyPausedChanged;

		[SerializeField]
		float defaultHitPauseDuration = 0.2f;

		float timeScale = 1f;
		float timeScaleChangedFor = -1f;
		float slowDownDuration = 1f;
		bool isManuallyPaused = false;
		bool isTimeScaleTemporarilyChanged = false;

		public float TimeScale
		{
			get
			{
				return timeScale;
			}
			set
			{
				if (Mathf.Approximately(timeScale, value) == false)
				{
					timeScale = value;
					if (IsManuallyPaused == false)
					{
						UnityEngine.Time.timeScale = timeScale;
					}
				}
			}
		}

		public bool IsManuallyPaused
		{
			get
			{
				return isManuallyPaused;
			}
			set
			{
				if (isManuallyPaused != value)
				{
					// Change value
					isManuallyPaused = value;

					// Change time scale
					if (isManuallyPaused == true)
					{
						UnityEngine.Time.timeScale = 0;
					}
					else
					{
						UnityEngine.Time.timeScale = TimeScale;
					}

					// Shoot the pause event
					OnManuallyPausedChanged?.Invoke(this);
				}
			}
		}

		void Start()
		{
			timeScale = UnityEngine.Time.timeScale;
		}

		public void RevertToCustomTimeScale()
		{
			IsManuallyPaused = false;
			TimeScale = Singleton.Get<Saves.GameSettings>().CustomTimeScale;
		}

		public void HitPause()
		{
			PauseFor(defaultHitPauseDuration);
		}

		public void PauseFor(float durationSeconds)
		{
			TemporarilyChangeTimeScaleFor(0f, durationSeconds);
		}

		public void TemporarilyChangeTimeScaleFor(float timeScale, float durationSeconds)
		{
			// Change the time scale immediately
			UnityEngine.Time.timeScale = timeScale;

			// Store how long it's going to change the time scale
			slowDownDuration = durationSeconds;

			// Update flags to revert the time scale later
			timeScaleChangedFor = 0f;
			isTimeScaleTemporarilyChanged = true;
		}

		void Update()
		{
			// Check to see if we're not paused, and changed the time scale temporarily
			if ((IsManuallyPaused == false) && (isTimeScaleTemporarilyChanged == true))
			{
				// Increment the duration we've changed the time scale
				timeScaleChangedFor += Time.unscaledDeltaTime;

				// Check to see if enough time has passed to revert the time scale
				if (timeScaleChangedFor > slowDownDuration)
				{
					// Revert the time scale
					UnityEngine.Time.timeScale = TimeScale;

					// Flag the 
					isTimeScaleTemporarilyChanged = false;
				}
			}
		}
	}
}
