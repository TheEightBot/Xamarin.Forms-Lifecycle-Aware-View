using System;
using Xamarin.Forms;

namespace XamForms.LifecycleAwareView.Interfaces
{
	public interface IRendererResolver
	{
		object GetRenderer(VisualElement element);

		bool HasRenderer(VisualElement element);
	}
}

