namespace PiPhotoBooth.ViewModels;

using System.Collections.ObjectModel;
using Model;

public class GalleryViewModel
{
    public ObservableCollection<Photo> Photos { get; }
    
    public Photo SelectedPhoto { get; set; }
}