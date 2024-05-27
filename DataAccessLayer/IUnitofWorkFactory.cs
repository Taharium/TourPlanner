namespace DataAccessLayer;

public interface IUnitofWorkFactory
{
    IUnitofWork CreateUnitofWork();
}