namespace Core.DomainModel
{

    public enum RoleEnum
    {
        Admin,
        Manager,
        Employee,
        Member,
    }

    public enum SubSystemEnum
    {
        Auth,
        CRUD,
        CQRS
    }

    public enum CachingDuration
    {
        Hour,
        Day,
        Week
    }

}
