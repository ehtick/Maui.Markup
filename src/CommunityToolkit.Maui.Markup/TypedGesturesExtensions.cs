﻿using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Windows.Input;
using CommunityToolkit.Maui.Markup.Services;

namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Element Gestures
/// </summary>
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
public static class TypedGesturesExtensions
{
	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command </summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		SwipeDirection? direction = null,
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{
		return BindSwipeGesture<TGestureElement, TCommandBindingContext, object, object?>(
			gestureElement,
			getter,
			setter,
			source,
			mode,
			direction: direction,
			threshold: threshold);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Expression<Func<TParameterBindingContext, TParameterSource>>? parameterGetter = null,
		Action<TParameterBindingContext, TParameterSource?>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		SwipeDirection? direction = null,
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		(Func<TParameterBindingContext, object?>, string)[]? parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => [(b => b, GetMemberName(parameterGetter))]
		};

		return BindSwipeGesture(
			gestureElement,
			getterFunc,
			[
				(b => b, GetMemberName(getter))
			],
			setter,
			source,
			commandBindingMode,
			parameterGetterFunc,
			parameterGetterHandlers,
			parameterSetter,
			parameterBindingMode,
			parameterSource,
			direction,
			threshold);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command</summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		SwipeDirection? direction = null,
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{
		return gestureElement.BindSwipeGesture<TGestureElement, TCommandBindingContext, object, object?>(
			getter,
			handlers,
			setter,
			source,
			mode,
			direction: direction,
			threshold: threshold);
	}

	/// <summary>Add a <see cref="SwipeGestureRecognizer"/> and bind to its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindSwipeGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource?>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		SwipeDirection? direction = null,
		uint? threshold = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var clickGesture = gestureElement.BindGesture<SwipeGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
			getter,
			handlers,
			setter,
			source,
			commandBindingMode,
			parameterGetter,
			parameterHandlers,
			parameterSetter,
			parameterBindingMode,
			parameterSource);

		return gestureElement.ConfigureSwipeGesture(clickGesture, direction, threshold);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command</summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{

		return BindTapGesture<TGestureElement, TCommandBindingContext, object, object?>(
			gestureElement,
			getter,
			setter,
			source,
			mode,
			numberOfTapsRequired: numberOfTapsRequired);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Expression<Func<TCommandBindingContext, ICommand>> getter,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Expression<Func<TParameterBindingContext, TParameterSource>>? parameterGetter = null,
		Action<TParameterBindingContext, TParameterSource?>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var getterFunc = ConvertExpressionToFunc(getter);
		var parameterGetterFunc = parameterGetter switch
		{
			null => null,
			_ => ConvertExpressionToFunc(parameterGetter)
		};

		(Func<TParameterBindingContext, object?>, string)[]? parameterGetterHandlers = parameterGetter switch
		{
			null => null,
			_ => [(b => b, GetMemberName(parameterGetter))]
		};

		return BindTapGesture(
			gestureElement,
			getterFunc,
			[
				(b => b, GetMemberName(getter))
			],
			setter,
			source,
			commandBindingMode,
			parameterGetterFunc,
			parameterGetterHandlers,
			parameterSetter,
			parameterBindingMode,
			parameterSource,
			numberOfTapsRequired);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command </summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode mode = BindingMode.Default,
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
	{
		return gestureElement.BindTapGesture<TGestureElement, TCommandBindingContext, object, object?>(
			getter,
			handlers,
			setter,
			source,
			mode,
			numberOfTapsRequired: numberOfTapsRequired);
	}

	/// <summary>Adds a <see cref="TapGestureRecognizer"/> and bind its Command and (optionally) CommandParameter properties</summary>
	public static TGestureElement BindTapGesture<TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource?>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default,
		int? numberOfTapsRequired = null)
		where TGestureElement : BindableObject, IGestureRecognizers
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var tapGesture = gestureElement.BindGesture<TapGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
			getter,
			handlers,
			setter,
			source,
			commandBindingMode,
			parameterGetter,
			parameterHandlers,
			parameterSetter,
			parameterBindingMode,
			parameterSource);

		return gestureElement.ConfigureTapGesture(tapGesture, numberOfTapsRequired);
	}

	static Func<TBindingContext, TSource> ConvertExpressionToFunc<TBindingContext, TSource>(in Expression<Func<TBindingContext, TSource>> expression) => expression.Compile();

	static string GetMemberName<T>(in Expression<T> expression) => expression.Body switch
	{
		MemberExpression m => m.Member.Name,
		UnaryExpression { Operand: MemberExpression m } => m.Member.Name,
		_ => throw new InvalidOperationException("Invalid getter. The `getter` parameter must point directly to a property in the ViewModel and cannot add additional logic")
	};

	static TGestureRecognizer BindGesture<TGestureRecognizer, TGestureElement, TCommandBindingContext, TParameterBindingContext, TParameterSource>(
		this TGestureElement gestureElement,
		Func<TCommandBindingContext, ICommand> getter,
		(Func<TCommandBindingContext, object?>, string)[] handlers,
		Action<TCommandBindingContext, ICommand?>? setter = null,
		TCommandBindingContext? source = default,
		BindingMode commandBindingMode = BindingMode.Default,
		Func<TParameterBindingContext, TParameterSource>? parameterGetter = null,
		(Func<TParameterBindingContext, object?>, string)[]? parameterHandlers = null,
		Action<TParameterBindingContext, TParameterSource?>? parameterSetter = null,
		BindingMode parameterBindingMode = BindingMode.Default,
		TParameterBindingContext? parameterSource = default)
		where TGestureElement : IGestureRecognizers
		where TGestureRecognizer : BindableObject, IGestureRecognizer, new()
		where TCommandBindingContext : class
		where TParameterBindingContext : class
	{
		var gestureRecognizer = new TGestureRecognizer().BindCommand(getter,
			handlers,
			setter,
			source,
			commandBindingMode,
			parameterGetter,
			parameterHandlers,
			parameterSetter,
			parameterSource,
			parameterBindingMode);
		gestureElement.GestureRecognizers.Add(gestureRecognizer);

		return gestureRecognizer;
	}
}