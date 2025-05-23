﻿using BenchmarkDotNet.Attributes;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

[MemoryDiagnoser]
public class InitializeBindings : BaseTest
{
	readonly Label defaultBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	readonly Label defaultMarkupBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	readonly Label typedMarkupBindingsLabel = new()
	{
		BindingContext = new LabelViewModel()
	};

	[Benchmark(Baseline = true)]
	public void InitializeDefaultBindings()
	{
		defaultBindingsLabel.SetBinding(Label.TextProperty, nameof(LabelViewModel.Text), mode: BindingMode.TwoWay);
		defaultBindingsLabel.SetBinding(Label.TextColorProperty, nameof(LabelViewModel.TextColor), mode: BindingMode.TwoWay);
	}

	[Benchmark]
	public void InitializeDefaultBindingsMarkup()
	{
		defaultMarkupBindingsLabel
			.Bind(Label.TextProperty, nameof(LabelViewModel.Text), mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty, nameof(LabelViewModel.TextColor), mode: BindingMode.TwoWay);
	}

	[Benchmark]
	public void InitializeTypedBindingsMarkup()
	{
		typedMarkupBindingsLabel
			.Bind(Label.TextProperty,
				getter: static (LabelViewModel vm) => vm.Text,
				setter: static (vm, text) => vm.Text = text ?? string.Empty,
				mode: BindingMode.TwoWay)
			.Bind(Label.TextColorProperty,
				getter: static (LabelViewModel vm) => vm.TextColor,
				setter: static (vm, textColor) => vm.TextColor = textColor ?? Colors.Transparent,
				mode: BindingMode.TwoWay);
	}
}