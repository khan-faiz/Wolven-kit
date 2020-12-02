﻿using AvalonDock.Layout;

namespace WolvenKitUI.Layout
{
	using System.Windows.Controls;
	using System.Windows;
	using WolvenKit.App.ViewModels;

	/// <summary>
	/// Implements a <see ref="DataTemplateSelector"/> for AvalonDock's documents and toolwindows.
	///
	/// One instance of this class is usually initialized in XAML and it returns
	/// a view for a specific given type of viewmodel.
	/// </summary>
	internal class PanesTemplateSelector : DataTemplateSelector
	{
		/// <summary>
		/// Default class constructor.
		/// </summary>
		public PanesTemplateSelector()
		{
		}

		/// <summary>
		/// Gets/sets the view instance of the file document.
		/// </summary>
		public DataTemplate FileViewTemplate { get; set; }

		/// <summary>
		/// Gets/sets the view instance of the LogView.
		/// </summary>
		public DataTemplate LogViewTemplate { get; set; }

		/// <summary>
		/// Gets/sets the view instance of the ProjectExplorerView.
		/// </summary>
		public DataTemplate ProjectExplorerTemplate { get; set; }


		/// <summary>
		/// Determines the matching view for a specific given type of viewmodel.
		/// </summary>
		/// <param name="item">Identifies the viewmodel object for which we require an associated view.</param>
		/// <param name="container">Identifies the container's instance that wants to resolve this association.</param>
		public override System.Windows.DataTemplate SelectTemplate(object item,
																   System.Windows.DependencyObject container)
		{
			var itemAsLayoutContent = item as LayoutContent;

			switch (item)
            {
                case IDocumentViewModel _:
                    return FileViewTemplate;
                case LogViewModel _:
                    return LogViewTemplate;
                case ProjectExplorerViewModel _:
                    return ProjectExplorerTemplate;
                default:
                    return base.SelectTemplate(item, container);
            }
        }
	}
}