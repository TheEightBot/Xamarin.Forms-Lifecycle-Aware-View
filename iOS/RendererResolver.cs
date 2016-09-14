using System;
using Xamarin.Forms;

namespace XamForms.LifecycleAwareView.iOS
{
	public class RendererResolver : Interfaces.IRendererResolver
	{
		public object GetRenderer(VisualElement element)
		{
			return Xamarin.Forms.Platform.iOS.Platform.GetRenderer(element);
		}

		public bool HasRenderer(VisualElement element)
		{
			return GetRenderer(element) != null;
		}
	}
}

