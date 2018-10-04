﻿using System.Threading.Tasks;

namespace RefactorThis.Core.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
