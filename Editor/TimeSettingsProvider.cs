using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using OmiyaGames.Global.Editor;
using OmiyaGames.Global.Settings.Editor;

namespace OmiyaGames.TimeSettings.Editor
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="TimeSettingsProvider.cs" company="Omiya Games">
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
	/// <strong>Date:</strong> 2/4/2022<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>Initial verison.</description>
	/// </item>
	/// </list>
	/// </remarks>
	///-----------------------------------------------------------------------
	/// <summary>
	/// Editor for <see cref="TimeSettings"/>.
	/// Appears under the Project Settings window.
	/// </summary>
	public class TimeSettingsProvider : BaseSettingsEditor<TimeSettings>
	{
		/// <inheritdoc/>
		public override string DefaultSettingsFileName => "TimeSettings";
		/// <inheritdoc/>
		public override string UxmlPath => "Packages/com.omiyagames.time/Editor/TimeManager.uxml";
		/// <inheritdoc/>
		public override string AddressableGroupName => SettingsEditorHelpers.OMIYA_GAMES_GROUP_NAME;
		/// <inheritdoc/>
		public override string AddressableName => TimeManager.ADDRESSABLE_NAME;
		/// <inheritdoc/>
		public override string ConfigName => TimeManager.CONFIG_NAME;
		/// <inheritdoc/>
		public override string HeaderText => "Time Settings";
		/// <inheritdoc/>
		public override string HelpUrl => "https://omiyagames.github.io/omiya-games-time";

		/// <summary>
		/// Constructs a project-scoped <see cref="SettingsProvider"/>.
		/// </summary>
		public TimeSettingsProvider(string path, IEnumerable<string> keywords) : base(path, keywords) { }

		/// <summary>
		/// Registers this <see cref="SettingsProvider"/>.
		/// </summary>
		/// <returns></returns>
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			// Create the settings provider
			return new TimeSettingsProvider(TimeManager.SIDEBAR_PATH, GetSearchKeywordsFromGUIContentProperties<Styles>());
		}

		class Styles
		{
			public static readonly GUIContent defaultHitPauseDuration = new GUIContent("Default Hit Pause Duration Seconds");
		}
	}
}
