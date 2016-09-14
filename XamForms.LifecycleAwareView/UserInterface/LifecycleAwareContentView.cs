using System;
using Xamarin.Forms;

namespace XamForms.LifecycleAwareView.UserInterface
{
	public class LifecycleAwareContentView : ContentView
	{
		const string RendererPropertyName = "Renderer";

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName.Equals(RendererPropertyName, StringComparison.OrdinalIgnoreCase))
			{
				var rendererResolver = DependencyService.Get<Interfaces.IRendererResolver>();

				if (rendererResolver == null)
					throw new NullReferenceException("The renderer resolver was not initialized properly");

				if (rendererResolver.HasRenderer(this))
					OnAttached();
				else
					OnDetached();
			}

		}

		protected virtual void OnAttached() { }

		protected virtual void OnDetached() { }
	}
}

