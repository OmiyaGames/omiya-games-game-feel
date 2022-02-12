using System.Collections;
using UnityEngine;
using OmiyaGames.Global;
using OmiyaGames.Global.Settings;

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
	public class TimeManager : BaseSettingsManager<TimeManager, TimeSettings>
	{
		/// <summary>
		/// The configuration name stored in Editor Settings.
		/// </summary>
		public const string CONFIG_NAME = "com.omiyagames.timesettings";
		/// <summary>
		/// The name this settings will appear in the
		/// Project Setting's left-sidebar.
		/// </summary>
		public const string SIDEBAR_PATH = "Project/Omiya Games/Time";
		/// <summary>
		/// Name of the addressable.
		/// </summary>
		public const string ADDRESSABLE_NAME = "TimeSettings";

		/// <summary>
		/// Triggers when paused.
		/// </summary>
		public static event System.Action<TimeManager> OnBeforeManualPauseChanged;
		/// <summary>
		/// Triggers when paused.
		/// </summary>
		public static event System.Action<TimeManager> OnAfterManualPauseChanged;

		float timeScale = 1f;
		float timeScaleChangedFor = -1f;
		float slowDownDuration = 1f;
		bool isManuallyPaused = false;
		bool isTimeScaleTemporarilyChanged = false;

		public static float TimeScale
		{
			get
			{
				return GetInstance().timeScale;
			}
			set
			{
				TimeManager self = GetInstance();
				if (Mathf.Approximately(self.timeScale, value) == false)
				{
					// Change value
					self.timeScale = value;
					UpdateTimeScale(self);
				}
			}
		}

		public static bool IsManuallyPaused
		{
			get
			{
				return GetInstance().isManuallyPaused;
			}
			set
			{
				TimeManager self = GetInstance();
				if (self.isManuallyPaused != value)
				{
					// Shoot the pause event
					OnBeforeManualPauseChanged?.Invoke(self);

					// Change value
					self.isManuallyPaused = value;
					UpdateTimeScale(self);

					// Shoot the pause event
					OnAfterManualPauseChanged?.Invoke(self);
				}
			}
		}

		public static void RevertToCustomTimeScale()
		{
			IsManuallyPaused = false;
			TimeScale = Singleton.Get<Saves.GameSettings>().CustomTimeScale;
		}

		public static void HitPause()
		{
			PauseFor(GetData().DefaultHitPauseDurationSeconds);
		}

		public static void PauseFor(float durationSeconds)
		{
			TemporarilyChangeTimeScaleFor(0f, durationSeconds);
		}

		public static void TemporarilyChangeTimeScaleFor(float timeScale, float durationSeconds)
		{
			// Change the time scale immediately
			Time.timeScale = timeScale;

			// Store how long it's going to change the time scale
			TimeManager self = GetInstance();
			self.slowDownDuration = durationSeconds;

			// Update flags to revert the time scale later
			self.timeScaleChangedFor = 0f;
			self.isTimeScaleTemporarilyChanged = true;
		}

		/// <inheritdoc/>
		protected override string AddressableName => ADDRESSABLE_NAME;

		/// <inheritdoc/>
		protected override IEnumerator OnSetup()
		{
			// Store the timescale at this moment.
			timeScale = Time.timeScale;
			yield return base.OnSetup();
		}

		// TODO: consider using different alternatives than using Update()
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
					Time.timeScale = TimeScale;

					// Flag the 
					isTimeScaleTemporarilyChanged = false;
				}
			}
		}

		static void UpdateTimeScale(TimeManager self)
		{
			// Check if paused
			if (IsManuallyPaused == false)
			{
				// If not, progress normally
				Time.timeScale = self.timeScale;
			}
			else
			{
				// If so, pause
				Time.timeScale = 0f;
			}
		}
	}
}
