﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Maui.Layouts;

namespace CommunityToolkit.Maui.Markup.Sample.Pages;

sealed partial class SettingsPage : BaseContentPage<SettingsViewModel>
{
	[RequiresUnreferencedCode("Calls CommunityToolkit.Maui.Behaviors.NumericValidationBehavior.NumericValidationBehavior()")]
	public SettingsPage(SettingsViewModel settingsViewModel) : base(settingsViewModel, "Settings")
	{
		Content = new AbsoluteLayout
		{
			Children =
			{
				new Image().Source("dotnet_bot.png").Opacity(0.25).IsOpaque(false).Aspect(Aspect.AspectFit)
					.ClearLayoutFlags()
					.LayoutFlags(AbsoluteLayoutFlags.SizeProportional | AbsoluteLayoutFlags.PositionProportional)
					.LayoutBounds(0.5, 0.5, 0.5, 0.5)
					.AutomationIsInAccessibleTree(false),

				new Label()
					.Text("Top Stories To Fetch")
					.AppThemeBinding(Label.TextColorProperty, AppStyles.BlackColor, AppStyles.PrimaryTextColorDark)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 0, 1, 40)
					.TextCenterHorizontal()
					.TextBottom()
					.Assign(out Label topNewsStoriesToFetchLabel),

				new Entry
					{
						Keyboard = Keyboard.Numeric
					}
					.BackgroundColor(Colors.White)
					.Placeholder($"Provide a value between {SettingsService.MinimumStoriesToFetch} and {SettingsService.MaximumStoriesToFetch}", Colors.Grey)
					.LayoutFlags(AbsoluteLayoutFlags.XProportional, AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0.5, 45, 0.8, 40)
					.Behaviors(new NumericValidationBehavior
					{
						Flags = ValidationFlags.ValidateOnValueChanged,
						MinimumValue = SettingsService.MinimumStoriesToFetch,
						MaximumValue = SettingsService.MaximumStoriesToFetch,
						ValidStyle = AppStyles.ValidEntryNumericValidationBehaviorStyle,
						InvalidStyle = AppStyles.InvalidEntryNumericValidationBehaviorStyle,
					})
					.TextCenter()
					.SemanticDescription(topNewsStoriesToFetchLabel.Text)
					.Bind(Entry.TextProperty, static (SettingsViewModel vm) => vm.NumberOfTopStoriesToFetch, static (vm, text) => vm.NumberOfTopStoriesToFetch = text),

				new Label()
					.Text(string.Format(CultureInfo.CurrentUICulture, $"The number must be between {SettingsService.MinimumStoriesToFetch} and {SettingsService.MaximumStoriesToFetch}."))
					.LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
					.LayoutBounds(0, 90, 1, 40)
					.TextCenter()
					.AppThemeColorBinding(Label.TextColorProperty, AppStyles.BlackColor, AppStyles.PrimaryTextColorDark)
					.Font(size: 12, italic: true)
					.SemanticHint($"The minimum and maximum possible values for the {topNewsStoriesToFetchLabel.Text} field above.")
			}
		};
	}
}