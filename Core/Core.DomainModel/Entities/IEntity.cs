﻿namespace Core.DomainModel.Entities
{
    public interface IEntity<TKey>
    {

        TKey Id { get; set; }

    }
}
