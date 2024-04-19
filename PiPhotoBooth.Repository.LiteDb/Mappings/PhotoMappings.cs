namespace PiPhotoBooth.Mappings;

using Model = Model.Photo;
using Entity = Entities.Photo;

internal static class PhotoMappings
{
    public static Model ToModel(this Entity entity)
    {
        return new Model(entity.Id, entity.Taken, entity.Path);
    }
    
    public static Entity ToEntity(this Model model)
    {
        return new Entity { Id = model.Id, Taken = model.Taken, Path = model.Path};
    }
}
