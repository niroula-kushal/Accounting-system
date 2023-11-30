﻿namespace Ams.Manager.Interfaces;

public interface IAuthManager
{
    Task Login(string username, string password);
    Task Logout();
}