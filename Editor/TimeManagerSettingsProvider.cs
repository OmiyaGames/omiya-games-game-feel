using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using OmiyaGames.Global.Editor;

namespace OmiyaGames.TimeSettings.Editor
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="TimeManagerSettingsProvider.cs" company="Omiya Games">
	/// The MIT License (MIT)
	/// 
	/// Copyright (c) 2020-2020 Omiya Games
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
	/// Editor for <see cref="TimeManager"/>.
	/// Appears under the Project Settings window.
	/// </summary>
	public class TimeManagerSettingsProvider : SettingsProvider
	{
		public const string AssetFileName = "TimeSettings.asset";
		public const string UxmlPath = "Packages/com.omiyagames.time/Editor/TimeManager.uxml";

		/// <summary>
		/// The actual asset we're modifying.
		/// </summary>
		SerializedObject timeManager;

		class Styles
		{
			public static readonly GUIContent defaultHitPauseDuration = new GUIContent("Default Hit Pause Duration");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="scope"></param>
		public TimeManagerSettingsProvider(string path, SettingsScope scope = SettingsScope.Project) : base(path, scope)
		{ }

		/// <summary>
		/// Registers this <see cref="SettingsProvider"/>.
		/// </summary>
		/// <returns></returns>
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			// Check if the asset file is available
			if (IsSettingsAvailable == false)
			{
				// Create the asset here.
				SettingsHelpers.CreateOmiyaGamesSettings<TimeManager>(AssetFileName);
			}

			// Create the settings provider
			var returnProvider = new TimeManagerSettingsProvider(TimeManager.SidebarDisplayPath, SettingsScope.Project);

			// Automatically extract all keywords from the Styles.
			returnProvider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
			return returnProvider;
		}

		/// <summary>
		/// 
		/// </summary>
		public static TimeManager Asset
		{
			get => SettingsHelpers.GetOmiyaGamesSettings<TimeManager>(AssetFileName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool IsSettingsAvailable
		{
			get => File.Exists(SettingsHelpers.GetFullOmiyaGamesSettingsPath(AssetFileName));
		}

		/// <inheritdoc/>
		public override void OnActivate(string searchContext, VisualElement rootElement)
		{
			// This function is called when the user clicks on the MyCustom element in the Settings window.
			// Import UXML
			VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);

			// Apply the UXML to the root element
			VisualElement fullTree = visualTree.CloneTree();
			rootElement.Add(fullTree);

			// Bind the UXML to a serialized object
			// Note: this must be done last
			timeManager = new SerializedObject(Asset);
			rootElement.Bind(timeManager);
		}
	}
}
