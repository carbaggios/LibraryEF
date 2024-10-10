﻿namespace WebApi.Interfaces
{
    public interface IHashService
    {
        (byte[] hash, byte[] salt) GetHash(string value, byte[]? salt = null);
    }
}
