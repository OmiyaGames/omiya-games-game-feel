using OmiyaGames.Global.Settings;
using OmiyaGames.Managers;

namespace OmiyaGames.GameFeel
{
	///-----------------------------------------------------------------------
	/// <copyright file="Juice.cs" company="Omiya Games">
	/// The MIT License (MIT)
	/// 
	/// Copyright (c) 2022 Omiya Games
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
	/// <strong>Version:</strong> 1.0.0-pre.1<br/>
	/// <strong>Date:</strong> 2/12/2022<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>Initial verison.</description>
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
	public class Juice : BaseSettingsManager<Juice, GameFeelSettings>
	{
		/// <summary>
		/// The configuration name stored in Editor Settings.
		/// </summary>
		public const string CONFIG_NAME = "com.omiyagames.gamefeel";
		/// <summary>
		/// The name this settings will appear in the
		/// Project Setting's left-sidebar.
		/// </summary>
		public const string SIDEBAR_PATH = "Project/Omiya Games/Game Feel";
		/// <summary>
		/// Name of the addressable.
		/// </summary>
		public const string ADDRESSABLE_NAME = "GameFeelSettings";

		/// <inheritdoc/>
		protected override string AddressableName => ADDRESSABLE_NAME;

		/// <summary>
		/// Briefly pauses the game, creating the "hit-pause" effect.
		/// </summary>
		/// <param name="durationSeconds">
		/// How long the effect lasts.
		/// Set to negative to use the default pause duration.
		/// </param>
		public static void HitPause(float durationSeconds = -1f)
		{
			if (durationSeconds <= 0f)
			{
				durationSeconds = GetData().DefaultHitPauseDurationSeconds;
			}
			TimeManager.SetTimeScaleFor(0f, durationSeconds);
		}
	}
}
