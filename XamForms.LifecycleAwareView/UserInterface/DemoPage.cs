using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamForms.LifecycleAwareView.UserInterface
{
	public class DemoPage : ContentPage
	{
		ToolbarItem _goNext;

		ScrollView _mainScroll;

		StackLayout _mainContainer;

		public DemoPage()
		{
			_goNext = new ToolbarItem
			{
				Text = "Next",
				Command = new Command(async _ => await this.Navigation.PushAsync(new DemoPage()))
			};

			this.ToolbarItems.Add(_goNext);

			_mainScroll = new ScrollView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Gray
			};

			_mainContainer = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 24
			};

			var rnd = new Random(Guid.NewGuid().GetHashCode());

			for (int i = 0; i < rnd.Next(5, 25); i++)
			{
				var demoView = new DemoView
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.Fill,
					HeightRequest = rnd.Next(50, 150)
				};

				_mainContainer.Children.Add(demoView);
			}


			_mainScroll.Content = _mainContainer;

			this.Content = _mainScroll;
		}
	}

	public class DemoView : LifecycleAwareContentView
	{

		Label _title;
		Guid _id;

		PanGestureRecognizer _panned;
		TapGestureRecognizer _tapped;

		public DemoView()
		{
			_id = Guid.NewGuid();
			var rnd = new Random(_id.GetHashCode());

			this.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
			this.InputTransparent = false;

			_title = new Label
			{
				Text = _id.ToString(),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				TextColor = Color.White,
				BackgroundColor = Color.Transparent,
				InputTransparent = true
			};

			_panned = new PanGestureRecognizer();
			this.GestureRecognizers.Add(_panned);

			_tapped = new TapGestureRecognizer();
			this.GestureRecognizers.Add(_tapped);

			this.Content = _title;
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			//Here, we can to attachment style work;
			_panned.PanUpdated += PanUpdated;
			_tapped.Tapped += Tapped;

			System.Diagnostics.Debug.WriteLine($"Attached: {_id}");
		}

		protected override void OnDetached()
		{
			base.OnDetached();

			//You can use this to do cleanup work!
			_panned.PanUpdated -= PanUpdated;
			_tapped.Tapped -= Tapped;

			System.Diagnostics.Debug.WriteLine($"Detached: {_id}");
		}

		double translationX, translationY;

		async void PanUpdated(object sender, PanUpdatedEventArgs e)
		{
			switch (e.StatusType)
			{
				case GestureStatus.Started:
					translationX = translationY = 0;
					break;
				case GestureStatus.Running:
					translationX = e.TotalX;
					translationY = e.TotalY;
					break;
				case GestureStatus.Completed:
					await this.TranslateTo(translationX * 1.2d, translationY * 1.2d, easing: Easing.CubicOut);
					await Task.Delay(100);
					await this.TranslateTo(0d, 0d, easing: Easing.CubicIn);
					break;
				default:
					break;
			}
		}

		async void Tapped(object sender, EventArgs e)
		{
			await this.ScaleTo(1.3d, easing: Easing.CubicOut);
			await Task.Delay(100);
			await this.ScaleTo(1d, easing: Easing.CubicIn);
		}
	}
}

