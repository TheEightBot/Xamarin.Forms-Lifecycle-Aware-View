using System;
using Xamarin.Forms;

namespace XamForms.LifecycleAwareView.Droid
{
	public class RendererResolver : Interfaces.IRendererResolver
	{
		public object GetRenderer(VisualElement element)
		{
			return Xamarin.Forms.Platform.Android.Platform.GetRenderer(element);
		}

		public bool HasRenderer(VisualElement element)
		{
			return GetRenderer(element) != null;
		}
	}
}

