namespace Api.Seed;

public interface IInitializer
{
    void Initialize(int numOfContacts = 20);
}