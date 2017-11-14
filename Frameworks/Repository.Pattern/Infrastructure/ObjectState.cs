namespace Repository.Pattern.Infrastructure
{
    public enum ObjectState
    {
        //Noha Changes : Adding the following "NotSet" to support backward compatibility as the framework expects an explicit setting for Object state so by adding the new state NotSetbto depend on the default entity framework state
        NotSet,
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}