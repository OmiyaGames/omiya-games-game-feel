using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OmiyaGames.Global.Settings.Editor;

namespace OmiyaGames.GameFeel.Editor
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="GameFeelSettingsProvider.cs" company="Omiya Games">
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
	/// Editor for <see cref="GameFeelSettings"/>.
	/// Appears under the Project Settings window.
	/// </summary>
	public class GameFeelSettingsProvider : BaseSettingsEditor<GameFeelSettings>
	{
		/// <inheritdoc/>
		public override string DefaultSettingsFileName => "GameFeelSettings";
		/// <inheritdoc/>
		public override string UxmlPath => "Packages/com.omiyagames.gamefeel/Editor/GameFeel.uxml";
		/// <inheritdoc/>
		public override string AddressableGroupName => SettingsEditorHelpers.OMIYA_GAMES_GROUP_NAME;
		/// <inheritdoc/>
		public override string AddressableName => Juice.ADDRESSABLE_NAME;
		/// <inheritdoc/>
		public override string ConfigName => Juice.CONFIG_NAME;
		/// <inheritdoc/>
		public override string HeaderText => "Game Feel Settings";
		/// <inheritdoc/>
		public override string HelpUrl => "https://github.com/OmiyaGames/omiya-games-game-feel";

		/// <summary>
		/// Constructs a project-scoped <see cref="SettingsProvider"/>.
		/// </summary>
		public GameFeelSettingsProvider(string path, IEnumerable<string> keywords) : base(path, keywords) { }

		/// <summary>
		/// Registers this <see cref="SettingsProvider"/>.
		/// </summary>
		/// <returns></returns>
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			// Create the settings provider
			return new GameFeelSettingsProvider(Juice.SIDEBAR_PATH, GetSearchKeywordsFromGUIContentProperties<Styles>());
		}

		class Styles
		{
			public static readonly GUIContent defaultHitPauseDuration = new GUIContent("Default Hit Pause Duration Seconds");
		}
	}
}
