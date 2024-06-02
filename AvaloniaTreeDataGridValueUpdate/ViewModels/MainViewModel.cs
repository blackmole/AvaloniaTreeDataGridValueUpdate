using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using ReactiveUI;

namespace AvaloniaTreeDataGridValueUpdate.ViewModels;

public interface IItem : IReactiveObject
{
    public string Name { get; set; }
    public int Prop { get; set; }
    public int Size { get; }
    public ObservableCollection<IItem> Items { get; }
}

public class Item : ReactiveObject, IItem
{
    public string Name { get; set; }
    public string Prop1 { get; set; }
    public int Prop { get; set; }
    public int Size { get; set; }
    public ObservableCollection<IItem> Items { get; }
}

public class Folder: ReactiveObject, IItem
{
    public string Name { get; set; }
    public string Prop1 { get; set; }
    public int Prop { get; set; }
    public int Size => Items.Sum(i => i.Size);
    public ObservableCollection<IItem> Items { get; } = new();

    public Folder()
    {
        Items.CollectionChanged += (sender, args) => this.RaisePropertyChanged(nameof(Size));
    }
}

public class MainViewModel : ViewModelBase
{
    private ObservableCollection<IItem> _items;
    public HierarchicalTreeDataGridSource<IItem> Items { get; }
    public HierarchicalTreeDataGridSource<IItem> Items2 { get; }

    public MainViewModel()
    {
        _items = new ObservableCollection<IItem>
        {
            new Folder
            {
                Name = "Folder 1",
                Items =
                {
                    new Item { Name = "Item 1", Size = 10, Prop = 5 },
                    new Item { Name = "Item 2", Size = 20, Prop = 5 },
                    new Item { Name = "Item 3", Size = 30, Prop = 5 }
                }
            },
            new Folder
            {
                Name = "Folder 2",
                Items =
                {
                    new Item { Name = "Item 4", Size = 40, Prop = 5 },
                    new Item { Name = "Item 5", Size = 50, Prop = 5 },
                    new Item { Name = "Item 6", Size = 60, Prop = 5 }
                }
            }
        };
        Items = new HierarchicalTreeDataGridSource<IItem>(_items)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<IItem>(
                    new TextColumn<IItem, string>("Name", x => x.Name),
                    x => x.Items
                ),
                new TextColumn<IItem, int>("Size", x => x.Size),
                new TextColumn<IItem, int>("Prop", x => x.Prop),
            }
        };
        Items2 = new HierarchicalTreeDataGridSource<IItem>(_items)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<IItem>(
                    new TextColumn<IItem, string>("Name", x => x.Name),
                    x => x.Items
                ),
                new TextColumn<IItem, int>("Size", x => x.Size),
            }
        };
    }
}
