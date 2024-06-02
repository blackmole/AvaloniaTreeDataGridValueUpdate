using Avalonia.Controls;
using AvaloniaTreeDataGridValueUpdate.ViewModels;

namespace AvaloniaTreeDataGridValueUpdate.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        ItemGrid.RowDragOver += CheckDropPosition;
        ItemGrid.RowDrop += CheckDropPosition;
        ItemGrid2.RowDragOver += CheckDropPosition;
        ItemGrid2.RowDrop += CheckDropPosition;
    }

    public void CheckDropPosition(object? sender, TreeDataGridRowDragEventArgs e)
    {
        if (e.TargetRow.Model.GetType().Name != "Folder" && e.Position == TreeDataGridRowDropPosition.Inside)
        {
            e.Position = TreeDataGridRowDropPosition.None;
        }
    }
}
